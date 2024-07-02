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

        /// <summary>
        /// Metodo para adicionar um usuario
        /// </summary>
        /// <param name="usuario"></param>
        /// <param name="senha"></param>
        /// <returns></returns>
        public async Task Adicionar(Usuario usuario, string senha)
        {
            // Criptografar a senha antes de armazená-la no banco de dados
            usuario.SenhaHash = await HashSenhaAsync(senha);

            await dbContexto.AddAsync(usuario);
            await dbContexto.SaveChangesAsync();
        }

        /// <summary>
        /// Metodo para atualizar um usuario
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public async Task Atualizar(Usuario usuario)
        {
            dbContexto.Update(usuario);
            await dbContexto.SaveChangesAsync();
        }

        /// <summary>
        /// Metodo para excluir um usuario
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public async Task Apagar(Usuario usuario)
        {
            dbContexto.Remove(usuario);
            await dbContexto.SaveChangesAsync();
        }

        /// <summary>
        /// Obtem todos usuários sem nenhum filtro
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Usuario>> BuscarTodos()
        {
            return await dbContexto.Usuario.ToListAsync();
        }

        /// <summary>
        /// Obtem usuário pelo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Usuario>BuscarUsuarioPorId(Guid id)
        {
            return await dbContexto.Usuario.FirstOrDefaultAsync(f => f.Id == id);
        }

        /// <summary>
        /// Gera o hash 256 da senha com base na senha
        /// </summary>
        /// <param name="senha"></param>
        /// <returns></returns>
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
    }
}
