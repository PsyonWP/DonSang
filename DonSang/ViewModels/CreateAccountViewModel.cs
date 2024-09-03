using DonSang.context.Models;
using System.Windows.Input;

namespace DonSang.ViewModels
{
    public class CreateAccountViewModel : BaseViewModel
    {
        private readonly DonSangYJContext _dbContext;
        private string _nom;
        private string _prenom;
        private DateTime _dateNaissance;
        private string _email;
        private string _password;
        private string _confirmPassword;

        public CreateAccountViewModel(DonSangYJContext dbContext)
        {
            _dbContext = dbContext;
            CreateAccountCommand = new Command(OnCreateAccount);
        }

        public string Nom
        {
            get => _nom;
            set => SetProperty(ref _nom, value);
        }

        public string Prenom
        {
            get => _prenom;
            set => SetProperty(ref _prenom, value);
        }

        public DateTime DateNaissance
        {
            get => _dateNaissance;
            set => SetProperty(ref _dateNaissance, value);
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

        public string ConfirmPassword
        {
            get => _confirmPassword;
            set => SetProperty(ref _confirmPassword, value);
        }

        public ICommand CreateAccountCommand { get; }

        private async void OnCreateAccount()
        {
            if (string.IsNullOrEmpty(Nom) || string.IsNullOrEmpty(Prenom) ||
                string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password) ||
                string.IsNullOrEmpty(ConfirmPassword))
            {
                await Application.Current.MainPage.DisplayAlert("Erreur", "Tous les champs doivent être remplis.", "OK");
                return;
            }

            if (Password != ConfirmPassword)
            {
                await Application.Current.MainPage.DisplayAlert("Erreur", "Les mots de passe ne correspondent pas.", "OK");
                return;
            }

            var existingUser = _dbContext.Donneurs.FirstOrDefault(d => d.Email == Email);
            if (existingUser != null)
            {
                await Application.Current.MainPage.DisplayAlert("Erreur", "Cet email est déjà utilisé.", "OK");
                return;
            }

            var newUser = new Donneur
            {
                Nom = Nom,
                Prenom = Prenom,
                DateNaissance = DateOnly.FromDateTime(DateNaissance),
                Email = Email,
                MotDePasse = Password,
                DateInscription = DateTime.Now
            };

            _dbContext.Donneurs.Add(newUser);
            await _dbContext.SaveChangesAsync();

            await Application.Current.MainPage.DisplayAlert("Succès", "Compte créé avec succès!", "OK");

            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}
