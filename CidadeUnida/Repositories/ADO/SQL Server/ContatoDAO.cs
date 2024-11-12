
using CidadeUnida.Models;
using Microsoft.Data.SqlClient;

namespace CidadeUnida.Repositories.ADO.SQL_Server
{
    public class ContatoDAO : IContatoDAO
    {
        private readonly string _connectionString;

        public ContatoDAO(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<int> InserirContato(Contato contato)
        {
            string sql = @"INSERT INTO tb_contato (nome_remetente, email_remetente, mensagem, data_envio)
                       VALUES (@NomeRemetente, @EmailRemetente, @Mensagem, GETDATE());
                       SELECT SCOPE_IDENTITY();";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@NomeRemetente", contato.NomeRemetente);
                command.Parameters.AddWithValue("@EmailRemetente", contato.EmailRemetente);
                command.Parameters.AddWithValue("@Mensagem", contato.Mensagem);

                await connection.OpenAsync();
                int novoId = Convert.ToInt32(await command.ExecuteScalarAsync());
                return novoId;
            }
        }

        public async Task<Contato> ObterContatoPorId(int idContato)
        {
            string sql = @"SELECT id_contato, nome_remetente, email_remetente, mensagem, data_envio 
                       FROM tb_contato 
                       WHERE id_contato = @IdContato";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@IdContato", idContato);
                await connection.OpenAsync();

                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new Contato
                        {
                            IdContato = reader.GetInt32(0),
                            NomeRemetente = reader.GetString(1),
                            EmailRemetente = reader.GetString(2),
                            Mensagem = reader.GetString(3),
                            DataEnvio = reader.GetDateTime(4)
                        };
                    }
                }
            }
            return null; // retorna nulo se o contato não for encontrado
        }

        public async Task<bool> AtualizarContato(Contato contato)
        {
            string sql = @"UPDATE tb_contato
                       SET nome_remetente = @NomeRemetente,
                           email_remetente = @EmailRemetente,
                           mensagem = @Mensagem
                       WHERE id_contato = @IdContato";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@IdContato", contato.IdContato);
                command.Parameters.AddWithValue("@NomeRemetente", contato.NomeRemetente);
                command.Parameters.AddWithValue("@EmailRemetente", contato.EmailRemetente);
                command.Parameters.AddWithValue("@Mensagem", contato.Mensagem);

                await connection.OpenAsync();
                int linhasAfetadas = await command.ExecuteNonQueryAsync();
                return linhasAfetadas > 0;
            }
        }

        public async Task<bool> ExcluirContato(int idContato)
        {
            string sql = @"DELETE FROM tb_contato WHERE id_contato = @IdContato";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@IdContato", idContato);

                await connection.OpenAsync();
                int linhasAfetadas = await command.ExecuteNonQueryAsync();
                return linhasAfetadas > 0;
            }
        }
        public async Task<List<Contato>> ObterTodosContatos()
        {
            string sql = @"SELECT id_contato, nome_remetente, email_remetente, mensagem, data_envio FROM tb_contato";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                await connection.OpenAsync();

                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    List<Contato> contatos = new List<Contato>();

                    while (await reader.ReadAsync())
                    {
                        contatos.Add(new Contato
                        {
                            IdContato = reader.GetInt32(0),
                            NomeRemetente = reader.GetString(1),
                            EmailRemetente = reader.GetString(2),
                            Mensagem = reader.GetString(3),
                            DataEnvio = reader.GetDateTime(4)
                        });
                    }

                    return contatos;
                }
            }
        }
    }
}
