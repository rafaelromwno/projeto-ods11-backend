using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace CidadeUnida.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "O campo Email é obrigatório.")]
        [EmailAddress(ErrorMessage = "O Email não é válido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo Senha é obrigatório.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "A senha deve ter entre 6 e 100 caracteres.")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        [BindNever]
        public bool IsAdm { get; set; }

        [BindNever]
        public int IdUsuario { get; set; }
    }
}
