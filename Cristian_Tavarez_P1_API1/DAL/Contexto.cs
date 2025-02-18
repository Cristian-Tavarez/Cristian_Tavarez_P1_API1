using System.ComponentModel.DataAnnotations;
using Cristian_Tavarez_P1_API1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;

namespace Cristian_Tavarez_P1_API1.DAL
{
    public class Ingresos
    {
        [Key]
        public int AporteID { get; set; }

        [Required(ErrorMessage = "Fecha es Requerida")]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "Concepto es Requerido")]
        [StringLength(maximumLength: 5000, ErrorMessage = "Concepto muy largo")]
        public string? Concepto { get; set; }

        [Required(ErrorMessage = "Monto es Requerido")]
        [Range(minimum: 0.01, maximum: double.MaxValue, ErrorMessage = "El Monto debe ser mayor a 0.01")]
        public double Monto { get; set; }
    }
}