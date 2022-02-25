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
            using (Context context = new Context())
            {
                await Task.Run(() =>
                {
                    context.User.Add(user);
                });
            }
        }

        public async Task<User> GetUserByLoginPasswordAsync(string login, string password)
        {
            using (Context context = new Context())
            {
                return await Task.Run(() =>
                {
                    return context.User.FirstOrDefault(u =>
                    u.Login.ToLower()
                    == login.ToLower() && u.Password == password);
                });
            }
        }

        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
