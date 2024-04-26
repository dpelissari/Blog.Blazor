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

        // metodo asincrono para adicionar post
        public async Task Adicionar(Post post)
        {
            await dbContexto.AddAsync(post);
            await dbContexto.SaveChangesAsync();
        }

        // metodo asincrono para atualizar post
        public async Task Atualizar(Post post)
        {
            post.UltimaAtualizacao = DateTime.Now;
            dbContexto.Update(post);
            await dbContexto.SaveChangesAsync();
        }

        // metodo asincrono para apagar posts
        public async Task Apagar(Post post)
        {
            dbContexto.Remove(post);
            await dbContexto.SaveChangesAsync();
        }

        // metodo para retornar um post com base no id
        public async Task<Post> BuscarPor(Guid id)
        {
            var post = await dbContexto.Post.FirstOrDefaultAsync(f => f.Id == id);
            return post;
        }

        // metodo para listar todos posts
        public async Task<IQueryable<Post>> BuscarTodos()
        {
            var post = await dbContexto.Post.ToListAsync();
            return post.AsQueryable();
        }

        // metodo para listar todos posts de um autor
        public async Task<IQueryable<Post>> BuscarPorAutorId(Guid autorId)
        {
            var posts = await dbContexto.Post
                .Where(f => f.Id == autorId)
                .ToListAsync();
            return posts.AsQueryable();
        }

        public async Task<IQueryable<Post>> BuscarPorIdCategoria(Guid categoriaId)
        {
            var post = await dbContexto.Post
                .Where(f => f.IdCategoria == categoriaId)
                .ToListAsync();

            return post.AsQueryable();
        }
    }
}
