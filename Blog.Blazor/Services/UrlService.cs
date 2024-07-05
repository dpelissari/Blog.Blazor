using Blog.Blazor.Data;
using Blog.Blazor.Interfaces;
using Blog.Blazor.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Blazor.Services
{
    public class UrlService : IUrlService
    {
        private readonly PostService postService;
        private readonly CategoriaService categoriaService;

        // Construtor para injeção de dependência
        public UrlService(PostService postService, CategoriaService categoriaService)
        {
            this.postService = postService ?? throw new ArgumentNullException(nameof(postService));
            this.categoriaService = categoriaService ?? throw new ArgumentNullException(nameof(categoriaService));
        }

        public async Task<ResultadoOperacao>VerificarUrl(string urlPost, string urlCategoria, bool ehUrlCategoria)
        {
            if (!string.IsNullOrWhiteSpace(urlPost) || !string.IsNullOrWhiteSpace(urlCategoria))
            {
                string urlDesejada;
                HashSet<string> urlsExistentes = new HashSet<string>();

                // Obter URLs de categorias
                var urlsCategorias = await categoriaService.ObterUrlCategorias();
                urlsExistentes.UnionWith(urlsCategorias.Select(url => url.ToLower().Trim()));

                if (!ehUrlCategoria)
                {
                    // Ajusta a URL removendo espaços e deixando tudo minúsculo
                    urlDesejada = $"{urlCategoria.ToLower().Trim()}/{urlPost.ToLower().Replace(" ", "-").Trim()}";

                    // Obter URLs de posts
                    var urlsPosts = await postService.ObterUrlPosts();

                    // Compor a lista de URLs compostas por categoria e post
                    var urlsCompostas = urlsCategorias.SelectMany(categoria =>
                        urlsPosts.Select(post => $"{categoria}/{post.ToLower().Replace(" ", "-")}"));

                    // Adicionar URLs compostas à lista de URLs existentes
                    urlsExistentes.UnionWith(urlsCompostas.Select(url => url.ToLower().Trim()));
                }
                else
                {
                    // Ajusta a URL de categoria removendo espaços e deixando tudo minúsculo
                    urlDesejada = urlCategoria.ToLower().Replace(" ", "-").Trim();
                }

                // Verifica se a URL já foi usada
                if (urlsExistentes.Contains(urlDesejada))
                {
                    return new ResultadoOperacao
                    {
                        Sucesso = false,
                        Mensagem = "A URL já existe. Por favor, escolha outra URL."
                    };
                }
                else
                {
                    return new ResultadoOperacao
                    {
                        Sucesso = true,
                        Parametro = urlDesejada
                    };
                }
            }

            return new ResultadoOperacao
            {
                Sucesso = false,
                Mensagem = "A URL fornecida é inválida."
            };
        }
    }
}
