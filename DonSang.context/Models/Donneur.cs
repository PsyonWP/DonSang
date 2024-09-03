﻿using System;
using System.Collections.Generic;

namespace DonSang.context.Models;

public partial class Donneur
{
    public int IdDonneur { get; set; }

    public string? Email { get; set; }

    public string? MotDePasse { get; set; }

    public string? Nom { get; set; }

    public string? Prenom { get; set; }

    public DateOnly? DateNaissance { get; set; }

    public DateTime? DateInscription { get; set; }

    public virtual ICollection<Questionnaire> Questionnaires { get; set; } = new List<Questionnaire>();

    public virtual ICollection<Reponse> Reponses { get; set; } = new List<Reponse>();
}
