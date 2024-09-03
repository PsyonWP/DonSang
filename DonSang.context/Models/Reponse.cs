using System;
using System.Collections.Generic;

namespace DonSang.context.Models;

public partial class Reponse
{
    public int IdReponse { get; set; }

    public bool? Reponse1 { get; set; }

    public string? ComplementTexte { get; set; }

    public DateTime? DateReponse { get; set; }

    public int? IdDonneur { get; set; }

    public int? IdQuestion { get; set; }

    public virtual Donneur? IdDonneurNavigation { get; set; }

    public virtual Question? IdQuestionNavigation { get; set; }
}
