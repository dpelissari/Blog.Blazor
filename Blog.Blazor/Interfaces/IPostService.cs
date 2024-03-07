using Blog.Blazor.Models;
using System.Diagnostics;

namespace Blog.Blazor.Interfaces
{
    public interface IPostService
    {
        // assinatura dos metodos a serem implementados pela classe PostService
        Task Adicionar(Post Post);
        Task Atualizar(Post Post);
        Task Apagar(Post Post);
        Task<Post> BuscarPor(Guid id);
        Task<IQueryable<Post>> BuscarPorAutorId(Guid id);
        Task<IQueryable<Post>> BuscarPorIdCategoria(Guid id);
        Task<IQueryable<Post>> BuscarTodos();
    }
}
