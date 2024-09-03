using System;
using System.Collections.Generic;

namespace DonSang.context.Models;

public partial class Question
{
    public int IdQuestion { get; set; }

    public int? NumeroQuestion { get; set; }

    public string? Enonce { get; set; }

    public string? Categorie { get; set; }

    public string? TypeQuestion { get; set; }

    public bool? Eliminatoire { get; set; }

    public virtual ICollection<Reponse> Reponses { get; set; } = new List<Reponse>();
}
