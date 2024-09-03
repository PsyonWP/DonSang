using DonSang.context.Models;
using Microsoft.Maui.Controls;

namespace DonSang.Views
{
    public partial class CreateAccountPage : ContentPage
    {
        private readonly DonSangYJContext _dbContext;

        public CreateAccountPage(DonSangYJContext dbContext)
        {
            InitializeComponent();
            _dbContext = dbContext;

            // Définir la date maximale pour DatePicker à aujourd'hui
            DateNaissancePicker.MaximumDate = DateTime.Today;
        }

        private async void OnCreateAccountClicked(object sender, EventArgs e)
        {
            string nom = NomEntry.Text;
            string prenom = PrenomEntry.Text;
            DateTime? dateNaissance = DateNaissancePicker.Date;
            string email = EmailEntry.Text;
            string password = PasswordEntry.Text;
            string confirmPassword = ConfirmPasswordEntry.Text;

            if (string.IsNullOrEmpty(nom) || string.IsNullOrEmpty(prenom) ||
                !dateNaissance.HasValue || string.IsNullOrEmpty(email) ||
                string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
            {
                await DisplayAlert("Erreur", "Tous les champs doivent être remplis.", "OK");
                return;
            }

            if (password != confirmPassword)
            {
                await DisplayAlert("Erreur", "Les mots de passe ne correspondent pas.", "OK");
                return;
            }

            // Vérifier si l'email existe déjà
            var existingUser = _dbContext.Donneurs.FirstOrDefault(d => d.Email == email);
            if (existingUser != null)
            {
                await DisplayAlert("Erreur", "Cet email est déjà utilisé.", "OK");
                return;
            }

            // Créer un nouvel utilisateur
            var newUser = new Donneur
            {
                Nom = nom,
                Prenom = prenom,
                DateNaissance = DateOnly.FromDateTime(dateNaissance.Value),
                Email = email,
                MotDePasse = password, // Note: Dans une vraie application, hachez le mot de passe avant de le stocker
                DateInscription = DateTime.Now
            };

            _dbContext.Donneurs.Add(newUser);
            await _dbContext.SaveChangesAsync();

            await DisplayAlert("Succès", "Compte créé avec succès!", "OK");

            // Redirection vers la page de connexion ou l'accueil
            await Navigation.PopAsync(); // Retour à la page précédente (HomePage)
        }
    }
}
