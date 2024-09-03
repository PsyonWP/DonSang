using DonSang.context.Models;
using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace DonSang.ViewModels
{
    public class StartQuestionnaireViewModel : BaseViewModel
    {
        private readonly DonSangYJContext _dbContext;

        public ICommand StartQuestionnaireCommand { get; }

        public StartQuestionnaireViewModel(DonSangYJContext dbContext)
        {
            _dbContext = dbContext;
            StartQuestionnaireCommand = new Command(OnStartQuestionnaire);
        }

        private async void OnStartQuestionnaire()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new QuestionnairePage(_dbContext));
        }
    }
}
