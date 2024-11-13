using CidadeUnida.Models;

namespace CidadeUnida.Repositories.ADO.SQL_Server
{
    public interface IDenunciaDAO
    {
        Task<int> InserirDenuncia(Denuncia denuncia);
        Task<bool> AtualizarDenuncia(Denuncia denuncia);
        Task<bool> DeletarDenuncia(int id);
        Task<Denuncia> ObterDenunciaPorId(int id);
        Task<List<Denuncia>> ListarTodasDenuncias();
    }
}
