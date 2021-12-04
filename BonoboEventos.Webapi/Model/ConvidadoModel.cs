using System;
using System.ComponentModel.DataAnnotations;

namespace Bonobo.Model
{
    public class ConvidadoModel
    {
        public int Id {get; set;}
        
        [Required(ErrorMessage = "Informe o nome do convidado")]
        [MaxLength(100, ErrorMessage = "Máximo de 100 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Informe o apelido do convidado")]
        [MaxLength(50, ErrorMessage = "Máximo de 50 caracteres")]
        public string Apelido { get; set; }

        [Required(ErrorMessage = "Informe data de nascimento do convidado")]
        [Display(Name = "Data de Nascimento")]
        [DataType(DataType.Date)]
        public DateTime DataDeNascimento { get; set; }
    }
}