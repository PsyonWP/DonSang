using Microsoft.Maui.Controls;
using System;
using DonSang.context.Models;

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
            // Naviguer vers la page de création de compte en passant le contexte de base de données
            await Navigation.PushAsync(new CreateAccountPage(_dbContext));
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            // Naviguer vers la page de connexion en passant le contexte de base de données
            await Navigation.PushAsync(new LoginPage(_dbContext));
        }
    }
}
