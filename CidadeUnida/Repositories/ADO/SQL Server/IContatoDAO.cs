using CidadeUnida.Models;

namespace CidadeUnida.Repositories.ADO.SQL_Server
{
    public interface IContatoDAO
    {
        Task<int> InserirContato(Contato contato);
        Task<Contato> ObterContatoPorId(int idContato);
        Task<bool> AtualizarContato(Contato contato);
        Task<bool> ExcluirContato(int idContato);
        Task<List<Contato>> ObterTodosContatos(); 
    }
}
