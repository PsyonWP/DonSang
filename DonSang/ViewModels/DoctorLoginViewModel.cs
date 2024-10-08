﻿using DonSang.context.Models; 
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
        private readonly DonSangYJContext _dbContext; 

        public event PropertyChangedEventHandler PropertyChanged;

        
        public DoctorLoginViewModel(INavigation navigation, DonSangYJContext dbContext)
        {
            _navigation = navigation;
            _dbContext = dbContext; 
            DoctorLoginCommand = new Command(OnDoctorLogin); 
        }

     
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

     
        public ICommand DoctorLoginCommand { get; }

        private async void OnDoctorLogin()
        {
            if (Username == DoctorUsername && Password == DoctorPassword)
            {
                await Application.Current.MainPage.DisplayAlert("Succès", "Connexion réussie!", "OK");

              
                await _navigation.PushAsync(new Views.ListeDonneursPage(_dbContext));
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Erreur", "Nom d'utilisateur ou mot de passe incorrect.", "OK");
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
