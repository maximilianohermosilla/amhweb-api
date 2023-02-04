using System;
using System.Collections.Generic;

namespace amhWebAPI.Models;

public partial class UsuarioSistema
{
    public int Id { get; set; }

    public int IdUsuario { get; set; }

    public int IdSistema { get; set; }

    public virtual Sistema IdSistemaNavigation { get; set; } = null!;

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
