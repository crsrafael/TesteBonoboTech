using Bonobo.Model;
using BonoboEventos.Webapi.Repositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BonoboEventos.Webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContatosController : ControllerBase
    {
        private readonly ContatoRepositorio _repositorio;

        public ContatosController(ContatoRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        [HttpGet("{id}")]
        public ContatoModel Get(int id)
        {
            try
            {
                 return _repositorio.SelecionaContato(id);
            }
            catch (System.Exception ex)
            {
                throw new System.Exception($"Erro ao localizar os contatos. {ex.Message}");
            }
        }

        [HttpPost]
        public string Post(ContatoModel model)
        {
            var aviso = "";

            try
            {
                _repositorio.Insere(model);
                aviso = "Contato inserido com sucesso!";
            }
            catch (System.Exception ex)
            {
                aviso = ex.Message;
            }

            return aviso;
            
        }

        [HttpPut("{id}")]
        public string Put(int id, ContatoModel model)
        {
            var aviso = "";
            try
            {
                 _repositorio.Altera(id, model);
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
            string aviso = "";

            try
            {
                _repositorio.Apaga(id);

                 aviso = "Contato removido com sucesso!";
            }
            catch (System.Exception ex)
            {
                aviso = ex.Message;
            }

            return aviso;
        }
    }
}