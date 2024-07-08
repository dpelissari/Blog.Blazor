using Blog.Blazor.Interfaces;

namespace Blog.Blazor.Services
{
    public class UrlService : IUrlService
    {
        private readonly PostService postService;
        private readonly CategoriaService categoriaService;

        // construtor para injeção de dependência
        public UrlService(PostService postService, CategoriaService categoriaService)
        {
            this.postService = postService ?? throw new ArgumentNullException(nameof(postService));
            this.categoriaService = categoriaService ?? throw new ArgumentNullException(nameof(categoriaService));
        }

        public async Task<ResultadoOperacao>VerificarUrl(string url, bool ehUrlCategoria)
        {
            if (!string.IsNullOrWhiteSpace(url))
            {
                // ajusta a url removendo espaços e deixando tudo minúsculo
                string urlDesejada = $"{url.ToLower().Replace(" ", "-").Trim().Normalize()}";

                if (!ehUrlCategoria)
                {
                    // obtem url de posts já existentes
                    var urlsPostsExistentes = await postService.ObterUrlPosts();

                    if (!urlsPostsExistentes.Contains(urlDesejada))
                        // retorna a url
                        return new ResultadoOperacao { Sucesso = true, Parametro = urlDesejada };
                    else
                        return new ResultadoOperacao { Sucesso = false, Mensagem = "A URL fornecida não está disponivel, pois já existe outra publicação com a mesma URL." };
                }
                else
                {
                    // obtem url existentes
                    var urlsCategoriasExistentes = await categoriaService.ObterUrlCategorias();

                    // se a url não existir
                    if (!urlsCategoriasExistentes.Contains(urlDesejada))
                        // retorna a url
                        return new ResultadoOperacao { Sucesso = true, Parametro = urlDesejada };
                    else
                        // retorna a mensagem que a url já existe
                        return new ResultadoOperacao { Sucesso = false, Mensagem = "A URL fornecida não está disponivel, pois já existe outra categoria com a mesma URL." };
                }
            }
            else
                return new ResultadoOperacao { Sucesso = false, Mensagem = "A URL fornecida é inválida" };
        }
    }
}