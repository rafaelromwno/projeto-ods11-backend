using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using CidadeUnida.Models.Enums;
using System.ComponentModel;

namespace CidadeUnida.Models
{
    public class Denuncia
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("ID")]
        public int IdDenuncia { get; set; }

        [Required]
        [DisplayName("Descrição")]
        public string Descricao { get; set; }

        [Required]
        public StatusDenuncia Status { get; set; }  // Usando a enumeração para Status

        [Required]
        public CategoriaDenuncia Categoria { get; set; }  // Usando a enumeração para Categoria

        [Required]
        [StringLength(100)]
        public string Rua { get; set; }

        [Required]
        [StringLength(10)]
        [DisplayName("Número")]
        public string Numero { get; set; }

        [Required]
        [StringLength(50)]
        public string Bairro { get; set; }

        [Required]
        [StringLength(50)]
        public string Cidade { get; set; }

        [Required]
        [StringLength(2)]
        public string Estado { get; set; }

        [Required]
        [StringLength(10)]
        [DisplayName("CEP")]
        public string Cep { get; set; }

        [StringLength(255)]
        public string UrlImagem { get; set; }

        [DisplayName("É Anônimo?")]
        public bool IsAnonimo { get; set; } = false;

        [Required]
        [DisplayName("Data")]
        public DateTime DataEnvio { get; set; } = DateTime.Now;

        [DisplayName("Está ativo?")]
        public bool Ativo { get; set; } = true;
    }
}
