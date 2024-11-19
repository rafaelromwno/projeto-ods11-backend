using CidadeUnida.Models;
using CidadeUnida.ViewModels;

namespace CidadeUnida.Repositories.ADO.SQL_Server
{
    public interface ILoginDAO
    {
        // Criar o método de validação do Usuario do Usuário.
        public bool Check(LoginViewModel login);
        public LoginResultado GetTipo(LoginViewModel login);
    }
}

