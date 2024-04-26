using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace Blog.Blazor.Models
{
    public class Categoria
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        
        [MaxLength(250)]
        [Required(ErrorMessage = "O campo nome é obrigatório")]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "O campo descrição é obrigatório")]
        [MaxLength(500, ErrorMessage = "A descrição deve ter no máximo 500 caracteres")]
        public string? Descricao { get; set; }

        [Required(ErrorMessage = "O campo data de cadastro é obrigatório")]
        public DateTime Cadastro { get; set; } = DateTime.Now;

        public ICollection<Post>? Posts { get; set; }

        [Required(ErrorMessage = "O campo descrição SEO é obrigatório")]
        [MaxLength(250)]
        public string DescricaoSEO { get; set; }

        [Required(ErrorMessage = "O campo descrição SEO é obrigatório")]
        [MaxLength(150)]
        public string TituloPaginaSEO { get; set; }

        [Required(ErrorMessage = "O campo exibir item no menu é obrigatório")]
        public bool ExibirItemMenu { get; set; } = true;

        [Required(ErrorMessage = "O campo ativo é obrigatório")]
        public bool Ativo { get; set; } = true;
    }
}
