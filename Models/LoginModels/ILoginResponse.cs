using System.Threading.Tasks;

namespace WorldYachtsDesktopApp.Models.LoginModels
{
    /// <summary>
    /// Определяет метод оповещения о событии аутентификации.
    /// </summary>
    public interface ILoginResponse
    {
        /// <summary>
        /// Оповещает о событии аутентификации.
        /// </summary>
        Task ExplainToAsync();
    }
}
