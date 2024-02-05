using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        [Required(ErrorMessage = "El campo Nombre es obligatorio.")]
        [StringLength(50, ErrorMessage = "El campo Nombre no puede tener más de 50 caracteres.")]
        public string Nombre { get; set; } = null!;

        [Required(ErrorMessage = "El campo Primer Apellido es obligatorio.")]
        [StringLength(50, ErrorMessage = "El campo Primer Apellido no puede tener más de 50 caracteres.")]
        public string PrimerApellido { get; set; } = null!;

        [StringLength(50, ErrorMessage = "El campo Segundo Apellido no puede tener más de 50 caracteres.")]
        public string? SegundoApellido { get; set; }

        [Required(ErrorMessage = "El campo Calle es obligatorio.")]
        [StringLength(50, ErrorMessage = "El campo Calle no puede tener más de 50 caracteres.")]
        public string Calle { get; set; } = null!;

        [Required(ErrorMessage = "El campo Número es obligatorio.")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "El campo Número solo puede contener caracteres numéricos.")]
        public string Numero { get; set; } = null!;

        [Required(ErrorMessage = "El campo Colonia es obligatorio.")]
        [StringLength(50, ErrorMessage = "El campo Colonia no puede tener más de 50 caracteres.")]
        public string Colonia { get; set; } = null!;

        [Required(ErrorMessage = "El campo Código Postal es obligatorio.")]
        [RegularExpression("^[0-9]{1,10}$", ErrorMessage = "El campo Código Postal solo puede contener hasta 10 caracteres numéricos.")]
        public string CodigoPostal { get; set; } = null!;

        [Required(ErrorMessage = "El campo Teléfono es obligatorio.")]
        [RegularExpression("^[0-9]{1,10}$", ErrorMessage = "El campo Teléfono solo puede contener hasta 10 caracteres numéricos.")]
        public string Telefono { get; set; } = null!;

        [Required(ErrorMessage = "El campo RFC es obligatorio.")]
        [StringLength(18, ErrorMessage = "El campo RFC no puede tener más de 18 caracteres.")]
        [RegularExpression("^[A-Za-z0-9]+$", ErrorMessage = "El campo RFC solo puede contener caracteres alfanuméricos.")]
        public string Rfc { get; set; } = null!;
        public string? Estatus { get; set; }

        public HashSet<Documento> Documentos { get; set; }

        public virtual ICollection<ObservacionesRechazo> ObservacionesRechazos { get; set; }
    }
}
