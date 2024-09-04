using DonSang.context.Models;
using DonSang.ViewModels;
using Microsoft.Maui.Controls;

namespace DonSang.Views
{
    public partial class DoctorLoginPage : ContentPage
    {
        private readonly DonSangYJContext _dbContext;

        public DoctorLoginPage(DonSangYJContext dbContext)
        {
            InitializeComponent();
            _dbContext = dbContext;

            // Instancier le ViewModel avec la navigation et le dbContext
            BindingContext = new DoctorLoginViewModel(Navigation, _dbContext);
        }
    }
}
