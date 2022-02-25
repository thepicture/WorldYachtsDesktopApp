using System;
using System.Linq;
using System.Threading.Tasks;
using WorldYachtsDesktopApp.Models.Entities;

namespace WorldYachtsDesktopApp.Services
{
    public class ContextUserRepository : IUserRepository
    {
        public async Task AddUserAsync(User user)
        {
            using (WorldYachtsBaseEntities context = new WorldYachtsBaseEntities())
            {
                _ = await Task.Run(() =>
                  {
                      return context.User.Add(user);
                  });
            }
        }

        public async Task<User> GetUserByLoginPasswordAsync(string login, string password)
        {
            using (WorldYachtsBaseEntities context = new WorldYachtsBaseEntities())
            {
                return await Task.Run(() =>
                {
                    return context.User.ToList().FirstOrDefault(u =>
                    u.Login.Equals(login, StringComparison.OrdinalIgnoreCase)
                    && u.Password == password);
                });
            }
        }

        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
