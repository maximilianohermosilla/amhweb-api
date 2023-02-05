﻿using amhWebAPI.Models;

namespace amhWebAPI.DTO
{
    #nullable disable
    public class PaisDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Imagen { get; set; }
        public int? IdArchivo { get; set; }
        public byte[]? ImageFile { get; set; }
        public List<Ciudad>? ciudades { get; set; }
    }
}
