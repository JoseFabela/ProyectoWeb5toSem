using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace prueba.Models
{
    [Table("equipo")]

    public class Equipo
    {
        [Key]
        public int id { get; set; }

        [StringLength(255)]
        public string? nombre { get; set; }
        public string categoria { get; set; }
        public string? estado { get; set; } = "activo";  // Valor por defecto 'activo'

    }
}
