using System.ComponentModel.DataAnnotations;

namespace Blog.Blazor.Models
{
    public class UsuarioLogin
    {
        [Required(ErrorMessage = "E-mail é obrigatório.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Senha é obrigatório.")]
        public string Senha { get; set; }
    }
}
