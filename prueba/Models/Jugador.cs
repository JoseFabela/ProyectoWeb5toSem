using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace prueba.Models
{
    [Table("jugador")]

    public class Jugador
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("nombre")]
        public string Nombre { get; set; }

        [Required]
        [Column("estado")]
        public string Estado { get; set; }



        // Relación con AperturaCaja
        public ICollection<ClienteVip> ClienteVip { get; set; }
    }
}
