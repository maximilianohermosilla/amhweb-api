﻿using System;
using System.Collections.Generic;

namespace amhWebAPI.Models;

public partial class TipoTarjeta
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Tarjeta> Tarjeta { get; } = new List<Tarjeta>();
}