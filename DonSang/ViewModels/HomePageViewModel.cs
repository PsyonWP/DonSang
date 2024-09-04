using DonSang.context.Models; // Ajoutez ceci pour utiliser le DonSangYJContext
using System.Windows.Input;
using Microsoft.Maui.Controls;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DonSang.ViewModels
{
    public class HomeViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private bool _isPatientSectionVisible;
        private readonly INavigation _navigation;
        private readonly DonSangYJContext _dbContext; // Stocker le dbContext

        public HomeViewModel(INavigation navigation, DonSangYJContext dbContext)
        {
            _navigation = navigation;
            _dbContext = dbContext; // Assigner le dbContext reçu
            IsPatientSectionVisible = false;  // Par défaut, cacher les boutons "Créer un compte" et "Connexion"
            ShowPatientSectionCommand = new Command(ShowPatientSection);
            ShowDoctorLoginCommand = new Command(OnDoctorLogin);
            CreateAccountCommand = new Command(OnCreateAccount);
            LoginCommand = new Command(OnLogin);
        }

        // Propriété pour gérer la visibilité des boutons "Créer un compte" et "Connexion"
        public bool IsPatientSectionVisible
        {
            get => _isPatientSectionVisible;
            set
            {
                _isPatientSectionVisible = value;
                OnPropertyChanged();
            }
        }

        // Commandes pour interagir avec l'interface utilisateur
        public ICommand ShowPatientSectionCommand { get; }
        public ICommand ShowDoctorLoginCommand { get; }
        public ICommand CreateAccountCommand { get; }
        public ICommand LoginCommand { get; }

        // Méthode pour afficher la section "patient"
        private void ShowPatientSection()
        {
            IsPatientSectionVisible = true;
        }

        // Méthode pour naviguer vers la page de login pour médecins
        private async void OnDoctorLogin()
        {
            // Passer le dbContext lors de la navigation vers DoctorLoginPage
            await _navigation.PushAsync(new Views.DoctorLoginPage(_dbContext));
        }

        // Méthode pour naviguer vers la page de création de compte
        private async void OnCreateAccount()
        {
            await _navigation.PushAsync(new Views.CreateAccountPage(_dbContext)); // Passer le dbContext
        }

        // Méthode pour naviguer vers la page de connexion
        private async void OnLogin()
        {
            await _navigation.PushAsync(new Views.LoginPage(_dbContext)); // Passer le dbContext
        }

        // Gestion de l'événement OnPropertyChanged
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
