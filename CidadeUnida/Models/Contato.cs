using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CidadeUnida.Models
{
    public class Contato
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdContato { get; set; }

        [Required]
        [StringLength(100)]
        public string NomeRemetente { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string EmailRemetente { get; set; }

        [Required]
        public string Mensagem { get; set; }

        public DateTime DataEnvio { get; set; } = DateTime.Now;

        public ICollection<FazContato> FazContatos { get; set; }
    }
}
