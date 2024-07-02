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
        /// Obtem todas categorias
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Categoria>> BuscarTodas()
        {
            return await dbContexto.Categoria.ToListAsync();
        }
    }
}
