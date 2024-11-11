using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CidadeUnida.Models
{
    public class Telefones
    {
        [ForeignKey("Usuario")]
        public int IdUsuario { get; set; }

        [Required]
        [StringLength(15)]
        public string NumeroTelefone { get; set; }

        public Usuario Usuario { get; set; }
    }
}
