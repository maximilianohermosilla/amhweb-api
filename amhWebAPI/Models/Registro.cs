﻿using System;
using System.Collections.Generic;

namespace amhWebAPI.Models;

public partial class Registro
{
    public int Id { get; set; }

    public string? Descripcion { get; set; }

    public int? IdEmpresa { get; set; }

    public int? IdSuscripcion { get; set; }

    public int IdCuenta { get; set; }

    public int? IdRegistroVinculado { get; set; }

    public int? NumeroCuota { get; set; }

    public DateTime Fecha { get; set; }

    public decimal Valor { get; set; }

    public int? IdUsuario { get; set; }

    public string? Observaciones { get; set; }

    public virtual Cuenta IdCuentaNavigation { get; set; } = null!;

    public virtual Empresa? IdEmpresaNavigation { get; set; }

    public virtual RegistroVinculado? IdRegistroVinculadoNavigation { get; set; }

    public virtual Suscripcion? IdSuscripcionNavigation { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }
}
