﻿using Blog.Blazor.Models;

namespace Blog.Blazor.Interfaces
{
    public interface IAutorService
    {
        // assinatura dos metodos a serem implementados pela classe Autor
        Task Adicionar(Autor autor);
        Task Atualizar(Autor autor);
        Task Apagar(Autor autor);
        Task<Autor> BuscarPor(Guid id);
        Task<IQueryable<Autor>> BuscarTodos();
    }
}