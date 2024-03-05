using System.ComponentModel.DataAnnotations;

namespace Blog.Blazor.Models
{
    public class Autor
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "O campo nome é obrigatório")]
        [MaxLength(250)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo data de cadastro é obrigatório")]
        public DateTime Cadastro { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "O campo imagem é obrigatório")]
        public string CaminhoImagem { get; set; }

        [Required(ErrorMessage = "O campo ativo é obrigatório")]
        public bool Ativo { get; set; } = true;
    }
}
