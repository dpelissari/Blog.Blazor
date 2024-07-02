using Blog.Blazor.Data;
using Blog.Blazor.Interfaces;
using Blog.Blazor.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Blog.Blazor.Services
{
    public class AutorService : IAutorService
    {
        // injecao de depencia do dbcontext p/ armezar op em memory
        private readonly AplicacaoDbContexto dbContexto;

        public AutorService(AplicacaoDbContexto appDbContexto)
        {
            dbContexto = appDbContexto;
        }

        /// <summary>
        /// Metodo asincrono para adicionar autores
        /// </summary>
        /// <param name="Autor"></param>
        /// <returns></returns>
        public async Task Adicionar(Autor Autor)
        {
            await dbContexto.AddAsync(Autor);
            await dbContexto.SaveChangesAsync();
        }

        /// <summary>
        /// Metodo asincrono para atualizar autores
        /// </summary>
        /// <param name="Autor"></param>
        /// <returns></returns>
        public async Task Atualizar(Autor Autor)
        {
            dbContexto.Update(Autor);
            await dbContexto.SaveChangesAsync();
        }

        /// <summary>
        /// Metodo asincrono para apagar autores
        /// </summary>
        /// <param name="Autor"></param>
        /// <returns></returns>
        public async Task Apagar(Autor Autor)
        {
            dbContexto.Remove(Autor);
            await dbContexto.SaveChangesAsync();
        }

        /// <summary>
        /// Obitem autores sem nenhum filtro
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Autor>> BuscarTodos()
        {
            return await dbContexto.Autor.ToListAsync();

        }

        /// <summary>
        /// Obtem autor pelo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Autor> BuscarPorId(Guid id)
        {
            var Autor = await dbContexto.Autor.FirstOrDefaultAsync(f => f.Id == id);
            return Autor;
        }
       
        /// <summary>
        /// Obtem autores de forma paginada
        /// </summary>
        /// <param name="paginaAtual"></param>
        /// <param name="itensPorPagina"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Autor>> BuscarAutoresPaginados(int paginaAtual, int itensPorPagina)
        {
            return await dbContexto.Autor.Skip((paginaAtual - 1) * itensPorPagina).Take(itensPorPagina).ToListAsync();
        }

        /// <summary>
        /// Obtem o numero total de autores
        /// </summary>
        /// <returns></returns>
        public async Task<int> ObterTotalDeAutores()
        {
            return await dbContexto.Autor.CountAsync();
        }
    }
}
