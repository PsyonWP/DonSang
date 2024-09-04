using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using DonSang.context.Models;
using Microsoft.Maui.Controls;

namespace DonSang.ViewModels
{
    public class ListeQuestionnairesViewModel : BaseViewModel
    {
        private readonly DonSangYJContext _dbContext;

        public ObservableCollection<Questionnaire> Questionnaires { get; set; } = new ObservableCollection<Questionnaire>();

        private Questionnaire _selectedQuestionnaire;
        public Questionnaire SelectedQuestionnaire
        {
            get => _selectedQuestionnaire;
            set
            {
                _selectedQuestionnaire = value;
                OnPropertyChanged();
                if (_selectedQuestionnaire != null)
                {
                    SelectQuestionnaireCommand.Execute(_selectedQuestionnaire); 
                }
            }
        }

        public ICommand SelectQuestionnaireCommand { get; }

        public ListeQuestionnairesViewModel(DonSangYJContext dbContext, Donneur donneur)
        {
            _dbContext = dbContext;
            SelectQuestionnaireCommand = new Command<Questionnaire>(OnSelectQuestionnaire);

            LoadQuestionnaires(donneur);
        }

        private void LoadQuestionnaires(Donneur donneur)
        {
            var questionnairesFromDb = _dbContext.Questionnaires
                .Where(q => q.IdDonneur == donneur.IdDonneur)
                .ToList();

            foreach (var questionnaire in questionnairesFromDb)
            {
                Questionnaires.Add(questionnaire);
            }
        }

        private async void OnSelectQuestionnaire(Questionnaire selectedQuestionnaire)
        {
            if (selectedQuestionnaire != null && selectedQuestionnaire.Resultat == "A Vérifier")
            {
                await Application.Current.MainPage.DisplayAlert("Info", $"Questionnaire sélectionné : {selectedQuestionnaire.IdQuestionnaire}", "OK");
                await Application.Current.MainPage.Navigation.PushAsync(
                    new Views.QuestionnaireDetailsPage(_dbContext, selectedQuestionnaire));
            }
        }

    }
}
