using DonSang.context.Models;
using DonSang.Helpers;  // Assurez-vous que cette directive using est présente
using System.Windows.Input;

namespace DonSang.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly DonSangYJContext _dbContext;
        private string _email;
        private string _password;

        public LoginViewModel(DonSangYJContext dbContext)
        {
            _dbContext = dbContext;
            LoginCommand = new Command(OnLogin);
        }

        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public ICommand LoginCommand { get; }

        private async void OnLogin()
        {
            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
            {
                await Application.Current.MainPage.DisplayAlert("Erreur", "Veuillez entrer votre email et mot de passe.", "OK");
                return;
            }

            var user = _dbContext.Donneurs.FirstOrDefault(d => d.Email == Email && d.MotDePasse == Password);
            if (user != null)
            {
                await Application.Current.MainPage.DisplayAlert("Succès", "Connexion réussie!", "OK");

                // Stocker l'ID du donneur dans la session utilisateur
                UserSession.DonneurId = user.IdDonneur;

                // Naviguer vers StartQuestionnairePage avec le dbContext
                await Application.Current.MainPage.Navigation.PushAsync(new StartQuestionnairePage(_dbContext));
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Erreur", "Échec de la connexion. Vérifiez vos informations.", "OK");
            }
        }
    }
}
