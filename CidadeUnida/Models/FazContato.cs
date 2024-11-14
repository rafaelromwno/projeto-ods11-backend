using System.ComponentModel.DataAnnotations.Schema;

namespace CidadeUnida.Models
{
    public class FazContato
    {
        [ForeignKey("Usuario")]
        public int IdUsuario { get; set; }

        [ForeignKey("Contato")]
        public int IdContato { get; set; }
        public DateTime DataContato { get; set; }

        /*public Usuario Usuario { get; set; }
        public Contato Contato { get; set; }*/

    }
}
