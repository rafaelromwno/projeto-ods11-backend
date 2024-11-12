using CidadeUnida.Models;
using Microsoft.Data.SqlClient;

namespace CidadeUnida.Repositories.ADO.SQL_Server
{
    public class UsuarioDAO : IUsuarioDAO
    {

        private readonly string _connectionString;

        public UsuarioDAO(string connectionString)
        {
            _connectionString = connectionString;
        }

        // método para inserir um usuário
        public async Task<int> InserirUsuario(Usuario usuario)
        {
            string sql = @"INSERT INTO tb_usuario (nome, email, senha, is_adm, ativo, telefone)
                       VALUES (@Nome, @Email, @Senha, @IsAdm, @Ativo, @Telefone);
                       SELECT SCOPE_IDENTITY();";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@Nome", usuario.Nome);
                command.Parameters.AddWithValue("@Email", usuario.Email);
                command.Parameters.AddWithValue("@Senha", usuario.Senha);
                command.Parameters.AddWithValue("@IsAdm", usuario.IsAdm);
                command.Parameters.AddWithValue("@Ativo", usuario.Ativo);
                command.Parameters.AddWithValue("@Telefone", usuario.Telefone);

                await connection.OpenAsync();
                int novoId = Convert.ToInt32(await command.ExecuteScalarAsync());
                return novoId;
            }
        }

        // método para obter um usuário por ID
        public async Task<Usuario> ObterUsuarioPorId(int idUsuario)
        {
            string sql = @"SELECT id_usuario, nome, email, senha, is_adm, ativo, telefone 
                       FROM tb_usuario 
                       WHERE id_usuario = @IdUsuario";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@IdUsuario", idUsuario);
                await connection.OpenAsync();

                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new Usuario
                        {
                            IdUsuario = reader.GetInt32(0),
                            Nome = reader.GetString(1),
                            Email = reader.GetString(2),
                            Senha = reader.GetString(3),
                            IsAdm = reader.GetBoolean(4),
                            Ativo = reader.GetBoolean(5),
                            Telefone = reader.IsDBNull(6) ? null : reader.GetString(6) // Trata telefone nulo
                        };
                    }
                }
            }
            return null; // retorna nulo se o usuário não for encontrado
        }

        // método para atualizar um usuário
        public async Task<bool> AtualizarUsuario(Usuario usuario)
        {
            string sql = @"UPDATE tb_usuario
                       SET nome = @Nome,
                           email = @Email,
                           senha = @Senha,
                           is_adm = @IsAdm,
                           ativo = @Ativo,
                           telefone = @Telefone
                       WHERE id_usuario = @IdUsuario";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@IdUsuario", usuario.IdUsuario);
                command.Parameters.AddWithValue("@Nome", usuario.Nome);
                command.Parameters.AddWithValue("@Email", usuario.Email);
                command.Parameters.AddWithValue("@Senha", usuario.Senha);
                command.Parameters.AddWithValue("@IsAdm", usuario.IsAdm);
                command.Parameters.AddWithValue("@Ativo", usuario.Ativo);
                command.Parameters.AddWithValue("@Telefone", usuario.Telefone);

                await connection.OpenAsync();
                int linhasAfetadas = await command.ExecuteNonQueryAsync();
                return linhasAfetadas > 0;
            }
        }

        // método para excluir um usuário
        public async Task<bool> ExcluirUsuario(int idUsuario)
        {
            string sql = @"DELETE FROM tb_usuario WHERE id_usuario = @IdUsuario";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@IdUsuario", idUsuario);

                await connection.OpenAsync();
                int linhasAfetadas = await command.ExecuteNonQueryAsync();
                return linhasAfetadas > 0;
            }
        }

        // método para obter todos os usuários
        public async Task<List<Usuario>> ObterTodosUsuarios()
        {
            string sql = @"SELECT id_usuario, nome, email, senha, is_adm, ativo, telefone FROM tb_usuario";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                await connection.OpenAsync();

                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    List<Usuario> usuarios = new List<Usuario>();

                    while (await reader.ReadAsync())
                    {
                        usuarios.Add(new Usuario
                        {
                            IdUsuario = reader.GetInt32(0),
                            Nome = reader.GetString(1),
                            Email = reader.GetString(2),
                            Senha = reader.GetString(3),
                            IsAdm = reader.GetBoolean(4),
                            Ativo = reader.GetBoolean(5),
                            Telefone = reader.IsDBNull(6) ? null : reader.GetString(6) // trata telefone nulo
                        });
                    }

                    return usuarios;
                }
            }
        }
    }
}

