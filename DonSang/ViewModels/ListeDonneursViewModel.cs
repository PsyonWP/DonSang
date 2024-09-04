using DonSang.context.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace DonSang.ViewModels
{
    public class ListeDonneursViewModel : BaseViewModel
    {
        private readonly DonSangYJContext _dbContext;

        // Propriété Observable pour lister les DonneurViewModel au lieu des Donneurs bruts
        public ObservableCollection<DonneurViewModel> Donneurs { get; set; } = new ObservableCollection<DonneurViewModel>();

        // Propriété pour le Donneur sélectionné
        private DonneurViewModel _selectedDonneur;
        public DonneurViewModel SelectedDonneur
        {
            get => _selectedDonneur;
            set
            {
                _selectedDonneur = value;
                OnPropertyChanged();
            }
        }

        // Constructeur avec le dbContext
        public ListeDonneursViewModel(DonSangYJContext dbContext)
        {
            _dbContext = dbContext;
            LoadDonneurs(); // Charger les donneurs dès l'initialisation
        }

        // Charger les donneurs depuis la base de données
        private async Task LoadDonneurs()
        {
            var donneursFromDb = await Task.Run(() => _dbContext.Donneurs.ToList());

            foreach (var donneur in donneursFromDb)
            {
                // Utilisez DonneurViewModel pour encapsuler chaque Donneur
                Donneurs.Add(new DonneurViewModel(donneur));
            }
        }
    }
}
