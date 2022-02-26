using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using WorldYachtsDesktopApp.Models.Entities;

namespace WorldYachtsDesktopApp.Services
{
    public class ContextUserRepository : IUserRepository
    {
        public async Task AddUserAsync(User user)
        {
            await Task.Run(() =>
            {
                using (WorldYachtsBaseEntities context = new WorldYachtsBaseEntities())
                {
                    context.User.Add(user);
                    context.SaveChanges();
                }
            });
        }

        public async Task<User> GetUserByLoginPasswordAsync(string login,
                                                            string password)
        {
            return await Task.Run(() =>
            {
                using (WorldYachtsBaseEntities context = new WorldYachtsBaseEntities())
                {
                    return context.User
                        .AsNoTracking()
                        .Include(u => u.Role)
                        .ToList()
                        .FirstOrDefault(u =>
                             u.Login.Equals(login, StringComparison.OrdinalIgnoreCase)
                             && u.Password == password);
                }
            });
        }

        public async Task<bool> IsExistsAsync(string login)
        {
            return await Task.Run(() =>
            {
                using (WorldYachtsBaseEntities context = new WorldYachtsBaseEntities())
                {
                    return context.User.Any(u => u.Login == login);
                }
            });
        }
    }
}
