using System;
using System.Collections.Generic;

namespace amhWebAPI.Models;

public partial class Caratula
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Expediente> Expediente { get; } = new List<Expediente>();
}
