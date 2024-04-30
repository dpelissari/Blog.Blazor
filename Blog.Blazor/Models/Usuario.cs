using Blog.Blazor.Enums;
using System.ComponentModel.DataAnnotations;

namespace Blog.Blazor.Models
{
    public class Usuario
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Email { get; set; }

        public TipoUsuario Tipo { get; set; }
        public string SenhaHash { get; set; }
    }
}
