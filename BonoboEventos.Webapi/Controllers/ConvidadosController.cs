using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Bonobo.Model;
using Bonobo.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace BonoboEventos.Webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConvidadosController : ControllerBase
    {
        private readonly ConvidadoRepositorio _repositorio;
        public ConvidadosController(ConvidadoRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        [HttpGet]
        public List<ConvidadoModel> Get()
        {
            var dtConvidados = _repositorio.SelecionaConvidados();

            var listaConvidados = new List<ConvidadoModel>();

            foreach(DataRow linha in dtConvidados.Rows)
            {
                var convidado = new ConvidadoModel
                {
                    Apelido = linha["Apelido"].ToString(),
                    DataDeNascimento = Convert.ToDateTime(linha["DataDeNascimento"]),
                    Nome = linha["Nome"].ToString()
                };

                listaConvidados.Add(convidado);
            }

            return listaConvidados;
        }

        [HttpGet("{id}")]
        public ConvidadoModel Get(int id)
        {
            return _repositorio.SelecionaConvidados(id);
        }

        [HttpGet("Busca/{pesquisa}")]
        public List<ConvidadoModel> Get(string pesquisa)
        {
            var listaConvidados = new List<ConvidadoModel>();

            try
            {
                var dtConvidados = _repositorio.SelecionaConvidados(pesquisa);

                foreach (DataRow linha in dtConvidados.Rows)
                {
                    var convidado = new ConvidadoModel
                    {
                        Apelido = linha["Apelido"].ToString(),
                        DataDeNascimento = Convert.ToDateTime(linha["DataDeNascimento"]),
                        Nome = linha["Nome"].ToString()
                    };

                    listaConvidados.Add(convidado);
                }

                return listaConvidados; 
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        public string Post(ConvidadoModel model)
        {
            var aviso = "";
            try
            {
                 _repositorio.Insere(model);
                 aviso = "Convidado inserido com sucesso";
            }
            catch (System.Exception ex)
            {
                aviso = ex.Message;
            }

            return aviso;
            
        }

        [HttpPut("{id}")]
        public string Put(int id, ConvidadoModel model)
        {
            var aviso = "";
            try
            {
                var convidado = _repositorio.SelecionaConvidados(id);

                if(convidado.Id == 0)
                {
                    return "Contato não encontrado!";
                }

                _repositorio.Altera(model);

                aviso = "Convidado alterado com sucesso!";
            }
            catch (System.Exception ex)
            {
                aviso = ex.Message;
            }

            return aviso;
            
        }

        [HttpDelete("{id}")]
        public string Delete(int id)
        {
            var aviso = "";
            try
            {
                var convidado = _repositorio.SelecionaConvidados(id);

                if(convidado.Id == 0)
                {
                    return "Convidado não encontrado!";
                }
                _repositorio.Apaga(id);
                aviso = "Convidado removido com sucesso!";
            }
            catch (System.Exception ex)
            {
                aviso = ex.Message;
            }

            return aviso;
        }
    }
}