using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace prueba.Models
{
    [Table("oferta_publicitaria")]

    public class OfertaPublicitaria
    {
        [Key]
        public int id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion {  get; set; }

        [Required(ErrorMessage = "La fecha es obligatoria.")]
        [DataType(DataType.DateTime)]
        public DateTime? fecha_inicio { get; set; }
        public DateTime? fecha_fin { get; set; }

        public string? estado { get; set; } = "activo";  // Valor por defecto 'activo

    }
}
