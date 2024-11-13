using CidadeUnida.Models;
using CidadeUnida.Models.Enums;
using Microsoft.Data.SqlClient;

namespace CidadeUnida.Repositories.ADO.SQL_Server
{
    public interface IDenunciaDAO
    {
        // 1. Método Listar Todos: Retornar todas as denúncias na tabela Denuncia.
        public List<Denuncia> GetAll();


        // Método para buscar uma denúncia pelo ID
        public Denuncia GetByIdDenuncia(int id);


        // Método para atualizar uma denúncia
        public void Update(int id, Denuncia denuncia);


        // Método para adicionar uma nova denúncia
        public void Add(Denuncia denuncia);


        // Método para deletar uma denúncia
        public void Delete(int id);
        
    }
}
