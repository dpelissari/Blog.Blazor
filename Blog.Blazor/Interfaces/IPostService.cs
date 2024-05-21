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
        Task<IEnumerable<Post>> BuscarPorAutorId(Guid id);
        Task<IEnumerable<Post>> BuscarPorIdCategoria(Guid id);
        Task<IEnumerable<Post>> BuscarTodos();
        Task<Dictionary<string, List<Post>>> BuscarPostsAgrupadosPorCategoria(int paginaAtual, int categoriasPorPagina);
        Task<int> ObterTotalDeCategorias();
    }
}
