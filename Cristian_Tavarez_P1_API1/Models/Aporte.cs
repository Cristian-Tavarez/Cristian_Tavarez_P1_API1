using System.ComponentModel.DataAnnotations;

namespace Cristian_Tavarez_P1_API1.Models
{
    public class Aporte
    {
        [Key] 
        public int AporteId  { get; set; }

        [Required(ErrorMessage = "Fecha es Requerida")]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "Concepto es Requerido")]
        [StringLength(maximumLength: 5000, ErrorMessage = "Concepto muy largo")]
        public string? Observacion { get; set; }

        [Required(ErrorMessage = "Monto es Requerido")]
        [Range(minimum: 0.01, maximum: double.MaxValue, ErrorMessage = "El Monto debe ser mayor a 0.01")]
        public double Monto { get; set; }
    }
}
