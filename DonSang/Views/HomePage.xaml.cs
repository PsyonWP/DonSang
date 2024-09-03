using DonSang.context.Models;
using Microsoft.Maui.Controls;

namespace DonSang.Views
{
    public partial class HomePage : ContentPage
    {
        private readonly DonSangYJContext _dbContext;

        public HomePage(DonSangYJContext dbContext)
        {
            InitializeComponent();
            _dbContext = dbContext;
        }

        private async void OnCreateAccountClicked(object sender, EventArgs e)
        {
            // Naviguer vers la page de création de compte
            await Navigation.PushAsync(new CreateAccountPage(_dbContext));
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            // Naviguer vers la page de connexion
            await Navigation.PushAsync(new LoginPage(_dbContext));
        }
    }
}
