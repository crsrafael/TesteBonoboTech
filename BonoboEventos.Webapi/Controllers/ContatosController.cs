using Bonobo.Model;
using BonoboEventos.Webapi.Repositorio;
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
            return _repositorio.SelecionaContato(id);
        }

        [HttpPost]
        public bool Post(ContatoModel model)
        {
            return _repositorio.Insere(model);
        }

        [HttpPut("{id}")]
        public bool Put(int id, ContatoModel model)
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