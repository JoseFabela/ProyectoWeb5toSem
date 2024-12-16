using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace prueba.Models
{
    [Table("cámara_seguridad")]

    public class CamaraSeguridad
    {
        [Key]
        public int id { get; set; }

        public string ubicacion { get; set; }  // Valor por defecto 'activo'


        public string? estado { get; set; } = "activo";  // Valor por defecto 'activo'
    }
}
