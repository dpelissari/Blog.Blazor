using Blog.Blazor.Models;

namespace Blog.Blazor.Interfaces
{
    public interface ICategoriaService
    {
        // assinatura dos metodos a serem implementados pela classe Categoria
        Task Adicionar(Categoria categoria);
        Task Atualizar(Categoria categoria);
        Task Apagar(Categoria categoria);
        Task<Categoria> BuscarPor(Guid id);
        Task<IEnumerable<Categoria>> BuscarTodas();
    }
}
