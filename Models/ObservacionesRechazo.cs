using System;
using System.Collections.Generic;

namespace AgricolaProspectos.Models
{
    public partial class ObservacionesRechazo
    {
        public int ObservacionId { get; set; }
        public int? ProspectoId { get; set; }
        public string Observacion { get; set; } = null!;

        public virtual Prospecto? Prospecto { get; set; }
    }
}
