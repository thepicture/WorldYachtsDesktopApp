using System.Threading.Tasks;
using System.Windows;
using WorldYachtsDesktopApp.Models.LoginModels;

namespace WorldYachtsDesktopApp.Services
{
    /// <summary>
    /// Определяет методы для аутентификации с поддержкой блокировки.
    /// </summary>
    public interface IAuthenticationService : IHaveBlocker<UIElement>
    {
        /// <summary>
        /// Осуществляет аутентификацию.
        /// </summary>
        /// <param name="login">Логин.</param>
        /// <param name="password">Пароль.</param>
        /// <returns>Задача.</returns>

        Task<ILoginResponse> LoginAsync(string login, string password);
    }
}
