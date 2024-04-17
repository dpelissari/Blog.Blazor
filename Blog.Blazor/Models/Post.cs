using System.ComponentModel.DataAnnotations;

namespace Blog.Blazor.Models
{
    public class Post
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "O campo titulo é obrigatório")]
        [MaxLength(250)]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "O campo conteudo é obrigatório")]
        [MaxLength(500)]
        public string Conteudo { get; set; }

        [Required(ErrorMessage = "O campo data de cadastro é obrigatório")]
        public DateTime Cadastro { get; set; } = DateTime.Now;

        public DateTime UltimaAtualizacao { get; set; }

        [Required(ErrorMessage = "O campo descrição SEO é obrigatório")]
        [MaxLength(250)]
        public string DescricaoSEO { get; set; }

        [Required(ErrorMessage = "O campo descrição SEO é obrigatório")]
        [MaxLength(150)]
        public string TituloPaginaSEO { get; set; }

        [Required(ErrorMessage = "O campo imagem é obrigatório")]
        public string CaminhoImagem { get; set; }

        [Required(ErrorMessage = "O campo autor é obrigatório")]
        public Guid IdAutor { get; set; }

        public Autor Autor { get; set; }

        [Required(ErrorMessage = "O campo categoria é obrigatório")]
        public Guid IdCategoria { get; set; }

        public Categoria Categoria { get; set; }

        [Required(ErrorMessage = "O campo ativo é obrigatório")]
        public bool Ativo { get; set; } = true;
    }
}
