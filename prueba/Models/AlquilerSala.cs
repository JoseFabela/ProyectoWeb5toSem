using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace prueba.Models
{
    [Table("alquiler_sala")]
    public class AlquilerSala
    {
        [Key]
        public int id { get; set; }


        
        [Required(ErrorMessage = "El empleado es obligatorio.")]
        [ForeignKey("empleado")]
        [Column("empleado_id")]
        public int EmpleadoId { get; set; }

        [Required(ErrorMessage = "El Jugador es obligatorio.")]
        [ForeignKey("jugador")]
        [Column("jugador_id")]
        public int JugadorId { get; set; }

        [Required(ErrorMessage = "El Jugador es obligatorio.")]
        [ForeignKey("sala_vip")]
        [Column("sala_vip_id")]
        public int SalaVipId { get; set; }
        [Required(ErrorMessage = "La fecha de inicio es obligatoria.")]
        [DataType(DataType.Date)]
        [Column("fecha_inicio")] // Mapea a la columna correcta
        [Display(Name = "Fecha de Inicio")]
        public DateTime fechaInicio { get; set; }

        [Required(ErrorMessage = "La fecha de fin es obligatoria.")]
        [DataType(DataType.Date)]
        [Column("fecha_fin")] // Mapea a la columna correcta
        [Display(Name = "Fecha de Fin")]
        public DateTime fechaFin { get; set; }


        public Empleado Empleado { get; set; } // Relación con la entidad Empleado
        public SalaVip SalaVip { get; set; } // Relación con la entidad Empleado
        public Jugador Jugador { get; set; } // Relación con la entidad Empleado


        public string? estado { get; set; } = "activo";  // Valor por defecto 'activo'
    }
}
