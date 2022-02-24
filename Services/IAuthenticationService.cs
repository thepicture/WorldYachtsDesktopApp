using System;
using System.Threading.Tasks;
using WorldYachtsDesktopApp.Models.LoginModels;

namespace WorldYachtsDesktopApp.Services
{
    /// <summary>
    /// Определяет методы для аутентификации.
    /// </summary>
    public interface IAuthenticationService
    {
        /// <summary>
        /// Получает причину результата аутентификации.
        /// </summary>
        /// <returns></returns>
        LoginReason GetReason();
        /// <summary>
        /// Осуществляет аутентификацию.
        /// </summary>
        /// <param name="login">Логин.</param>
        /// <param name="password">Пароль.</param>
        /// <returns>Задача.</returns>

        Task LoginAsync(string login, string password);
        /// <summary>
        /// Получает время блокировки.
        /// </summary>
        /// <returns>Время блокировки.</returns>
        TimeSpan GetBlockTime();
    }
}
