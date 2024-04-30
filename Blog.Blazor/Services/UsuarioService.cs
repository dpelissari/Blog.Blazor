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
            usuario.SenhaHash = await HashSenhaAsync(senha);

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


        public async Task<string> HashSenhaAsync(string senha)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = await Task.Run(() => sha256.ComputeHash(Encoding.UTF8.GetBytes(senha)));
                var hashString = new StringBuilder();
                foreach (byte b in hashedBytes)
                {
                    hashString.Append(b.ToString("x2"));
                }
                return hashString.ToString();
            }
        }


        public async Task<Usuario> AutenticarUsuario(string email, string senha)
        {
            // Busca o usuário pelo e-mail
            var usuario = await dbContexto.Usuario.FirstOrDefaultAsync(u => u.Email == email);

            // Verifica se o usuário foi encontrado
            if (usuario != null)
            {

                // Compara o hash da senha fornecida com o hash armazenado no banco de dados
                if (usuario.SenhaHash.Equals(senha))
                {
                    return usuario; // Retorna o usuário autenticado se os hashes coincidirem
                }
            }

            return null; // Retorna null se a autenticação falhar
        }




    }
}
