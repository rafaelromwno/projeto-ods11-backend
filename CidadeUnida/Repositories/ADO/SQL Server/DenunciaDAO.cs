using CidadeUnida.Models.Enums;
using CidadeUnida.Models;
using Microsoft.Data.SqlClient;

namespace CidadeUnida.Repositories.ADO.SQL_Server
{
    public class DenunciaDAO : IDenunciaDAO
    {
        private readonly string connectionString;

        public DenunciaDAO(string _connectionString)
        {
            connectionString = _connectionString;
        }

        // 1. Método Listar Todos: Retornar todas as denúncias na tabela Denuncia.
        public List<Denuncia> GetAll()
        {
            List<Denuncia> denuncias = new List<Denuncia>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "SELECT id_denuncia, descricao, status_denuncia, categoria, rua, numero, bairro, cidade, estado, cep, url_imagem, is_anonimo, data_envio, ativo FROM tb_denuncia;";

                    SqlDataReader dr = command.ExecuteReader();

                    while (dr.Read())
                    {
                        Denuncia denuncia = new Denuncia
                        {
                            IdDenuncia = (int)dr["id_denuncia"],
                            Descricao = dr["descricao"].ToString(),
                            Status = (StatusDenuncia)dr["status_denuncia"],
                            Categoria = (CategoriaDenuncia)dr["categoria"],
                            Rua = dr["rua"].ToString(),
                            Numero = dr["numero"].ToString(),
                            Bairro = dr["bairro"].ToString(),
                            Cidade = dr["cidade"].ToString(),
                            Estado = dr["estado"].ToString(),
                            Cep = dr["cep"].ToString(),
                            UrlImagem = dr["url_imagem"].ToString(),
                            IsAnonimo = (bool)dr["is_anonimo"],
                            DataEnvio = (DateTime)dr["data_envio"],
                            Ativo = (bool)dr["ativo"]
                        };

                        denuncias.Add(denuncia);
                    }
                }
            }
            return denuncias;
        }

        // Método para buscar uma denúncia pelo ID
        public Denuncia GetByIdDenuncia(int id)
        {
            Denuncia denuncia = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "SELECT id_denuncia, descricao, status_denuncia, categoria, rua, numero, bairro, cidade, estado, cep, url_imagem, is_anonimo, data_envio, ativo FROM tb_denuncia WHERE id_denuncia = @id;";
                    command.Parameters.Add(new SqlParameter("@id", System.Data.SqlDbType.Int)).Value = id;

                    SqlDataReader dr = command.ExecuteReader();

                    if (dr.Read())
                    {
                        denuncia = new Denuncia
                        {
                            IdDenuncia = (int)dr["id_denuncia"],
                            Descricao = dr["descricao"].ToString(),
                            Status = (StatusDenuncia)dr["status_denuncia"],
                            Categoria = (CategoriaDenuncia)dr["categoria"],
                            Rua = dr["rua"].ToString(),
                            Numero = dr["numero"].ToString(),
                            Bairro = dr["bairro"].ToString(),
                            Cidade = dr["cidade"].ToString(),
                            Estado = dr["estado"].ToString(),
                            Cep = dr["cep"].ToString(),
                            UrlImagem = dr["url_imagem"].ToString(),
                            IsAnonimo = (bool)dr["is_anonimo"],
                            DataEnvio = (DateTime)dr["data_envio"],
                            Ativo = (bool)dr["ativo"]
                        };
                    }
                }
            }
            return denuncia;
        }

        // Método para atualizar uma denúncia
        public void Update(int id, Denuncia denuncia)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "UPDATE tb_denuncia SET descricao = @Descricao, status_denuncia = @Status, categoria = @Categoria, rua = @Rua, numero = @Numero, bairro = @Bairro, cidade = @Cidade, estado = @Estado, cep = @Cep, url_imagem = @UrlImagem, is_anonimo = @IsAnonimo, data_envio = @DataEnvio, ativo = @Ativo WHERE id_denuncia = @IdDenuncia;";
                    command.Parameters.AddWithValue("@Descricao", denuncia.Descricao);
                    command.Parameters.AddWithValue("@Status", (int)denuncia.Status);
                    command.Parameters.AddWithValue("@Categoria", (int)denuncia.Categoria);
                    command.Parameters.AddWithValue("@Rua", denuncia.Rua);
                    command.Parameters.AddWithValue("@Numero", denuncia.Numero);
                    command.Parameters.AddWithValue("@Bairro", denuncia.Bairro);
                    command.Parameters.AddWithValue("@Cidade", denuncia.Cidade);
                    command.Parameters.AddWithValue("@Estado", denuncia.Estado);
                    command.Parameters.AddWithValue("@Cep", denuncia.Cep);
                    command.Parameters.AddWithValue("@UrlImagem", (object)denuncia.UrlImagem ?? DBNull.Value);
                    command.Parameters.AddWithValue("@IsAnonimo", denuncia.IsAnonimo);
                    command.Parameters.AddWithValue("@DataEnvio", denuncia.DataEnvio);
                    command.Parameters.AddWithValue("@Ativo", denuncia.Ativo);
                    command.Parameters.AddWithValue("@IdDenuncia", id);

                    command.ExecuteNonQuery();
                }
            }
        }

        // Método para adicionar uma nova denúncia
        public void Add(Denuncia denuncia)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "INSERT INTO tb_denuncia (descricao, categoria, rua, numero, bairro, cidade, estado, cep, url_imagem, is_anonimo, data_envio, ativo) VALUES (@Descricao, @Categoria, @Rua, @Numero, @Bairro, @Cidade, @Estado, @Cep, @UrlImagem, @IsAnonimo, @DataEnvio, @Ativo); SELECT convert(int,@@identity) as IdDenuncia;";

                    command.Parameters.AddWithValue("@Descricao", denuncia.Descricao);
                    command.Parameters.AddWithValue("@Status", (int)denuncia.Status);
                    command.Parameters.AddWithValue("@Categoria", (int)denuncia.Categoria);
                    command.Parameters.AddWithValue("@Rua", denuncia.Rua);
                    command.Parameters.AddWithValue("@Numero", denuncia.Numero);
                    command.Parameters.AddWithValue("@Bairro", denuncia.Bairro);
                    command.Parameters.AddWithValue("@Cidade", denuncia.Cidade);
                    command.Parameters.AddWithValue("@Estado", denuncia.Estado);
                    command.Parameters.AddWithValue("@Cep", denuncia.Cep);
                    command.Parameters.AddWithValue("@UrlImagem", (object)denuncia.UrlImagem ?? DBNull.Value);
                    command.Parameters.AddWithValue("@IsAnonimo", denuncia.IsAnonimo);
                    command.Parameters.AddWithValue("@DataEnvio", denuncia.DataEnvio);
                    command.Parameters.AddWithValue("@Ativo", denuncia.Ativo);

                    denuncia.IdDenuncia = (int)command.ExecuteScalar();
                }
            }
        }

        // Método para deletar uma denúncia
        public void Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "DELETE FROM tb_denuncia WHERE id_denuncia = @IdDenuncia;";
                    command.Parameters.AddWithValue("@IdDenuncia", id);

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
