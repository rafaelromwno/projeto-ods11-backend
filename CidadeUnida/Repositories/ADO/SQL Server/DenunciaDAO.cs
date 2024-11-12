using CidadeUnida.Models.Enums;
using CidadeUnida.Models;
using Microsoft.Data.SqlClient;

namespace CidadeUnida.Repositories.ADO.SQL_Server
{
    public class DenunciaDAO : IDenunciaDAO
    {
        private readonly string _connectionString;

        public DenunciaDAO(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Denuncia> InserirDenuncia(Denuncia denuncia)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string sql = @"INSERT INTO tb_denuncia (descricao, id_status, id_categoria, rua, numero, bairro, cidade, estado, cep, url_imagem, is_anonimo, data_envio, ativo)
                           VALUES (@descricao, @id_status, @id_categoria, @rua, @numero, @bairro, @cidade, @estado, @cep, @url_imagem, @is_anonimo, @data_envio, @ativo);
                           SELECT SCOPE_IDENTITY();";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@descricao", denuncia.Descricao);
                    command.Parameters.AddWithValue("@id_status", (int)denuncia.Status);
                    command.Parameters.AddWithValue("@id_categoria", (int)denuncia.Categoria);
                    command.Parameters.AddWithValue("@rua", denuncia.Rua);
                    command.Parameters.AddWithValue("@numero", denuncia.Numero);
                    command.Parameters.AddWithValue("@bairro", denuncia.Bairro);
                    command.Parameters.AddWithValue("@cidade", denuncia.Cidade);
                    command.Parameters.AddWithValue("@estado", denuncia.Estado);
                    command.Parameters.AddWithValue("@cep", denuncia.Cep);
                    command.Parameters.AddWithValue("@url_imagem", denuncia.UrlImagem ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@is_anonimo", denuncia.IsAnonimo);
                    command.Parameters.AddWithValue("@data_envio", denuncia.DataEnvio);
                    command.Parameters.AddWithValue("@ativo", denuncia.Ativo);

                    denuncia.IdDenuncia = Convert.ToInt32(await command.ExecuteScalarAsync());
                    return denuncia;
                }
            }
        }

        public async Task<Denuncia> AtualizarDenuncia(Denuncia denuncia)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string sql = @"UPDATE tb_denuncia SET descricao = @descricao, id_status = @id_status, id_categoria = @id_categoria, rua = @rua, numero = @numero, bairro = @bairro,
                           cidade = @cidade, estado = @estado, cep = @cep, url_imagem = @url_imagem, is_anonimo = @is_anonimo, ativo = @ativo WHERE id_denuncia = @id_denuncia";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@descricao", denuncia.Descricao);
                    command.Parameters.AddWithValue("@id_status", (int)denuncia.Status);
                    command.Parameters.AddWithValue("@id_categoria", (int)denuncia.Categoria);
                    command.Parameters.AddWithValue("@rua", denuncia.Rua);
                    command.Parameters.AddWithValue("@numero", denuncia.Numero);
                    command.Parameters.AddWithValue("@bairro", denuncia.Bairro);
                    command.Parameters.AddWithValue("@cidade", denuncia.Cidade);
                    command.Parameters.AddWithValue("@estado", denuncia.Estado);
                    command.Parameters.AddWithValue("@cep", denuncia.Cep);
                    command.Parameters.AddWithValue("@url_imagem", denuncia.UrlImagem ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@is_anonimo", denuncia.IsAnonimo);
                    command.Parameters.AddWithValue("@ativo", denuncia.Ativo);
                    command.Parameters.AddWithValue("@id_denuncia", denuncia.IdDenuncia);

                    await command.ExecuteNonQueryAsync();
                    return denuncia;
                }
            }
        }

        public async Task<bool> DeletarDenuncia(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string sql = "DELETE FROM tb_denuncia WHERE id_denuncia = @id_denuncia";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id_denuncia", id);
                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected > 0;
                }
            }
        }

        public async Task<Denuncia> ObterDenunciaPorId(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string sql = "SELECT * FROM tb_denuncia WHERE id_denuncia = @id_denuncia";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id_denuncia", id);
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return MapearDenuncia(reader);
                        }
                    }
                }
            }
            return null;
        }

        public async Task<List<Denuncia>> ListarTodasDenuncias()
        {
            var denuncias = new List<Denuncia>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string sql = "SELECT * FROM tb_denuncia";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            denuncias.Add(MapearDenuncia(reader));
                        }
                    }
                }
            }
            return denuncias;
        }

        private Denuncia MapearDenuncia(SqlDataReader reader)
        {
            return new Denuncia
            {
                IdDenuncia = Convert.ToInt32(reader["id_denuncia"]),
                Descricao = reader["descricao"].ToString(),
                Status = (StatusDenuncia)Convert.ToInt32(reader["id_status"]),
                Categoria = (CategoriaDenuncia)Convert.ToInt32(reader["id_categoria"]),
                Rua = reader["rua"].ToString(),
                Numero = reader["numero"].ToString(),
                Bairro = reader["bairro"].ToString(),
                Cidade = reader["cidade"].ToString(),
                Estado = reader["estado"].ToString(),
                Cep = reader["cep"].ToString(),
                UrlImagem = reader["url_imagem"] as string,
                IsAnonimo = Convert.ToBoolean(reader["is_anonimo"]),
                DataEnvio = Convert.ToDateTime(reader["data_envio"]),
                Ativo = Convert.ToBoolean(reader["ativo"])
            };
        }
    }
}
