using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace Blog.Blazor.Services
{
    public class UsuarioAutenticadoService
    {
        private readonly IJSRuntime _jsRuntime;

        public UsuarioAutenticadoService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task<bool> IsUserLoggedIn()
        {
            try
            {
                var currentUserJson = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "currentUser");

                if (!string.IsNullOrEmpty(currentUserJson))
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                // Se ocorrer um erro ao acessar o armazenamento local, registre o erro para depuração
                Console.WriteLine($"Erro ao acessar o armazenamento local: {ex.Message}");
                return false;
            }
        }
    }
}
