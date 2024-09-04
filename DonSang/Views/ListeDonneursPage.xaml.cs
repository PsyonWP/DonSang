using DonSang.context.Models;
using DonSang.ViewModels;
using Microsoft.Maui.Controls;

namespace DonSang.Views
{
    public partial class ListeDonneursPage : ContentPage
    {
        private readonly DonSangYJContext _dbContext;

        public ListeDonneursPage(DonSangYJContext dbContext)
        {
            InitializeComponent();
            _dbContext = dbContext;

            // Instancier le ViewModel avec le dbContext
            BindingContext = new ListeDonneursViewModel(_dbContext);
        }
    }
}
