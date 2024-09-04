using DonSang.context.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonSang.ViewModels
{
    public class DonneurViewModel
    {
        private readonly Donneur _donneur;

        public DonneurViewModel(Donneur donneur)
        {
            _donneur = donneur;
        }

        // Propriété calculée pour concaténer Nom et Prénom
        public string NomPrenom => $"{_donneur.Nom} {_donneur.Prenom}";

        // Si vous avez besoin d'autres propriétés de Donneur, exposez-les ici
        public int IdDonneur => _donneur.IdDonneur;
    }

}
