
using CidadeUnida.Models;
using CidadeUnida.ViewModels;
using Microsoft.Data.SqlClient;
using NuGet.Protocol.Plugins;

namespace CidadeUnida.Repositories.ADO.SQL_Server
{
    public class LoginDAO : ILoginDAO
    {
        private readonly string connectionString; //Declarado para toda a classe. Possível alterar somente no construtor.

        public LoginDAO(string connectionString)
        {
            this.connectionString = connectionString;
        }

        // Criar o método de validação do Usuario do Usuário.
        public bool Check(LoginViewModel login)
        {
            bool result = false;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Abrir conexão do banco de dados: CarroDB
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "SELECT id_usuario FROM tb_usuario WHERE email=@email AND senha=@senha;";
                    command.Parameters.Add(new SqlParameter("@email", System.Data.SqlDbType.VarChar)).Value = login.Email;
                    command.Parameters.Add(new SqlParameter("@senha", System.Data.SqlDbType.VarChar)).Value = login.Senha;

                    // Executa o comando da SQL: "SELECT".
                    SqlDataReader dr = command.ExecuteReader();

                    // Se encontrado o usuário, result é verdadeiro (result = true;),
                    // caso contrário, result se mantém como falso (result = false;).
                    result = dr.Read();
                }
                // retona o valor de result (true ou false).
            }
            return result;
        }

        public LoginResultado GetTipo(LoginViewModel login)
        {
            LoginResultado result = new LoginResultado();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Abrir conexão do banco de dados: CarroDB
                connection.Open();

                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "SELECT id_usuario, is_adm FROM tb_usuario WHERE email=@email AND senha=@senha";
                    command.Parameters.Add(new SqlParameter("@email", System.Data.SqlDbType.VarChar)).Value = login.Email;
                    command.Parameters.Add(new SqlParameter("@senha", System.Data.SqlDbType.VarChar)).Value = login.Senha;

                    using (SqlDataReader dr = command.ExecuteReader())
                    {
                        result.Sucesso = dr.Read();

                        if (result.Sucesso)
                        {
                            result.Id = (int)dr["id_usuario"];
                            result.IsAdm = (bool)dr["is_adm"];


                            login.IdUsuario = result.Id;
                            login.IsAdm = result.IsAdm;
                        }
                    }

                }
                //...executar códigos dentro da sessão durante o usuario do usuário efetuado com sucesso.
            }
            return result;
        }
    }
}
