using System;
using System.Collections.Generic;

namespace DonSang.context.Models;

public partial class Questionnaire
{
    public int IdQuestionnaire { get; set; }

    public DateTime? DateRemplissage { get; set; }

    public string? Statut { get; set; }

    public string? Resultat { get; set; }

    public int? IdDonneur { get; set; }

    public virtual Donneur? IdDonneurNavigation { get; set; }
}
