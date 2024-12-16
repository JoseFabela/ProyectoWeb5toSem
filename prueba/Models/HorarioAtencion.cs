using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace prueba.Models
{
    [Table("horario_atencion")]

    public class HorarioAtencion
    {
        [Key]
        public int id { get; set; }

        [StringLength(255)]
        public string? dia { get; set; }

        public DateTime? apertura { get; set; }
        public DateTime? cierre { get; set; }

        public string? estado { get; set; } = "activo";  // Valor por defecto 'activo'


    }
}
