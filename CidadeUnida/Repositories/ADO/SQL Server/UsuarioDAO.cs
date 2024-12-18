﻿using CidadeUnida.Models;
using Microsoft.Data.SqlClient;

namespace CidadeUnida.Repositories.ADO.SQL_Server
{
    public class UsuarioDAO : IUsuarioDAO
    {
        private readonly string connectionString;

        public UsuarioDAO(string _connectionString)
        {
            connectionString = _connectionString;
        }

        // 1. Método Listar Todos: Retornar todos os usuários na tabela Usuario.
        public List<Usuario> GetAll()
        {
            List<Usuario> usuarios = new List<Usuario>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "SELECT id_usuario, nome, email, telefone, senha, is_adm, ativo FROM tb_usuario;";

                    SqlDataReader dr = command.ExecuteReader();

                    while (dr.Read())
                    {
                        Usuario usuario = new Usuario
                        {
                            IdUsuario = (int)dr["id_usuario"],
                            Nome = dr["nome"].ToString(),
                            Email = dr["email"].ToString(),
                            Telefone = dr["telefone"].ToString(),
                            Senha = dr["senha"].ToString(),
                            IsAdm = (bool)dr["is_adm"],
                            Ativo = (bool)dr["ativo"]
                        };

                        usuarios.Add(usuario);
                    }
                }
            }
            return usuarios;
        }

        // Método para buscar um usuário pelo ID
        public Usuario GetByIdUsuario(int id)
        {
            Usuario usuario = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "SELECT id_usuario, nome, email, telefone, senha, is_adm, ativo FROM tb_usuario WHERE id_usuario = @id;";
                    command.Parameters.Add(new SqlParameter("@id", System.Data.SqlDbType.Int)).Value = id;

                    SqlDataReader dr = command.ExecuteReader();

                    if (dr.Read())
                    {
                        usuario = new Usuario
                        {
                            IdUsuario = (int)dr["id_usuario"],
                            Nome = dr["nome"].ToString(),
                            Email = dr["email"].ToString(),
                            Telefone = dr["telefone"].ToString(),
                            Senha = dr["senha"].ToString(),
                            IsAdm = (bool)dr["is_adm"],
                            Ativo = (bool)dr["ativo"]
                        };
                    }
                }
            }
            return usuario;
        }

        // Método para atualizar um usuário
        public void Update(int id, Usuario usuario)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "UPDATE tb_usuario SET nome = @nome, email = @email, telefone = @telefone, senha = @senha, is_adm = @is_adm, ativo = @ativo WHERE id_usuario = @id_usuario;";
                    command.Parameters.AddWithValue("@nome", usuario.Nome);
                    command.Parameters.AddWithValue("@email", usuario.Email);
                    command.Parameters.AddWithValue("@telefone", usuario.Telefone);
                    command.Parameters.AddWithValue("@senha", usuario.Senha);
                    command.Parameters.AddWithValue("@is_adm", usuario.IsAdm);
                    command.Parameters.AddWithValue("@ativo", usuario.Ativo);
                    command.Parameters.AddWithValue("@id_usuario", id);

                    command.ExecuteNonQuery();
                }
            }
        }

        // Método para adicionar um novo usuário
        public void Add(Usuario usuario)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "INSERT INTO tb_usuario (nome, email, telefone, senha, is_adm, ativo) VALUES (@nome, @email, @telefone, @senha, @is_adm, @ativo); SELECT convert(int, @@identity) as IdUsuario;";

                    command.Parameters.AddWithValue("@nome", usuario.Nome);
                    command.Parameters.AddWithValue("@email", usuario.Email);
                    command.Parameters.AddWithValue("@telefone", usuario.Telefone);
                    command.Parameters.AddWithValue("@senha", usuario.Senha);
                    command.Parameters.AddWithValue("@is_adm", usuario.IsAdm);
                    command.Parameters.AddWithValue("@ativo", usuario.Ativo);

                    usuario.IdUsuario = (int)command.ExecuteScalar(); // Retorna o ID do novo usuário inserido
                }
            }
        }

        // Método para deletar um usuário
        public void Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "DELETE FROM tb_usuario WHERE id_usuario = @id_usuario;";
                    command.Parameters.AddWithValue("@id_usuario", id);

                    command.ExecuteNonQuery();
                }
            }
        }
    }

}

