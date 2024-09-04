using DonSang.context.Models;
using DonSang.ViewModels;
using Microsoft.Maui.Controls;
using System.Linq;

namespace DonSang.Views
{
    public partial class ListeQuestionnairesPage : ContentPage
    {
        private readonly DonSangYJContext _dbContext;

        public ListeQuestionnairesPage(DonSangYJContext dbContext, Donneur donneur)
        {
            InitializeComponent();
            _dbContext = dbContext;

            // Liez le ViewModel au contexte de données
            BindingContext = new ListeQuestionnairesViewModel(_dbContext, donneur);
        }

        // Ajoutez ici la méthode OnItemSelected pour capturer les événements de sélection
        private async void OnItemSelected(object sender, SelectionChangedEventArgs e)
        {
            var selectedQuestionnaire = e.CurrentSelection.FirstOrDefault() as Questionnaire;

            if (selectedQuestionnaire != null && selectedQuestionnaire.Resultat == "A Vérifier")
            {
                // Affichez une alerte pour confirmer la sélection
                await DisplayAlert("Questionnaire sélectionné", $"Questionnaire Id : {selectedQuestionnaire.IdQuestionnaire}", "OK");

                // Naviguer vers la page de détails des questions problématiques
                await Navigation.PushAsync(new QuestionnaireDetailsPage(_dbContext, selectedQuestionnaire));
            }
        }
    }
}
