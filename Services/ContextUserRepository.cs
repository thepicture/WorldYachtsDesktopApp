using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using WorldYachtsDesktopApp.Models.Entities;

namespace WorldYachtsDesktopApp.Services
{
    public class ContextUserRepository : IUserRepository
    {
        private bool disposedValue;
        private WorldYachtsBaseEntities context;

        public ContextUserRepository()
        {
            Task.Run(() => context = new WorldYachtsBaseEntities());
        }

        public async Task AddUserAsync(User user)
        {
            await Task.Run(() =>
            {
                return context.User.Add(user);
            });
        }

        public async Task<User> GetUserByLoginPasswordAsync(string login,
                                                            string password)
        {
            List<User> users = await context.User.ToListAsync();
            return await Task.Run(() =>
            {
                return users.FirstOrDefault(u =>
                         u.Login.Equals(login, StringComparison.OrdinalIgnoreCase)
                         && u.Password == password);
            });
        }

        public async Task<bool> IsExistsAsync(string login)
        {
            return await context.User.AnyAsync(u => u.Login == login);
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    context.Dispose();
                }

                context = null;
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
