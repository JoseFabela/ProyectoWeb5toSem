using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace prueba.Models
{
    [Table("licencias")]
    public class Licencias
    {
        [Key]
        public int id { get; set; }
        public string tipo { get; set; }
        public DateTime fecha_emision { get; set; }
        public DateTime fecha_vencimiento { get; set; }
        public string? estado { get; set; } = "activo";  // Valor por defecto 'activo'

    }
}
