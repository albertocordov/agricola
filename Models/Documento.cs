using System;
using System.Collections.Generic;

namespace AgricolaProspectos.Models
{
    public partial class Documento
    {
        public int DocumentoId { get; set; }
        public int? ProspectoId { get; set; }
        public string NombreDocumento { get; set; } = null!;
        public string RutaDocumento { get; set; } = null!;

        public virtual Prospecto? Prospecto { get; set; }
    }
}
