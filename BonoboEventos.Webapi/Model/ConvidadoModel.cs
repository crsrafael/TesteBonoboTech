using System;

namespace Bonobo.Model
{
    public class ConvidadoModel
    {
        int Id {get; set;}

        public string Nome { get; set; }

        public string Apelido { get; set; }

        public DateTime DataDeNascimento { get; set; }
    }
}