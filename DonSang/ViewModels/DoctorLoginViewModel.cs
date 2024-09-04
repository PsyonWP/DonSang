using DonSang.context.Models; // Import du dbContext
using System.Windows.Input;
using Microsoft.Maui.Controls;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DonSang.ViewModels
{
    public class DoctorLoginViewModel : INotifyPropertyChanged
    {
        private const string DoctorUsername = "ychaabane";
        private const string DoctorPassword = "2209";
        private string _username;
        private string _password;
        private readonly INavigation _navigation;
        private readonly DonSangYJContext _dbContext; // Ajout du dbContext

        public event PropertyChangedEventHandler PropertyChanged;

        // Constructeur avec INavigation et dbContext pour permettre la navigation
        public DoctorLoginViewModel(INavigation navigation, DonSangYJContext dbContext)
        {
            _navigation = navigation;
            _dbContext = dbContext; // Assigner le dbContext
            DoctorLoginCommand = new Command(OnDoctorLogin); // Liaison de la commande
        }

        // Propriétés bindables
        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }

        // Commande de connexion
        public ICommand DoctorLoginCommand { get; }

        // Méthode de connexion
        private async void OnDoctorLogin()
        {
            if (Username == DoctorUsername && Password == DoctorPassword)
            {
                await Application.Current.MainPage.DisplayAlert("Succès", "Connexion réussie!", "OK");

                // Naviguer vers la page ListeDonneursPage
                await _navigation.PushAsync(new Views.ListeDonneursPage(_dbContext));
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Erreur", "Nom d'utilisateur ou mot de passe incorrect.", "OK");
            }
        }

        // Méthode pour notifier les changements de propriété
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
