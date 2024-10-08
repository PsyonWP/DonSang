﻿using DonSang.context.Models;
using DonSang.ViewModels;
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

            // Initialiser le ViewModel avec la navigation et le dbContext
            BindingContext = new HomeViewModel(Navigation, _dbContext);
        }
    }
}
