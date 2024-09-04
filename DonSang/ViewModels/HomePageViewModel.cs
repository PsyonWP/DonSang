using DonSang.context.Models; 
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
        private readonly DonSangYJContext _dbContext; 

        public HomeViewModel(INavigation navigation, DonSangYJContext dbContext)
        {
            _navigation = navigation;
            _dbContext = dbContext; 
            IsPatientSectionVisible = false; 
            ShowPatientSectionCommand = new Command(ShowPatientSection);
            ShowDoctorLoginCommand = new Command(OnDoctorLogin);
            CreateAccountCommand = new Command(OnCreateAccount);
            LoginCommand = new Command(OnLogin);
        }

        public bool IsPatientSectionVisible
        {
            get => _isPatientSectionVisible;
            set
            {
                _isPatientSectionVisible = value;
                OnPropertyChanged();
            }
        }

        public ICommand ShowPatientSectionCommand { get; }
        public ICommand ShowDoctorLoginCommand { get; }
        public ICommand CreateAccountCommand { get; }
        public ICommand LoginCommand { get; }

        private void ShowPatientSection()
        {
            IsPatientSectionVisible = true;
        }

        private async void OnDoctorLogin()
        {
            await _navigation.PushAsync(new Views.DoctorLoginPage(_dbContext));
        }

      
        private async void OnCreateAccount()
        {
            await _navigation.PushAsync(new Views.CreateAccountPage(_dbContext)); 
        }

        private async void OnLogin()
        {
            await _navigation.PushAsync(new Views.LoginPage(_dbContext)); 
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
