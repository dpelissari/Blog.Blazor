using Blog.Blazor.Models;

namespace Blog.Blazor.Interfaces
{
    public interface IAutorService
    {
        // assinatura dos metodos a serem implementados pela classe Autor
        Task Adicionar(Autor autor);
        Task Atualizar(Autor autor);
        Task Apagar(Autor autor);
        Task<Autor> BuscarPor(Guid id);
        Task<IEnumerable<Autor>> BuscarTodos();
        Task<IEnumerable<Autor>> BuscarAutoresPaginados(int paginaAtual, int itensPorPagina);
        Task<int> ObterTotalDeAutores();
    }
}
