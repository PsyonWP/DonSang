using DonSang.context.Models;
using DonSang.ViewModels;
using Microsoft.Maui.Controls;

namespace DonSang.Views
{
    public partial class QuestionnairePage : ContentPage
    {
        public QuestionnairePage(DonSangYJContext dbContext)
        {
            InitializeComponent();
            BindingContext = new QuestionnaireViewModel(dbContext);
        }
    }
}
