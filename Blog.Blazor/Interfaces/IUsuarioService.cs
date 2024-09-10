using Blog.Blazor.Models;

namespace Blog.Blazor.Interfaces
{
    public interface IUsuarioService
    {
        // assinatura dos metodos a serem implementados pela classe autor
        Task Adicionar(Usuario usuario, string senha);
        Task Atualizar(Usuario usuario);
        Task Apagar(Usuario usuario);
        Task<IEnumerable<Usuario>> BuscarTodos();
        Task<Usuario> BuscarUsuarioPorId(Guid id);
        Task<string> HashSenhaAsync(string senha);
    }
}
