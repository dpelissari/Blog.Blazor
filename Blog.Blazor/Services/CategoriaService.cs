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

        // metodo asincrono para adicionar categoria
        public async Task Adicionar(Categoria categoria)
        {
            await dbContexto.AddAsync(categoria);
            await dbContexto.SaveChangesAsync();
        }

        // metodo asincrono para atualizar categoria
        public async Task Atualizar(Categoria categoria)
        {
            dbContexto.Update(categoria);
            await dbContexto.SaveChangesAsync();
        }

        // metodo asincrono para apagar categoria
        public async Task Apagar(Categoria categoria)
        {

            // remover as frases associadas a categoria
            var categorias = await dbContexto.Categoria.Where(f => f.Id == categoria.Id).ToListAsync();
            dbContexto.Categoria.RemoveRange(categorias);

            // Remover a categoria
            dbContexto.Remove(categoria);
            await dbContexto.SaveChangesAsync();
        }

        // metodo para retornar um autor com base no id
        public async Task<Categoria> BuscarPor(Guid id)
        {
            return await dbContexto.Categoria.FirstOrDefaultAsync(f => f.Id == id);
        }

        // metodo para listar todos categoria
        public async Task<IEnumerable<Categoria>> BuscarTodas()
        {
            return await dbContexto.Categoria.ToListAsync();
        }
    }
}
