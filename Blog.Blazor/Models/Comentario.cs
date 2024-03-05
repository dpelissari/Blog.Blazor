using System.ComponentModel.DataAnnotations;

namespace Blog.Blazor.Models
{
    public class Comentario
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [MaxLength(250)]
        [Required(ErrorMessage = "O campo conteudo do comentário é obrigatório")]
        public string Conteudo { get; set; }

        [Required(ErrorMessage = "O campo autor é obrigatório")]
        public Autor Autor { get; set; }

        [Required(ErrorMessage = "O campo data de cadastro é obrigatório")]
        public DateTime Cadastro { get; set; } = DateTime.Now;

        public bool? Aprovado { get; set; }
    }
}
