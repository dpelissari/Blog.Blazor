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

        // metodo asincrono para adicionar autores
        public async Task Adicionar(Autor Autor)
        {
            await dbContexto.AddAsync(Autor);
            await dbContexto.SaveChangesAsync();
        }

        // metodo asincrono para atualizar autores
        public async Task Atualizar(Autor Autor)
        {
            dbContexto.Update(Autor);
            await dbContexto.SaveChangesAsync();
        }

        // metodo asincrono para apagar autores
        public async Task Apagar(Autor Autor)
        {
            dbContexto.Remove(Autor);
            await dbContexto.SaveChangesAsync();
        }

        // metodo para retornar uma frase com base no id
        public async Task<Autor> BuscarPor(Guid id)
        {
            var Autor = await dbContexto.Autor.FirstOrDefaultAsync(f => f.Id == id);
            return Autor;
        }

        // metodo para listar todas autores
        public async Task<IEnumerable<Autor>> BuscarTodos()
        {
            return await dbContexto.Autor.ToListAsync();

        }

        // metodo para listar todas autores de um autor
        public async Task<IEnumerable<Autor>> BuscarPorAutorId(Guid autorId)
        {
            return await dbContexto.Autor.Where(f => f.Id == autorId).ToListAsync();
        }

        public async Task<IEnumerable<Autor>> BuscarPorIdCategoria(Guid categoriaId)
        {
            return await dbContexto.Autor.Where(f => f.Id == categoriaId).ToListAsync();
        }
    }
}
