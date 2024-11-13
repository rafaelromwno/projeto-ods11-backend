using CidadeUnida.Models;
using Microsoft.Data.SqlClient;

namespace CidadeUnida.Repositories.ADO.SQL_Server
{
    public interface IContatoDAO
    {
        // 1. Método Listar Todos: Retornar todos os contatos na tabela Contato.
        public List<Contato> GetAll();


        // Método para buscar um contato pelo ID
        public Contato GetByIdContato(int id);


        // Método para atualizar um contato
        public void Update(int id, Contato contato);


        // Método para adicionar um novo contato
        public void Add(Contato contato);


        // Método para deletar um contato
        public void Delete(int id);
        
    }
}
