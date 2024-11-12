using CidadeUnida.Models;

namespace CidadeUnida.Repositories.ADO.SQL_Server
{
    public interface IUsuarioDAO
    {
        Task<int> InserirUsuario(Usuario usuario);
        Task<Usuario> ObterUsuarioPorId(int idUsuario);
        Task<bool> AtualizarUsuario(Usuario usuario);
        Task<bool> ExcluirUsuario(int idUsuario);
        Task<List<Usuario>> ObterTodosUsuarios();
    }
}
