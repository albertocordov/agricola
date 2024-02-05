using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AgricolaProspectos.Models
{
    public partial class Documento
    {
        [Key]
        public int DocumentoId { get; set; }
        public int? ProspectoId { get; set; }
        public string NombreDocumento { get; set; } = null!;
        public string? RutaDocumento { get; set; }
        public virtual Prospecto? Prospecto { get; set; }

        [NotMapped]
        public IFormFile Archivo { get; set; }
    }
}
