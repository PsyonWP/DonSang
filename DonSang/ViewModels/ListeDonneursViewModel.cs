using System.Windows.Input;
using DonSang.context.Models;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;

namespace DonSang.ViewModels
{
    public class ListeDonneursViewModel : BaseViewModel
    {
        private readonly DonSangYJContext _dbContext;

        public ObservableCollection<DonneurViewModel> Donneurs { get; set; } = new ObservableCollection<DonneurViewModel>();

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

        public ICommand SelectDonneurCommand { get; }

        public ListeDonneursViewModel(DonSangYJContext dbContext)
        {
            _dbContext = dbContext;
            SelectDonneurCommand = new Command<DonneurViewModel>(OnSelectDonneur);

            LoadDonneurs();
        }

        private void LoadDonneurs()
        {
            var donneursFromDb = _dbContext.Donneurs.ToList();

            foreach (var donneur in donneursFromDb)
            {
                Donneurs.Add(new DonneurViewModel(donneur));
            }
        }

        private async void OnSelectDonneur(DonneurViewModel selectedDonneur)
        {
            if (selectedDonneur != null)
            {
                await Application.Current.MainPage.Navigation.PushAsync(new Views.ListeQuestionnairesPage(_dbContext, selectedDonneur.Donneur));
            }
        }
    }
}
