using CidadeUnida.ViewModels;
using Microsoft.Data.SqlClient;

namespace CidadeUnida.Repositories.ADO.SQL_Server
{
    public class EditarPerfilDAO : IEditarPerfilDAO
    {

        private readonly string connectionString; // Declarado para toda a classe. Possível alterar somente no construtor.

        public EditarPerfilDAO(string connectionString)
        {
            this.connectionString = connectionString;
        }

        // Método para atualizar as informações do usuário no banco de dados.
        public bool AtualizarPerfil(PerfilViewModel perfil)
        {
            bool result = false;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Abrir conexão com o banco de dados.
                connection.Open();

                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "UPDATE tb_usuario SET nome = @nome, email = @email, telefone = @telefone, senha = @senha WHERE id_usuario = @id_usuario;";

                    command.Parameters.Add(new SqlParameter("@nome", System.Data.SqlDbType.VarChar)).Value = perfil.Nome;
                    command.Parameters.Add(new SqlParameter("@email", System.Data.SqlDbType.VarChar)).Value = perfil.Email;
                    command.Parameters.Add(new SqlParameter("@telefone", System.Data.SqlDbType.VarChar)).Value = perfil.Telefone;
                    command.Parameters.Add(new SqlParameter("@senha", System.Data.SqlDbType.VarChar)).Value = perfil.Senha;
                    command.Parameters.Add(new SqlParameter("@id_usuario", System.Data.SqlDbType.Int)).Value = perfil.Id;

                    // Executa o comando de atualização no banco de dados.
                    int rowsAffected = command.ExecuteNonQuery();

                    // Se a quantidade de linhas afetadas for maior que 0, significa que a atualização foi bem-sucedida.
                    result = rowsAffected > 0;
                }
            }

            return result;
        }

        // Método para buscar as informações atuais do perfil do usuário.
        public PerfilViewModel ObterPerfil(int idUsuario)
        {
            PerfilViewModel perfil = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Abrir conexão com o banco de dados.
                connection.Open();

                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "SELECT id_usuario, nome, email, telefone, senha FROM tb_usuario WHERE id_usuario = @id_usuario;";

                    command.Parameters.Add(new SqlParameter("@id_usuario", System.Data.SqlDbType.Int)).Value = idUsuario;

                    using (SqlDataReader dr = command.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            perfil = new PerfilViewModel
                            {
                                Id = (int)dr["id_usuario"],
                                Nome = dr["nome"].ToString(),
                                Email = dr["email"].ToString(),
                                Telefone = dr["telefone"].ToString(),
                                Senha = dr["senha"].ToString()
                            };
                        }
                    }
                }
            }

            return perfil;
        }
    }
}


