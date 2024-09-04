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

            // Initialiser les couleurs des boutons
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

            // Mettre à jour les couleurs des boutons en fonction de la sélection
            ResetButtonColors();
            switch (response)
            {
                case "Oui":
                    YesButtonColor = "#388E3C"; // Vert
                    break;
                case "Non":
                    NoButtonColor = "#701818"; // Rouge
                    break;
                case "Je ne sais pas":
                    DontKnowButtonColor = "#FBC02D"; // Jaune
                    break;
            }
            OnPropertyChanged(nameof(YesButtonColor));
            OnPropertyChanged(nameof(NoButtonColor));
            OnPropertyChanged(nameof(DontKnowButtonColor));

            // Si la question nécessite un complément de réponse, montrer la textbox
            ShowTextBox = _questions[_currentQuestionIndex].TypeQuestion == "Texte";
        }

        private void ResetButtonColors()
        {
            YesButtonColor = "#D32F2F"; // Couleur par défaut
            NoButtonColor = "#D32F2F"; // Couleur par défaut
            DontKnowButtonColor = "#D32F2F"; // Couleur par défaut
        }

        private async void OnSubmitResponse()
        {
            if (UserSession.DonneurId == null)
            {
                await Application.Current.MainPage.DisplayAlert("Erreur", "Impossible de récupérer l'ID du donneur.", "OK");
                return;
            }

            // Convertir CurrentResponse en bool?
            bool? parsedResponse = CurrentResponse?.ToLower() switch
            {
                "oui" => true,
                "non" => false,
                _ => null // "Je ne sais pas" ou autre réponse non définie
            };

            // Enregistrer la réponse dans la base de données
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

            // Passer à la question suivante
            _currentQuestionIndex++;

            if (_currentQuestionIndex >= _questions.Count)
            {
                EvaluateDonationEligibility();
            }
            else
            {
                // Mettre à jour la question actuelle
                OnPropertyChanged(nameof(CurrentQuestion));
                CurrentResponse = string.Empty;
                ShowTextBox = false;

                // Réinitialiser les couleurs des boutons
                ResetButtonColors();
                OnPropertyChanged(nameof(YesButtonColor));
                OnPropertyChanged(nameof(NoButtonColor));
                OnPropertyChanged(nameof(DontKnowButtonColor));
            }
        }

        private async void EvaluateDonationEligibility()
        {
            // Charger les questions éliminatoires et non-éliminatoires en mémoire
            var questionIdsEliminatory = _questions
                .Where(q => q.Eliminatoire == true)
                .Select(q => q.IdQuestion)
                .ToList();

            var nonEliminatoryQuestions = _questions
                .Where(q => q.Eliminatoire == false || q.Eliminatoire == null)
                .Select(q => q.IdQuestion)
                .ToList();

            // Filtrer les réponses en utilisant les IDs des questions
            var eliminatoryAnswers = _dbContext.Reponses
                .Where(r => questionIdsEliminatory.Contains(r.IdQuestion.Value) && r.IdDonneur == UserSession.DonneurId.Value)
                .ToList();

            var nonEliminatoryYesAnswers = _dbContext.Reponses
                .Where(r => nonEliminatoryQuestions.Contains(r.IdQuestion.Value) && r.IdDonneur == UserSession.DonneurId.Value && r.Reponse1 == true)
                .ToList();

            var dontKnowAnswers = _dbContext.Reponses
                .Where(r => r.IdDonneur == UserSession.DonneurId.Value && r.Reponse1 == null)
                .ToList();

            // Pour déboguer, vérifier les réponses récupérées
            Console.WriteLine($"Eliminatory answers: {eliminatoryAnswers.Count}");
            Console.WriteLine($"Non-eliminatory Yes answers: {nonEliminatoryYesAnswers.Count}");
            Console.WriteLine($"Don't know answers: {dontKnowAnswers.Count}");

            string message;
            string resultat;

            // Vérifier les réponses aux questions éliminatoires
            if (eliminatoryAnswers.Any(r => r.Reponse1 == true))
            {
                // Si l'utilisateur a répondu "Oui" à une question éliminatoire
                message = "Don impossible.";
                resultat = "Infaisable";
            }
            // Vérifier si des réponses "Je ne sais pas" existent
            else if (dontKnowAnswers.Any())
            {
                message = "Dépend de l'entretien.";
                resultat = "À vérifier";
            }
            // Vérifier si des réponses "Oui" aux questions non-éliminatoires existent
            else if (nonEliminatoryYesAnswers.Any())
            {
                message = "Dépend de l'entretien.";
                resultat = "À vérifier";
            }
            else
            {
                // Si l'utilisateur a répondu "Non" à toutes les questions
                message = "Don faisable.";
                resultat = "Faisable";
            }

            // Enregistrer le résultat du questionnaire dans la table Questionnaire
            var questionnaire = new Questionnaire
            {
                IdDonneur = UserSession.DonneurId.Value,
                DateRemplissage = DateTime.Now,
                Statut = "Fini", // Questionnaire terminé
                Resultat = resultat
            };

            // Ajouter le questionnaire à la base de données
            _dbContext.Questionnaires.Add(questionnaire);
            _dbContext.SaveChanges();

            // Afficher le résultat à l'utilisateur
            await Application.Current.MainPage.DisplayAlert("Résultat", message, "OK");

            // Retourner à la page d'accueil ou une autre page
            await Application.Current.MainPage.Navigation.PopToRootAsync();
        }



    }
}
