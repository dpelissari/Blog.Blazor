using Blog.Blazor.Models;

namespace Blog.Blazor.Services
{
    public class UsuarioAutenticadoService
    {
        private Usuario _currentUser;

        public Usuario CurrentUser => _currentUser;

        public bool IsUserLoggedIn => _currentUser != null;

        public event Action OnUserChanged;

        public void SetCurrentUser(Usuario user)
        {
            _currentUser = user;
            OnUserChanged?.Invoke();
        }
    }
}
