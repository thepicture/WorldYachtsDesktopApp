using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorldYachtsDesktopApp.Models.Entities;

namespace WorldYachtsDesktopApp.Services
{
    public class StubUserRepository
    {
        private IList<User> users = new List<User>
            {
                new User
                {
                    Login = "123",
                    Password = "321",
                    LastInteractionDate = DateTime.Now,
                    LastChangePasswordDate = DateTime.Now,
                },
                new User
                {
                    Login = "abc",
                    Password = "cba",
                    LastInteractionDate = DateTime.Now,
                    LastChangePasswordDate =
                    DateTime.Now.Subtract(TimeSpan.FromDays(14)),
                },
                new User
                {
                    Login = "xyz",
                    Password = "zyx",
                    LastInteractionDate =
                    DateTime.Now.Subtract(TimeSpan.FromDays(31)),
                    LastChangePasswordDate = DateTime.Now,
                },
                new User
                {
                    Login = "cDe",
                    Password = "123",
                    LastInteractionDate = DateTime.Now,
                    LastChangePasswordDate = DateTime.Now,
                },
            };

        public async Task AddUserAsync(User user)
        {
            await Task.Run(() =>
            {
                users.Add(user);
            });
        }

        public void Dispose()
        {
            users = null;
        }

        public async Task<User> GetUserByLoginPasswordAsync(string login,
                                                            string password)
        {
            return await Task.Run(() =>
            {
                return users.FirstOrDefault(u =>
                u.Login.Equals(login, StringComparison.OrdinalIgnoreCase)
                && u.Password == password);
            });
        }

        public async Task<bool> IsExistsAsync(string login)
        {
            bool isExists = users.Any(u =>
            {
                return u.Login.Equals(login,
                                      StringComparison.OrdinalIgnoreCase);
            });
            return await Task.FromResult(isExists);
        }

        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
