﻿using System;
using System.Collections.Generic;

namespace amhWebAPI.Models;

public partial class Empresa
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual Registro? Registro { get; set; }
}
