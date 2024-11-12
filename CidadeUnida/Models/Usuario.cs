using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CidadeUnida.Models
{
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdUsuario { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(15)]
        public string Telefone { get; set; }

        [Required]
        [StringLength(255)]
        public string Senha { get; set; }

        public bool IsAdm { get; set; } = false;

        public bool Ativo { get; set; } = true;

        public ICollection<Telefone> Telefones { get; set; }
        public ICollection<RealizaDenuncia> RealizaDenuncias { get; set; }
        public ICollection<FazContato> FazContatos { get; set; }
    }
}
