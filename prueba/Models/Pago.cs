using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace prueba.Models
{
    [Table("pago")]
    public class Pago
    {
        [Key]
        public int id { get; set; }

        [Required(ErrorMessage = "El jugador es obligatorio.")]
        [ForeignKey("jugador")]
        [Column("jugador_id")]
        public int jugadorId { get; set; }


        public DateTime? fecha { get; set; }
        public decimal monto { get; set; }
        public string tipo_pago { get; set; }

        public Jugador jugador { get; set; } // Relación con la entidad Jugador

        public string? estado { get; set; } = "activo";  // Valor por defecto 'activo'

    }
}
