﻿using System;
using System.Collections.Generic;

namespace amhWebAPI.Models;

public partial class Expediente
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string Expediente1 { get; set; } = null!;

    public DateTime? Fecha { get; set; }

    public string? Documento { get; set; }

    public int? IdCaratula { get; set; }

    public int? IdActo { get; set; }

    public int? IdSituacionRevista { get; set; }

    public DateTime? FechaExpediente { get; set; }

    public bool? FirmadoSumario { get; set; }

    public bool? FirmadoLaborales { get; set; }

    public bool? EnviadoLaborales { get; set; }

    public bool? Avisado { get; set; }

    public string? Observaciones { get; set; }

    public virtual Acto? IdActoNavigation { get; set; }

    public virtual Caratula? IdCaratulaNavigation { get; set; }

    public virtual SituacionRevista? IdSituacionRevistaNavigation { get; set; }
}
