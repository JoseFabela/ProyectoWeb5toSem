using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace prueba.Models
{
    [Table("cliente_potencial")]

    public class ClientePotencial
    {
        [Key]
        public int id { get; set; }

        [StringLength(255)]
        public string? nombre { get; set; }

        [StringLength(255)]
        [EmailAddress(ErrorMessage = "El formato del email no es válido")]
        public string? email { get; set; }



        public string? estado { get; set; } = "activo";  // Valor por defecto 'activo'
    }
}
