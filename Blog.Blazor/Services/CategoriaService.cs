using Blog.Blazor.Data;
using Blog.Blazor.Interfaces;
using Blog.Blazor.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace Blog.Blazor.Services
{
    public class CategoriaService : ICategoriaService
    {
        // injecao de depencia do dbcontext p/ armezar op em memory
        private readonly AplicacaoDbContexto dbContexto;

        public CategoriaService(AplicacaoDbContexto appDbContexto)
        {
            dbContexto = appDbContexto;
        }

        /// <summary>
        /// Metodo asincrono para adicionar categoria
        /// </summary>
        /// <param name="categoria"></param>
        /// <returns></returns>
        public async Task Adicionar(Categoria categoria)
        {
            await dbContexto.AddAsync(categoria);
            await dbContexto.SaveChangesAsync();
        }

        /// <summary>
        /// Metodo para verificar se uma url já existe
        /// </summary>
        /// <param name="post"></param>
        public async Task<ResultadoOperacao> VerificarUrl(string url)
        {
            // ajusta a url
            var urlDesejada = url.ToLower().Replace(" ", "-").Trim();

            // obtém todas as urls para comparação de forma assíncrona
            var urls = await ObterUrlCategorias();

            // verifica se a url ja foi usada
            if (urls.Contains(urlDesejada))
            {
                return new ResultadoOperacao
                {
                    Sucesso = false,
                    Mensagem = "A URL já existe. Por favor, escolha outra URL.",
                };
            }
            else
            {
                return new ResultadoOperacao
                {
                    Sucesso = true,
                    Parametro = urlDesejada,
                };
            }
        }

        /// <summary>
        /// Metodo asincrono para atualizar categoria
        /// </summary>
        /// <param name="categoria"></param>
        /// <returns></returns>
        public async Task Atualizar(Categoria categoria)
        {
            dbContexto.Update(categoria);
            await dbContexto.SaveChangesAsync();
        }

        /// <summary>
        /// Metodo asincrono para apagar categoria
        /// </summary>
        /// <param name="categoria"></param>
        /// <returns></returns>
        public async Task Apagar(Categoria categoria)
        {

            // remover as frases associadas a categoria
            var categorias = await dbContexto.Categoria.Where(f => f.Id == categoria.Id).ToListAsync();
            dbContexto.Categoria.RemoveRange(categorias);

            // Remover a categoria
            dbContexto.Remove(categoria);
            await dbContexto.SaveChangesAsync();
        }

        /// <summary>
        /// Obtem categoria pelo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Categoria> BuscarPorId(Guid id)
        {
            return await dbContexto.Categoria.FirstOrDefaultAsync(f => f.Id == id);
        }

        /// <summary>
        /// Obtem categoria com base na url
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Categoria> BuscarPorUrl(string url)
        {
            return await dbContexto.Categoria.FirstOrDefaultAsync(f => f.Url == url);
        }

        /// <summary>
        /// Obtem todas categorias
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Categoria>> BuscarTodas()
        {
            return await dbContexto.Categoria.ToListAsync();
        }


        public async Task<IEnumerable<string>> ObterUrlCategorias()
        {
            return await dbContexto.Categoria.Select(categoria => categoria.Url).ToListAsync();
        }
    }
}
