using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace prueba.Models
{
    [Table("cliente_vip")]

    public class ClienteVip
    {
        [Key]
        public int id { get; set; }

        [Required(ErrorMessage = "El jugador es obligatorio.")]
        [ForeignKey("jugador")]
        [Column("jugador_id")]
        public int jugadorId { get; set; }


        [StringLength(255)]
        public string? nivel { get; set; }

        public Jugador jugador { get; set; } // Relación con la entidad Jugador

        public string? estado { get; set; } = "activo";  // Valor por defecto 'activo'

    }
}
