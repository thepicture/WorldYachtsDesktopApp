using System.Threading.Tasks;

namespace WorldYachtsDesktopApp.Services
{
    /// <summary>
    /// Определяет методы для обратной связи.
    /// </summary>
    public interface IFeedbackService
    {
        /// <summary>
        /// Информировать.
        /// </summary>
        /// <param name="message">Сообщение.</param>
        /// <returns>Задача.</returns>
        Task InformAsync(string message);
        /// <summary>
        /// Предупредить.
        /// </summary>
        /// <param name="message">Сообщение.</param>
        /// <returns>Задача.</returns>
        Task WarnAsync(string message);
        /// <summary>
        /// Спросить.
        /// </summary>
        /// <param name="question">Вопрос.</param>
        /// <returns><see langword="true"/>, если ответ утвердительный, 
        /// иначе <see langword="false"/></returns>
        Task<bool> AskAsync(string question);
        /// <summary>
        /// Информировать об ошибке.
        /// </summary>
        /// <param name="message">Сообщение.</param>
        /// <returns>Задача</returns>
        Task InformErrorAsync(string message);
    }
}