using CidadeUnida.Models;

namespace CidadeUnida.Repositories.ADO.SQL_Server
{
    public interface IRealizaDenunciaDAO
    {
        Task<int> InserirRealizaDenuncia(RealizaDenuncia denuncia);
        Task<RealizaDenuncia> ObterRealizaDenunciaPorId(int idUsuario, int idDenuncia);
        Task<bool> AtualizarRealizaDenuncia(RealizaDenuncia denuncia);
        Task<bool> ExcluirRealizaDenuncia(int idUsuario, int idDenuncia);
        Task<List<RealizaDenuncia>> ObterTodosRealizaDenuncias();
    }
}
