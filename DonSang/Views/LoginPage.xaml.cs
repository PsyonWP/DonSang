using DonSang.context.Models;
using Microsoft.Maui.Controls;
using System.Linq;

namespace DonSang.Views
{
    public partial class LoginPage : ContentPage
    {
        private readonly DonSangYJContext _dbContext;

        public LoginPage(DonSangYJContext dbContext)
        {
            InitializeComponent();
            _dbContext = dbContext;
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            string email = EmailEntry.Text;
            string password = PasswordEntry.Text;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                await DisplayAlert("Erreur", "Veuillez entrer votre email et mot de passe.", "OK");
                return;
            }

            // Vérifier les informations d'identification
            var user = _dbContext.Donneurs.FirstOrDefault(d => d.Email == email && d.MotDePasse == password);
            if (user != null)
            {
                await DisplayAlert("Succès", "Connexion réussie!", "OK");

                // Naviguer vers HomePage en passant l'instance de DonSangYJContext
                await Navigation.PushAsync(new HomePage(_dbContext));
            }
            else
            {
                await DisplayAlert("Erreur", "Échec de la connexion. Vérifiez vos informations.", "OK");
            }
        }
    }
}
