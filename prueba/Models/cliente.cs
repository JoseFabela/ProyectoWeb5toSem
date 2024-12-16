using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace prueba.Models
{
    [Table("cliente")]

    public class cliente
    {
        [Key]
        public int id { get; set; }

        [StringLength(255)]
        public string? nombre { get; set; }

        [StringLength(255)]
        [EmailAddress(ErrorMessage = "El formato del email no es válido")]
        public string? email { get; set; }

        [StringLength(255)]
        public string? telefono { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Registro")]
        public DateTime? fecha_registro { get; set; }
        public string? estado { get; set; } = "activo";  // Valor por defecto 'activo'

    }
}
