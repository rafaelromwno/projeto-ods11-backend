using CidadeUnida.Models;

namespace CidadeUnida.Repositories.ADO.SQL_Server
{
    public interface IDenunciaDAO
    {
        Task<Denuncia> InserirDenuncia(Denuncia denuncia);
        Task<Denuncia> AtualizarDenuncia(Denuncia denuncia);
        Task<bool> DeletarDenuncia(int id);
        Task<Denuncia> ObterDenunciaPorId(int id);
        Task<List<Denuncia>> ListarTodasDenuncias();
    }
}
