using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WorldYachtsDesktopApp.Services
{
    /// <summary>
    /// Определяет метод для возвращения связки логин/пароль.
    /// </summary>
    /// <typeparam name="TCredentials"></typeparam>
    public interface ILoginProvider<TCredentials> : IDisposable
    {
        /// <summary>
        /// Предоставляет связки логин/пароль.
        /// </summary>
        /// <returns>Связка логин/пароль</returns>
        Task<IEnumerable<TCredentials>> GetAllAsync();
    }
}
