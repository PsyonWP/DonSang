using System.Collections.ObjectModel;
using System.Linq;
using DonSang.context.Models;

namespace DonSang.ViewModels
{
    public class QuestionnaireDetailsViewModel : BaseViewModel
    {
        private readonly DonSangYJContext _dbContext;

        public ObservableCollection<Reponse> ProblematicQuestions { get; set; }

        public QuestionnaireDetailsViewModel(DonSangYJContext dbContext, Questionnaire questionnaire)
        {
            _dbContext = dbContext;

            ProblematicQuestions = new ObservableCollection<Reponse>(
                _dbContext.Reponses
                    .Where(r => r.IdDonneur == questionnaire.IdDonneur &&
                                r.IdQuestionNavigation.Eliminatoire == false &&
                                (r.Reponse1 == null || r.Reponse1 == true))
                    .ToList()
            );
        }
    }
}
