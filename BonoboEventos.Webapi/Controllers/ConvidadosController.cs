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
            var dtConvidados = _repositorio.SelecionaConvidados(pesquisa);

            var listaConvidados = new List<ConvidadoModel>();

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

        [HttpPost]
        public bool Post(ConvidadoModel model)
        {
            return _repositorio.Insere(model);
        }

        [HttpPut("{id}")]
        public bool Put(int id, ConvidadoModel model)
        {
            return _repositorio.Altera(id, model);
        }

        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            return _repositorio.Apaga(id);
        }
    }
}