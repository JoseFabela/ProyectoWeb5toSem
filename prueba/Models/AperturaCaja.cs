using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace prueba.Models
{
	[Table("apertura_caja")]
	public class AperturaCaja
	{
        public int Id { get; set; }

        [Required(ErrorMessage = "El empleado es obligatorio.")]
        [ForeignKey("empleado")]
        [Column("empleado_id")]
        public int EmpleadoId { get; set; }

        [Required(ErrorMessage = "El monto es obligatorio.")]
        [Range(0, double.MaxValue, ErrorMessage = "El monto debe ser un valor positivo.")]
        public decimal Monto { get; set; }

        [Required(ErrorMessage = "La fecha es obligatoria.")]
        [DataType(DataType.DateTime)]
        public DateTime? Fecha { get; set; }

        public Empleado Empleado { get; set; } // Relación con la entidad Empleado

        public string? estado { get; set; } = "activo";  // Valor por defecto 'activo'

    }
}
