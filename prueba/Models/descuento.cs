using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace prueba.Models
{
    [Table("descuento")]
    public class descuento
    {
		public int id { get; set; }

		public decimal? porcentaje { get; set; }

		[StringLength(255, ErrorMessage = "La descripción no puede exceder los 255 caracteres.")]
		public string? descripcion { get; set; }

		[DataType(DataType.Date)]
		[Display(Name = "Fecha de Inicio")]
		public DateTime? fecha_inicio { get; set; }

		[DataType(DataType.Date)]
		[Display(Name = "Fecha de Fin")]
		public DateTime? fecha_fin { get; set; }

		public string? estado { get; set; } = "activo";  // Valor por defecto 'activo'


	}
}
