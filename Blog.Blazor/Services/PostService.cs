﻿using Blog.Blazor.Data;
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
            return await dbContexto.Post.FirstOrDefaultAsync(f => f.Id == id);
        }

        // metodo para listar todos posts
        public async Task<IEnumerable<Post>> BuscarTodos()
        {
            return await dbContexto.Post.ToListAsync();
        }

        // metodo para listar todos posts de um autor
        public async Task<IEnumerable<Post>> BuscarPorAutorId(Guid autorId)
        {
            return await dbContexto.Post.Where(f => f.Id == autorId).ToListAsync();
        }

        public async Task<IEnumerable<Post>> BuscarPorIdCategoria(Guid categoriaId)
        {
            return await dbContexto.Post.Where(f => f.IdCategoria == categoriaId).ToListAsync();
        }

        public async Task<Dictionary<string, List<Post>>> BuscarPostsAgrupadosPorCategoria(int paginaAtual, int categoriasPorPagina)
        {
            var todosPosts = await dbContexto.Post.Include(post => post.Categoria).OrderBy(post => post.Categoria.Nome).ToListAsync();
            var categoriasPaginadas = todosPosts.GroupBy(post => post.Categoria.Nome).Skip((paginaAtual - 1) * categoriasPorPagina).Take(categoriasPorPagina).ToDictionary(group => group.Key, group => group.ToList());
            return categoriasPaginadas;
        }


        public async Task<IEnumerable<Post>> BuscarPostsPaginados(int paginaAtual, int itensPorPagina)
        {
            return await dbContexto.Post.Skip((paginaAtual - 1) * itensPorPagina).Take(itensPorPagina).ToListAsync();
        }

        public async Task<int> ObterTotalDeCategorias()
        {
            var allPosts = await dbContexto.Post.Include(post => post.Categoria).ToListAsync();
            var groupedPosts = allPosts.GroupBy(post => post.Categoria.Nome).Count();
            return groupedPosts;
        }

        // Método de busca por texto
        public async Task<IEnumerable<Post>> BuscarPorTexto(string texto)
        {
            return await dbContexto.Post
                .Where(p => EF.Functions.Like(p.Titulo, $"%{texto}%") || EF.Functions.Like(p.Conteudo, $"%{texto}%"))
                .ToListAsync();
        }

        public async Task<IEnumerable<Post>> BuscarPorTextoPaginado(string texto, int paginaAtual, int itensPorPagina)
        {
            return await dbContexto.Post
                .Where(p => EF.Functions.Like(p.Titulo, $"%{texto}%") || EF.Functions.Like(p.Conteudo, $"%{texto}%"))
                .Skip((paginaAtual - 1) * itensPorPagina)
                .Take(itensPorPagina)
                .ToListAsync();
        }

        public async Task<int> ObterTotalDePosts()
        {
            return await dbContexto.Post.CountAsync();
        }

    }
}
