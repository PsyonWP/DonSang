using DonSang.Helpers;
using DonSang.context.Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace DonSang.ViewModels
{
    public class QuestionnaireViewModel : BaseViewModel
    {
        private readonly DonSangYJContext _dbContext;
        private List<Question> _questions;
        private int _currentQuestionIndex;

        private string _currentResponse;
        private bool _showTextBox;

        public QuestionnaireViewModel(DonSangYJContext dbContext)
        {
            _dbContext = dbContext;
            LoadQuestions();
            _currentQuestionIndex = 0;

            SelectResponseCommand = new Command<string>(OnSelectResponse);
            SubmitResponseCommand = new Command(OnSubmitResponse);

            ResetButtonColors();
        }

        public string CurrentQuestion => _questions[_currentQuestionIndex]?.Enonce;
        public bool ShowTextBox
        {
            get => _showTextBox;
            set => SetProperty(ref _showTextBox, value);
        }

        public string CurrentResponse
        {
            get => _currentResponse;
            set => SetProperty(ref _currentResponse, value);
        }

        public string YesButtonColor { get; private set; } = "#D32F2F";
        public string NoButtonColor { get; private set; } = "#D32F2F";
        public string DontKnowButtonColor { get; private set; } = "#D32F2F";

        public ICommand SelectResponseCommand { get; }
        public ICommand SubmitResponseCommand { get; }

        private void LoadQuestions()
        {
            _questions = _dbContext.Questions.OrderBy(q => q.NumeroQuestion).ToList();
        }

        private void OnSelectResponse(string response)
        {
            CurrentResponse = response;

            ResetButtonColors();
            switch (response)
            {
                case "Oui":
                    YesButtonColor = "#388E3C"; 
                    break;
                case "Non":
                    NoButtonColor = "#701818"; 
                    break;
                case "Je ne sais pas":
                    DontKnowButtonColor = "#FBC02D"; 
                    break;
            }
            OnPropertyChanged(nameof(YesButtonColor));
            OnPropertyChanged(nameof(NoButtonColor));
            OnPropertyChanged(nameof(DontKnowButtonColor));

            ShowTextBox = _questions[_currentQuestionIndex].TypeQuestion == "Texte";
        }

        private void ResetButtonColors()
        {
            YesButtonColor = "#D32F2F"; 
            NoButtonColor = "#D32F2F"; 
            DontKnowButtonColor = "#D32F2F"; 
        }

        private async void OnSubmitResponse()
        {
            if (UserSession.DonneurId == null)
            {
                await Application.Current.MainPage.DisplayAlert("Erreur", "Impossible de récupérer l'ID du donneur.", "OK");
                return;
            }

            bool? parsedResponse = CurrentResponse?.ToLower() switch
            {
                "oui" => true,
                "non" => false,
                _ => null
            };

            var reponse = new Reponse
            {
                IdQuestion = _questions[_currentQuestionIndex].IdQuestion,
                Reponse1 = parsedResponse,
                ComplementTexte = ShowTextBox ? CurrentResponse : null,
                IdDonneur = UserSession.DonneurId.Value,
                DateReponse = DateTime.Now
            };

            _dbContext.Reponses.Add(reponse);
            _dbContext.SaveChanges();

            _currentQuestionIndex++;

            if (_currentQuestionIndex >= _questions.Count)
            {
                EvaluateDonationEligibility();
            }
            else
            {
                OnPropertyChanged(nameof(CurrentQuestion));
                CurrentResponse = string.Empty;
                ShowTextBox = false;

                ResetButtonColors();
                OnPropertyChanged(nameof(YesButtonColor));
                OnPropertyChanged(nameof(NoButtonColor));
                OnPropertyChanged(nameof(DontKnowButtonColor));
            }
        }

        private async void EvaluateDonationEligibility()
        {
            var questionIdsEliminatory = _questions
                .Where(q => q.Eliminatoire == true)
                .Select(q => q.IdQuestion)
                .ToList();

            var nonEliminatoryQuestions = _questions
                .Where(q => q.Eliminatoire == false || q.Eliminatoire == null)
                .Select(q => q.IdQuestion)
                .ToList();

            var eliminatoryAnswers = _dbContext.Reponses
                .Where(r => questionIdsEliminatory.Contains(r.IdQuestion.Value) && r.IdDonneur == UserSession.DonneurId.Value)
                .ToList();

            var nonEliminatoryYesAnswers = _dbContext.Reponses
                .Where(r => nonEliminatoryQuestions.Contains(r.IdQuestion.Value) && r.IdDonneur == UserSession.DonneurId.Value && r.Reponse1 == true)
                .ToList();

            var dontKnowAnswers = _dbContext.Reponses
                .Where(r => r.IdDonneur == UserSession.DonneurId.Value && r.Reponse1 == null)
                .ToList();

            Console.WriteLine($"Eliminatory answers: {eliminatoryAnswers.Count}");
            Console.WriteLine($"Non-eliminatory Yes answers: {nonEliminatoryYesAnswers.Count}");
            Console.WriteLine($"Don't know answers: {dontKnowAnswers.Count}");

            string message;
            string resultat;

            if (eliminatoryAnswers.Any(r => r.Reponse1 == true))
            {
                message = "Don impossible.";
                resultat = "Infaisable";
            }
            else if (dontKnowAnswers.Any())
            {
                message = "Dépend de l'entretien.";
                resultat = "À vérifier";
            }

            else if (nonEliminatoryYesAnswers.Any())
            {
                message = "Dépend de l'entretien.";
                resultat = "À vérifier";
            }
            else
            {
    
                message = "Don faisable.";
                resultat = "Faisable";
            }

      
            var questionnaire = new Questionnaire
            {
                IdDonneur = UserSession.DonneurId.Value,
                DateRemplissage = DateTime.Now,
                Statut = "Fini",
                Resultat = resultat
            };

         
            _dbContext.Questionnaires.Add(questionnaire);
            _dbContext.SaveChanges();

            await Application.Current.MainPage.DisplayAlert("Résultat", message, "OK");

         
            await Application.Current.MainPage.Navigation.PopToRootAsync();
        }



    }
}
