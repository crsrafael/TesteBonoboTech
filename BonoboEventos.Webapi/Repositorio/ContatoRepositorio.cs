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
            var contatoExiste = ContatoExiste(contato);

            if(contatoExiste)
            {
                throw new Exception("Contato já está cadastrado.");
            }

            using(var conexao = new SqlConnection(_dbConfig.ConnectionString))
            {
                var cmd = new SqlCommand("sp_insere_contato", conexao);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue(@"Tipo", contato.Tipo);
                cmd.Parameters.AddWithValue(@"Contato", contato.Contato);
                cmd.Parameters.AddWithValue(@"ConvidadoId", contato.ConvidadoId);

                conexao.Open();

                cmd.ExecuteNonQuery(); 
            }
        }

        private bool ContatoExiste(ContatoModel contato)
        {
            using(var conexao = new SqlConnection(_dbConfig.ConnectionString))
            {
                var sql = "Select Id From Contatos where tipo = @tipo and contato = @contato";
                var cmd = new SqlCommand(sql, conexao);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@tipo", contato.Tipo);
                cmd.Parameters.AddWithValue("@contato", contato.Contato);
                
                conexao.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                if(dr.Read())
                {
                    return true;
                }

                return false;
            }
        }

        public void Altera(int id, ContatoModel contato)
        {
            using (var conexao = new SqlConnection(_dbConfig.ConnectionString))
            {
                var cmd = new SqlCommand("sp_altera_contato", conexao);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue(@"Tipo", contato.Tipo);
                cmd.Parameters.AddWithValue(@"Contato", contato.Contato);
                cmd.Parameters.AddWithValue(@"ConvidadoId", contato.ConvidadoId);

                conexao.Open();

                cmd.ExecuteNonQuery();
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
                    contato.Id = Convert.ToInt32(dr["Id"]); // Preciso do ID para validar a alteração e a exclusão;
                    contato.Tipo = dr["Tipo"].ToString();
                    contato.Contato = dr["Contato"].ToString();
                    contato.ConvidadoId = Convert.ToInt32(dr["ConvidadoId"]);
                }

                return contato;
            }
        }

        public DataTable SelecionaContatosDoConvidado(int convidadoId)
        {
            var da = new SqlDataAdapter();
            var dt = new DataTable();
            using(var conexao = new SqlConnection(_dbConfig.ConnectionString))
            {
                da.SelectCommand = new SqlCommand("sp_seleciona_contato_convidado", conexao);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@ConvidadoId", convidadoId);
                da.Fill(dt);
            }

            return dt;
        }
    }
}