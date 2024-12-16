using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace prueba.Models
{
    [Table("cierre_caja")]

    public class CierreCaja
    {
        [Key]
        public int id { get; set; }

        [Required(ErrorMessage = "La fecha es obligatoria.")]
        [DataType(DataType.DateTime)]
        public DateTime? Fecha { get; set; }
        public decimal Monto { get; set; }

        [Required(ErrorMessage = "El empleado es obligatorio.")]
        [ForeignKey("empleado")]
        [Column("empleado_id")]
        public int EmpleadoId { get; set; }
        public Empleado Empleado { get; set; } // Relación con la entidad Empleado

        public string? estado { get; set; } = "activo";  // Valor por defecto 'activo
    }
}
