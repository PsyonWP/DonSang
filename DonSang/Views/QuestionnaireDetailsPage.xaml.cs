using DonSang.context.Models;

namespace DonSang.Views
{
    public partial class QuestionnaireDetailsPage : ContentPage
    {
        private readonly DonSangYJContext _dbContext;
        private readonly Questionnaire _questionnaire;

        public QuestionnaireDetailsPage(DonSangYJContext dbContext, Questionnaire questionnaire)
        {
            InitializeComponent();
            _dbContext = dbContext;
            _questionnaire = questionnaire;

            // Chargez les détails du questionnaire ici
        }
    }
}
