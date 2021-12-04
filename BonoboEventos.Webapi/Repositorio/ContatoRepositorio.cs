using System;
using System.Data;
using System.Data.SqlClient;
using Bonobo.Model;
using Bonobo.Repositorio;

namespace BonoboEventos.Webapi.Repositorio
{
    public class ContatoRepositorio
    {
        private readonly DatabaseConfig _dbConfig;
        public ContatoRepositorio(DatabaseConfig dbConfig)
        {
            _dbConfig = dbConfig;
        }

        public void Insere(ContatoModel contato)
        {
            using(var conexao = new SqlConnection(_dbConfig.ConnectionString))
            {
                var cmd = new SqlCommand("sp_insere_contato", conexao);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue(@"Tipo", contato.Tipo);
                cmd.Parameters.AddWithValue(@"Contato", contato.Contato);
                cmd.Parameters.AddWithValue(@"ConvidadoId", contato.ConvidadoId);

                conexao.Open();

                cmd.ExecuteNonQuery();

                return true; 
            }
        }

        public void Altera(int id, ContatoModel contato)
        {
            using (var conexao = new SqlConnection(_dbConfig.ConnectionString))
            {
                var cmd = new SqlCommand("sp_altera_contato", conexao);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", contato.Id);
                cmd.Parameters.AddWithValue(@"Tipo", contato.Tipo);
                cmd.Parameters.AddWithValue(@"Contato", contato.Contato);
                cmd.Parameters.AddWithValue(@"ConvidadoId", contato.ConvidadoId);


                conexao.Open();

                cmd.ExecuteNonQuery();

                return true;

            }
        }

        public void Apaga(int id)
        {
            using (var conexao = new SqlConnection(_dbConfig.ConnectionString))
            {
                var cmd = new SqlCommand("sp_remove_contato", conexao);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue(@"Id", id);

                conexao.Open();

                cmd.ExecuteNonQuery();

                return true;
            }
        }

        public ContatoModel SelecionaContato(int id)
        {
            using (var conexao = new SqlConnection(_dbConfig.ConnectionString))
            {
                var cmd = new SqlCommand("sp_seleciona_contato", conexao);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);

                conexao.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                var contato = new ContatoModel();

                if (dr.Read())
                {
                    contato.Tipo = dr["Tipo"].ToString();
                    contato.Contato = dr["Contato"].ToString();
                    contato.ConvidadoId = Convert.ToInt32(dr["ConvidadoId"]);
                }

                return contato;
            }
        }
    }
}