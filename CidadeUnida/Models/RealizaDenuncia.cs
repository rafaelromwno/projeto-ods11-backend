using System.ComponentModel.DataAnnotations.Schema;

namespace CidadeUnida.Models
{
    public class RealizaDenuncia
    {
        [ForeignKey("Usuario")]
        public int IdUsuario { get; set; }

        [ForeignKey("Denuncia")]
        public int IdDenuncia { get; set; }

        public Usuario Usuario { get; set; }
        public Denuncia Denuncia { get; set; }
    }
}
