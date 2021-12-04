using System.ComponentModel.DataAnnotations;

namespace Bonobo.Model
{
    public class ContatoModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe o tipo do contato")]
        [MaxLength(50, ErrorMessage ="Máximo de 50 caracteres")]
        public string Tipo { get; set; }

        [Required(ErrorMessage = "Informe o contato")]
        [MaxLength(50, ErrorMessage ="Máximo de 50 caracteres")]
        public string Contato { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Informe o Convidado")]
        public int ConvidadoId { get; set; }
    }
}