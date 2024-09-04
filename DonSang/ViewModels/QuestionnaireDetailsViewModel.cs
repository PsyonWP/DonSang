using DonSang.context.Models;
using System.Collections.ObjectModel;
using System.Linq;

namespace DonSang.ViewModels
{
    public class QuestionnaireDetailsViewModel : BaseViewModel
    {
        public ObservableCollection<Reponse> ProblematicQuestions { get; set; }

        public QuestionnaireDetailsViewModel(DonSangYJContext dbContext, Questionnaire questionnaire)
        {
            // Charger les réponses problématiques (Je ne sais pas ou Oui pour non-éliminatoires)
            ProblematicQuestions = new ObservableCollection<Reponse>(
                dbContext.Reponses
                    .Where(r => r.IdDonneur == questionnaire.IdDonneur && // Filtrer par le donneur lié au questionnaire
                                r.IdQuestionNavigation.Eliminatoire == false && // Filtrer les questions non éliminatoires
                                (r.Reponse1 == null || r.Reponse1 == true)) // "Je ne sais pas" ou "Oui"
                    .ToList()
            );
        }
    }
}
