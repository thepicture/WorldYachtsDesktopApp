using System.Threading.Tasks;
using WorldYachtsDesktopApp.Models.Entities;

namespace WorldYachtsDesktopApp.Services
{
    public interface IUserRepository
    {
        Task<User> GetUserByLoginPasswordAsync(string login, string password);
        Task AddUserAsync(User user);
        Task SaveChangesAsync();
    }
}
