using DonSang.context.Models;
using DonSang.ViewModels;
using Microsoft.Maui.Controls;

namespace DonSang.Views
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage(DonSangYJContext dbContext)
        {
            InitializeComponent();
            BindingContext = new LoginViewModel(dbContext);
        }
    }
}
