using Blog.Blazor.Models;

namespace Blog.Blazor.Interfaces
{
    public interface IUrlService
    {
        Task<ResultadoOperacao> VerificarUrl(string url, bool ehUrlCategoria);
    }
}
