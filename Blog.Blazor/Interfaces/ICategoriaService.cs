﻿using Blog.Blazor.Models;

namespace Blog.Blazor.Interfaces
{
    public interface ICategoriaService
    {
        // assinatura dos metodos a serem implementados pela classe Categoria
        Task Adicionar(Categoria categoria);
        Task Atualizar(Categoria categoria);
        Task Apagar(Categoria categoria);
        Task<Categoria> BuscarPorId(Guid id);
        Task<Categoria> BuscarPorUrl(string url);
        Task<IEnumerable<Categoria>> BuscarTodas();
    }
}
