﻿using System;
using System.Collections.Generic;

namespace amhWebAPI.Models;

public partial class TipoCuenta
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Cuenta> Cuenta { get; } = new List<Cuenta>();
}