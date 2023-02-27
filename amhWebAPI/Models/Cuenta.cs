﻿using System;
using System.Collections.Generic;

namespace amhWebAPI.Models;

public partial class Cuenta
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public int IdTipoCuenta { get; set; }

    public int? IdTarjeta { get; set; }

    public int IdUsuario { get; set; }

    public virtual Usuario IdNavigation { get; set; } = null!;

    public virtual Tarjeta? IdTarjetaNavigation { get; set; }

    public virtual TipoCuenta IdTipoCuentaNavigation { get; set; } = null!;

    public virtual ICollection<Registro> Registro { get; } = new List<Registro>();
}