using System.Threading.Tasks;
using WorldYachtsDesktopApp.Models.Entities;

namespace WorldYachtsDesktopApp.Services
{
    /// <summary>
    /// Определяет репозиторий для работы с пользователями.
    /// </summary>
    public interface IUserRepository
    {
        Task<User> GetUserByLoginPasswordAsync(string login, string password);
        Task AddUserAsync(User user);
        Task<bool> IsExistsAsync(string login);
    }
}
