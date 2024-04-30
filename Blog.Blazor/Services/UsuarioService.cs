using Blog.Blazor.Components.Pages;
using Blog.Blazor.Data;
using Blog.Blazor.Interfaces;
using Blog.Blazor.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

namespace Blog.Blazor.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly AplicacaoDbContexto dbContexto;

        public UsuarioService(AplicacaoDbContexto appDbContexto)
        {
            dbContexto = appDbContexto;
        }

        public async Task Adicionar(Usuario usuario, string senha)
        {
            // Criptografar a senha antes de armazená-la no banco de dados
            usuario.SenhaHash = HashSenha(senha);

            await dbContexto.AddAsync(usuario);
            await dbContexto.SaveChangesAsync();
        }

        public async Task Atualizar(Usuario usuario)
        {
            dbContexto.Update(usuario);
            await dbContexto.SaveChangesAsync();
        }

        public async Task Apagar(Usuario usuario)
        {
            dbContexto.Remove(usuario);
            await dbContexto.SaveChangesAsync();
        }

        public async Task<Usuario> BuscarPor(Guid id)
        {
            var usuario = await dbContexto.Usuario.FirstOrDefaultAsync(f => f.Id == id);
            return usuario;
        }

        public async Task<IQueryable<Usuario>> BuscarTodos()
        {
            var usuario = await dbContexto.Usuario.ToListAsync();
            return usuario.AsQueryable();
        }

        // Método para criptografar a senha usando SHA256
        private string HashSenha(string senha)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(senha));
                var hashString = new StringBuilder();
                foreach (byte b in hashedBytes)
                {
                    hashString.Append(b.ToString("x2"));
                }
                return hashString.ToString();
            }
        }
    }
}
