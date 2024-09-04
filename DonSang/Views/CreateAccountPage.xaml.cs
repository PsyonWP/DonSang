using DonSang.context.Models;
using Microsoft.Maui.Controls;

namespace DonSang.Views
{
    public partial class CreateAccountPage : ContentPage
    {
        private readonly DonSangYJContext _dbContext;

        public CreateAccountPage(DonSangYJContext dbContext)
        {
            InitializeComponent();
            _dbContext = dbContext;
            BindingContext = new CreateAccountViewModel(dbContext);
        }
    }
}
