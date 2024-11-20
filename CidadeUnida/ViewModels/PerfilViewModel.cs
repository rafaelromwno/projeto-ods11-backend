using System.ComponentModel.DataAnnotations;

namespace CidadeUnida.ViewModels
{
    public class PerfilViewModel
    {

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }

        [Required(ErrorMessage = "Campo Senha é obrigatório!", AllowEmptyStrings = false)]
        public string Senha { get; set; }

        [Required(ErrorMessage = "Campo Confirmar Senha é obrigatório!", AllowEmptyStrings = false)]
        public string ConfirmarSenha { get; set; }

    }
}
