using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace prueba.Models
{
    [Table("empleado")] // Especifica el nombre exacto de la tabla en la base de datos

    public class Empleado
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
        public ICollection<AperturaCaja> AperturasCaja { get; set; }
    }
}
