using System;
using System.Collections.Generic;

namespace AgricolaProspectos.Models
{
    public partial class Prospecto
    {
        public Prospecto()
        {
            Documentos = new HashSet<Documento>();
            ObservacionesRechazos = new HashSet<ObservacionesRechazo>();
        }

        public int ProspectoId { get; set; }
        public string Nombre { get; set; } = null!;
        public string PrimerApellido { get; set; } = null!;
        public string? SegundoApellido { get; set; }
        public string Calle { get; set; } = null!;
        public string Numero { get; set; } = null!;
        public string Colonia { get; set; } = null!;
        public string CodigoPostal { get; set; } = null!;
        public string Telefono { get; set; } = null!;
        public string Rfc { get; set; } = null!;
        public string? Estatus { get; set; }

        public HashSet<Documento> Documentos { get; set; }

        public virtual ICollection<ObservacionesRechazo> ObservacionesRechazos { get; set; }
    }
}
