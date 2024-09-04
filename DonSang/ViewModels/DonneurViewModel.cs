using DonSang.context.Models;

namespace DonSang.ViewModels
{
    public class DonneurViewModel : BaseViewModel
    {
        public Donneur Donneur { get; private set; }

        public string NomPrenom => $"{Donneur.Nom} {Donneur.Prenom}";

        public DonneurViewModel(Donneur donneur)
        {
            Donneur = donneur;
        }
    }
}
