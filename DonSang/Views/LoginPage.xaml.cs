using DonSang.context.Models;
using Microsoft.Maui.Controls;

namespace DonSang.Views
{
    public partial class LoginPage : ContentPage
    {
        private readonly DonSangYJContext _dbContext;

        public LoginPage(DonSangYJContext dbContext)
        {
            InitializeComponent();
            _dbContext = dbContext;
            BindingContext = new LoginViewModel(dbContext);
        }
    }
}
