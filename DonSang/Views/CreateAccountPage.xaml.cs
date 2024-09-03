using DonSang.context.Models;
using DonSang.ViewModels;
using Microsoft.Maui.Controls;

namespace DonSang.Views
{
    public partial class CreateAccountPage : ContentPage
    {
        public CreateAccountPage(DonSangYJContext dbContext)
        {
            InitializeComponent();
            BindingContext = new CreateAccountViewModel(dbContext);
        }
    }
}
