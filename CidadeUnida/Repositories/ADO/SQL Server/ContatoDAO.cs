
using CidadeUnida.Models;
using Microsoft.Data.SqlClient;

namespace CidadeUnida.Repositories.ADO.SQL_Server
{
    public class ContatoDAO : IContatoDAO
    {
        private readonly string connectionString;

        public ContatoDAO(string _connectionString)
        {
            connectionString = _connectionString;
        }

        // 1. Método Listar Todos: Retornar todos os contatos na tabela Contato.
        public List<Contato> GetAll()
        {
            List<Contato> contatos = new List<Contato>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "SELECT id_contato, nome_remetente, email_remetente, mensagem, data_envio FROM tb_contato;";

                    SqlDataReader dr = command.ExecuteReader();

                    while (dr.Read())
                    {
                        Contato contato = new Contato
                        {
                            IdContato = (int)dr["id_contato"],
                            NomeRemetente = dr["nome_remetente"].ToString(),
                            EmailRemetente = dr["email_remetente"].ToString(),
                            Mensagem = dr["mensagem"].ToString(),
                            DataEnvio = (DateTime)dr["data_envio"]
                        };

                        contatos.Add(contato);
                    }
                }
            }
            return contatos;
        }

        // Método para buscar um contato pelo ID
        public Contato GetByIdContato(int id)
        {
            Contato contato = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "SELECT id_contato, nome_remetente, email_remetente, mensagem, data_envio FROM tb_contato WHERE id_contato = @id;";
                    command.Parameters.Add(new SqlParameter("@id", System.Data.SqlDbType.Int)).Value = id;

                    SqlDataReader dr = command.ExecuteReader();

                    if (dr.Read())
                    {
                        contato = new Contato
                        {
                            IdContato = (int)dr["id_contato"],
                            NomeRemetente = dr["nome_remetente"].ToString(),
                            EmailRemetente = dr["email_remetente"].ToString(),
                            Mensagem = dr["mensagem"].ToString(),
                            DataEnvio = (DateTime)dr["data_envio"]
                        };
                    }
                }
            }
            return contato;
        }

        // Método para atualizar um contato
        public void Update(int id, Contato contato)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "UPDATE tb_contato SET nome_remetente = @nome_remetente, email_remetente = @email_remetente, mensagem = @mensagem, data_envio = @data_envio WHERE id_contato = @id_contato;";
                    command.Parameters.AddWithValue("@nome_remetente", contato.NomeRemetente);
                    command.Parameters.AddWithValue("@email_remetente", contato.EmailRemetente);
                    command.Parameters.AddWithValue("@mensagem", contato.Mensagem);
                    command.Parameters.AddWithValue("@data_envio", contato.DataEnvio);
                    command.Parameters.AddWithValue("@id_contato", id);

                    command.ExecuteNonQuery();
                }
            }
        }

        // Método para adicionar um novo contato
        public void Add(Contato contato)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "INSERT INTO tb_contato (nome_remetente, email_remetente, mensagem, data_envio) VALUES (@nome_remetente, @email_remetente, @mensagem, @data_envio); SELECT convert(int, @@identity) as IdContato;";

                    command.Parameters.AddWithValue("@nome_remetente", contato.NomeRemetente);
                    command.Parameters.AddWithValue("@email_remetente", contato.EmailRemetente);
                    command.Parameters.AddWithValue("@mensagem", contato.Mensagem);
                    command.Parameters.AddWithValue("@data_envio", contato.DataEnvio);

                    contato.IdContato = (int)command.ExecuteScalar(); // Retorna o ID do novo contato inserido
                }
            }
        }

        // Método para deletar um contato
        public void Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "DELETE FROM tb_contato WHERE id_contato = @id_contato;";
                    command.Parameters.AddWithValue("@id_contato", id);

                    command.ExecuteNonQuery();
                }
            }
        }


    }
}
