using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace prueba.Models
{
    [Table("sala_vip")]
    public class SalaVip

    {
        [Key]
        public int id { get; set; }

    
        public string nombre { get; set; }


        public int capacidad { get; set; }

        public string? estado { get; set; } = "activo";  // Valor por defecto 'activo'
    }
}
