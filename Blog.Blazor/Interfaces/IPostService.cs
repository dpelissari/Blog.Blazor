using Blog.Blazor.Models;
using System.Diagnostics;

namespace Blog.Blazor.Interfaces
{
    public interface IPostService
    {
        Task Adicionar(Post Post);
        Task Atualizar(Post Post);
        Task Apagar(Post Post);
        Task<IEnumerable<Post>> BuscarTodosPosts();
        Task<Post> BuscarPorIdPost(Guid id);
        Task<IEnumerable<Post>> BuscarPostsPorIdAutor(Guid id);
        Task<IEnumerable<Post>> BuscarPostsPorIdCategoria(Guid id);
        Task<Dictionary<string, List<Post>>> BuscarPostsAgrupadosPorCategoria(int paginaAtual, int categoriasPorPagina);
        Task<IEnumerable<Post>> BuscarPostsPaginados(int paginaAtual, int itensPorPagina);
        Task<IEnumerable<Post>> BuscarPostsPorTexto(string texto);
        Task<IEnumerable<Post>> BuscarPostsPorTextoPaginado(string texto, int paginaAtual, int itensPorPagina);
        Task<int> ObterTotalDePosts();
        Task<int> ObterTotalDeCategorias();
        Task<int> ObterTotalDePostsPorTexto(string texto);
    }
}
