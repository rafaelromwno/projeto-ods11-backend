using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace CidadeUnida.Models
{
    public class Contato
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("ID")]
        public int IdContato { get; set; }

        [Required]
        [StringLength(100)]
        [DisplayName("Nome")]
        public string NomeRemetente { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        [DisplayName("Email")]
        public string EmailRemetente { get; set; }

        [Required]
        public string Mensagem { get; set; }

        [DisplayName("Data De Envio")]
        public DateTime DataEnvio { get; set; } = DateTime.Now;

        public ICollection<FazContato> FazContatos { get; set; }
    }
}
