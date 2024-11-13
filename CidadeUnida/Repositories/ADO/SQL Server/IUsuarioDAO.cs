using CidadeUnida.Models;
using Microsoft.Data.SqlClient;

namespace CidadeUnida.Repositories.ADO.SQL_Server
{
    public interface IUsuarioDAO
    {
        // 1. Método Listar Todos: Retornar todos os usuários na tabela Usuario.
        public List<Usuario> GetAll();


        // Método para buscar um usuário pelo ID
        public Usuario GetByIdUsuario(int id);


        // Método para atualizar um usuário
        public void Update(int id, Usuario usuario);


        // Método para adicionar um novo usuário
        public void Add(Usuario usuario);


        // Método para deletar um usuário
        public void Delete(int id);
        
    }
}
