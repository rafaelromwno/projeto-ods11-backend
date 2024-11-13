using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CidadeUnida.Models.Enums
{
    public enum StatusDenuncia
    {
        [Display(Name = "Pendente")]
        Pendente = 1,

        [Display(Name = "Em Andamento")]
        EmAndamento = 2,

        [Display(Name = "Resolvido")]
        Resolvido = 3,

        [Display(Name = "Arquivado")]
        Arquivado = 4
    }
}
