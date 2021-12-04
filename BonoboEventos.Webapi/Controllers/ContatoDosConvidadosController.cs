using System.Collections.Generic;
using System.Data;
using Bonobo.Model;
using BonoboEventos.Webapi.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace BonoboEventos.Webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContatosDoConvidadoController : ControllerBase
    {
        private readonly ContatoRepositorio _repositorio;

        public ContatosDoConvidadoController(ContatoRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        [HttpGet("{convidadoId}")]
        public List<ContatoModel> Get(int convidadoId)
        {
            var listaContatos = new List<ContatoModel>();

            try
            {
                 var dtContaos = _repositorio.SelecionaContatosDoConvidado(convidadoId);

                 foreach (DataRow linha in dtContaos.Rows)
                 {
                     var contato = new ContatoModel
                     {
                         Contato = linha["Contato"].ToString(),
                         Tipo = linha["Tipo"].ToString()
                     };

                     listaContatos.Add(contato);
                 }

                 return listaContatos;
            }
            catch (System.Exception ex)
            {
                throw new System.Exception($"Erro ao localizar os contatos do convidado. {ex.Message}");
            }
        }
    }
}