using CidadeUnida.Models;

namespace CidadeUnida.Repositories.ADO.SQL_Server
{
    public interface IFazContatoDAO
    {
        Task<int> InserirFazContato(FazContato contato);
        Task<FazContato> ObterFazContatoPorId(int idUsuario, int idContato);
        Task<bool> AtualizarFazContato(FazContato contato);
        Task<bool> ExcluirFazContato(int idUsuario, int idContato);
        Task<List<FazContato>> ObterTodosFazContatos();
    }
}
