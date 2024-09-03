using DonSang.context.Models;
using DonSang.ViewModels;
using Microsoft.Maui.Controls;

namespace DonSang.Views
{
    public partial class StartQuestionnairePage : ContentPage
    {
        private readonly DonSangYJContext _dbContext;

        public StartQuestionnairePage(DonSangYJContext dbContext)
        {
            InitializeComponent();
            _dbContext = dbContext;
            BindingContext = new StartQuestionnaireViewModel(dbContext);
        }
    }
}
