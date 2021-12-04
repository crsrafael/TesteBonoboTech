using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Bonobo.Model;

namespace Bonobo.Repositorio
{
    public class ConvidadoRepositorio
    {
        private readonly DatabaseConfig _dbConfig;

        public ConvidadoRepositorio(DatabaseConfig dbConfig)
        {
            _dbConfig = dbConfig;
        }

        public void Insere(ConvidadoModel convidado)
        {
            var convidadoExiste = ConvidadoExiste(convidado.Nome);

            if(convidadoExiste)
            {
                throw new Exception("Convidado já está cadastrado");
            }

            using (var conexao = new SqlConnection(_dbConfig.ConnectionString))
            {
                var cmd = new SqlCommand("sp_insere_convidado", conexao);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Nome", convidado.Nome);
                cmd.Parameters.AddWithValue("@Apelido", convidado.Apelido);
                cmd.Parameters.AddWithValue("@DataDeNascimento", convidado.DataDeNascimento);

                conexao.Open();

                cmd.ExecuteNonQuery();
            }
        }

        private bool ConvidadoExiste(string nome)
        {
            using (var conexao = new SqlConnection(_dbConfig.ConnectionString))
            {
                var sql = "Select Id From Convidados Where Nome = @nome";
                var cmd = new SqlCommand(sql, conexao);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@nome", nome);

                conexao.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                if(dr.Read())
                {
                    return true;
                }

                return false;
            }
        }

        public void Altera(int id, ConvidadoModel convidado)
        {
            using (var conexao = new SqlConnection(_dbConfig.ConnectionString))
            {
                var cmd = new SqlCommand("sp_altera_convidado", conexao);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@Nome", convidado.Nome);
                cmd.Parameters.AddWithValue("@Apelido", convidado.Apelido);
                cmd.Parameters.AddWithValue("@DataDeNascimento", convidado.DataDeNascimento);

                conexao.Open();

                cmd.ExecuteNonQuery();
            }
        }

        public void Apaga(int id)
        {
            using (var conexao = new SqlConnection(_dbConfig.ConnectionString))
            {
                var cmd = new SqlCommand("sp_remove_convidado", conexao);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);

                conexao.Open();

                cmd.ExecuteNonQuery();
            }
        }

        public ConvidadoModel SelecionaConvidados(int id)
        {
            using (var conexao = new SqlConnection(_dbConfig.ConnectionString))
            {
                var cmd = new SqlCommand("sp_seleciona_convidado", conexao);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);

                conexao.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                var convidado = new ConvidadoModel();

                if (dr.Read())
                {
                    convidado.Apelido = dr["Apelido"].ToString();
                    convidado.DataDeNascimento = Convert.ToDateTime(dr["DataDeNascimento"]);
                    convidado.Nome = dr["Nome"].ToString();
                }

                return convidado;
            }


        }

        public DataTable SelecionaConvidados(string busca = "")
        {
            var da = new SqlDataAdapter();
            var dt = new DataTable();
            using (var conexao = new SqlConnection(_dbConfig.ConnectionString))
            {
                da.SelectCommand = new SqlCommand("sp_seleciona_convidados", conexao);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@Pesquisa", busca);
                da.Fill(dt);
            }

            return dt;
        }
    }
}