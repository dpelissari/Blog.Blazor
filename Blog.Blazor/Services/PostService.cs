using Blog.Blazor.Data;
using Blog.Blazor.Interfaces;
using Blog.Blazor.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Blog.Blazor.Services
{
    public class PostService : IPostService
    {
        // injecao de depencia do dbcontext p/ armezar op em memory
        private readonly AplicacaoDbContexto dbContexto;

        public PostService(AplicacaoDbContexto appDbContexto)
        {
            dbContexto = appDbContexto;
        }

        /// <summary>
        /// Metodo para adicionar uma publicação/post
        /// </summary>
        /// <param name="post"></param>
        public async Task Adicionar(Post post)
        {

            var urlDisponivel = await VerificarUrl(post.Url);

            if (urlDisponivel.Sucesso)
            {

                // Adiciona o post ao contexto e salva as mudanças
                await dbContexto.AddAsync(post);
                await dbContexto.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Metodo para verificar se uma url já existe
        /// </summary>
        /// <param name="post"></param>
        public async Task<ResultadoOperacao> VerificarUrl(string url)
        {
            // ajusta a url
            var urlDesejada = url.Replace(" ", "-").Trim();

            // obtém todas as urls para comparação de forma assíncrona
            var urls = await ObterUrlPosts();

            // verifica se a url ja foi usada
            if (urls.Contains(urlDesejada))
            {
                return new ResultadoOperacao
                {
                    Sucesso = false,
                    Mensagem = "A URL já existe. Por favor, escolha outra URL.",
                };
            } else
            {
                return new ResultadoOperacao
                {
                    Sucesso = true,
                    Parametro = urlDesejada,
                };
            }
        }

        /// <summary>
        /// Metodo para atualizar uma publicação/post
        /// </summary>
        /// <param name="post"></param>
        public async Task Atualizar(Post post)
        {
            post.UltimaAtualizacao = DateTime.Now;
            dbContexto.Update(post);
            await dbContexto.SaveChangesAsync();
        }

        /// <summary>
        /// Metodo para excluir uma publicação/post
        /// </summary>
        /// <param name="post"></param>
        public async Task Apagar(Post post)
        {
            dbContexto.Remove(post);
            await dbContexto.SaveChangesAsync();
        }

        /// <summary>
        /// Obtem todas publicações/posts sem nenhum filtro
        /// </summary>
        public async Task<IEnumerable<Post>> BuscarTodosPosts()
        {
            return await dbContexto.Post.ToListAsync();
        }

        /// <summary>
        /// Obtem uma publicação/post pelo id
        /// </summary>
        /// <param name="id"></param>
        public async Task<Post> BuscarPorIdPost(Guid id)
        {
            return await dbContexto.Post.FirstOrDefaultAsync(f => f.Id == id);
        }


        /// <summary>
        /// Obtem uma publicacao com base na url
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<Post> BuscarPorUrlPost(string url)
        {
            return await dbContexto.Post.FirstOrDefaultAsync(f => f.Url == url);
        }

        /// <summary>
        /// Obtem todas publicações/posts de um autor
        /// </summary>
        /// <param name="autorId"></param>
        public async Task<IEnumerable<Post>> BuscarPostsPorIdAutor(Guid autorId)
        {
            return await dbContexto.Post.Where(f => f.Id == autorId).ToListAsync();
        }

        /// <summary>
        /// Obtem todas publicações/posts de uma categoria
        /// </summary>
        /// <param name="categoriaId"></param>
        public async Task<IEnumerable<Post>> BuscarPostsPorIdCategoria(Guid categoriaId)
        {
            return await dbContexto.Post.Where(f => f.IdCategoria == categoriaId).ToListAsync();
        }


        /// <summary>
        /// Obtem publicações/posts agrupadas por categoria
        /// </summary>
        /// <param name="paginaAtual"></param>
        /// <param name="categoriasPorPagina"></param>
        public async Task<Dictionary<string, List<Post>>> BuscarPostsAgrupadosPorCategoria(int paginaAtual, int categoriasPorPagina)
        {
            var todosPosts = await dbContexto.Post.Include(post => post.Categoria).OrderBy(post => post.Categoria.Nome).ToListAsync();
            var categoriasPaginadas = todosPosts.GroupBy(post => post.Categoria.Nome).Skip((paginaAtual - 1) * categoriasPorPagina).Take(categoriasPorPagina).ToDictionary(group => group.Key, group => group.ToList());
            return categoriasPaginadas;
        }

        /// <summary>
        /// Obtem publicações/posts de forma paginada
        /// </summary>
        /// <param name="paginaAtual"></param>
        /// <param name="itensPorPagina"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Post>> BuscarPostsPaginados(int paginaAtual, int itensPorPagina)
        {
            return await dbContexto.Post.Skip((paginaAtual - 1) * itensPorPagina).Take(itensPorPagina).ToListAsync();
        }

        /// <summary>
        /// Metodo para buscar publicações/posts que contenham um termo especificado
        /// </summary>
        /// <param name="texto"></param>
        public async Task<IEnumerable<Post>> BuscarPostsPorTexto(string texto)
        {
            return await dbContexto.Post
                .Where(p => EF.Functions.Like(p.Titulo, $"%{texto}%") || EF.Functions.Like(p.Conteudo, $"%{texto}%") || EF.Functions.Like(p.Categoria.Nome, $"%{texto}%"))
                .ToListAsync();
        }

        /// <summary>
        /// Metodo para buscar publicações/posts que contenham um termo especificado de forma paginada
        /// </summary>
        /// <param name="texto"></param>
        /// <param name="paginaAtual"></param>
        /// <param name="itensPorPagina"></param>
        public async Task<IEnumerable<Post>> BuscarPostsPorTextoPaginado(string texto, int paginaAtual, int itensPorPagina)
        {
            return await dbContexto.Post
                .Where(p => EF.Functions.Like(p.Titulo, $"%{texto}%") || EF.Functions.Like(p.Conteudo, $"%{texto}%") || EF.Functions.Like(p.Categoria.Nome, $"%{texto}%"))
                .Skip((paginaAtual - 1) * itensPorPagina)
                .Take(itensPorPagina)
                .ToListAsync();
        }

        /// <summary>
        /// Retorna o numero total de publicações/posts
        /// </summary>
        /// <returns></returns>
        public async Task<int> ObterTotalDePosts()
        {
            return await dbContexto.Post.CountAsync();
        }

        /// <summary>
        /// Retorna o total de categorias de publicações/posts
        /// </summary>
        /// <returns></returns>
        public async Task<int> ObterTotalDeCategorias()
        {
            var allPosts = await dbContexto.Post.Include(post => post.Categoria).ToListAsync();
            var groupedPosts = allPosts.GroupBy(post => post.Categoria.Nome).Count();
            return groupedPosts;
        }

        /// <summary>
        /// Retorna o total de posts correspondentes a um termo especificado
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        public async Task<int> ObterTotalDePostsPorTexto(string texto)
        {
            return await dbContexto.Post
                .Where(p => EF.Functions.Like(p.Titulo, $"%{texto}%") || EF.Functions.Like(p.Conteudo, $"%{texto}%"))
                .CountAsync();
        }

        public async Task<IEnumerable<string>> ObterUrlPosts()
        {
            return await dbContexto.Post.Select(post => post.Url).ToListAsync();
        }
    }
}