using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorldYachtsDesktopApp.Models;
using WorldYachtsDesktopApp.Models.LoginModels;

namespace WorldYachtsDesktopApp.Services
{
    public class UserAuthenticatonService : IAuthenticationService
    {
        private TimeSpan _blockTime =
            TimeSpan.FromSeconds(15);
        private readonly TimeSpan _blockIncrementTime =
            TimeSpan.FromSeconds(20);
        private int _incorrectAttemptsCount = 0;
        private LoginReason _currentReason = LoginReason.NoActions;
        public TimeSpan GetBlockTime()
        {
            return _blockTime;
        }

        public LoginReason GetReason()
        {
            return _currentReason;
        }

        public async Task<bool> LoginAsync(string login, string password)
        {
            using (ILoginProvider<MockLoginPasswordPair> context =
                new MockLoginProvider())
            {
                IEnumerable<MockLoginPasswordPair> credentials =
                    await context.GetAllAsync();
                bool isAuthenticated = credentials.Any(c =>
                {
                    return c.Login.ToLower() == login
                           && c.Password.ToLower() == password;
                });
                if (isAuthenticated)
                {
                    _incorrectAttemptsCount = 0;
                    _blockTime = TimeSpan.FromSeconds(15);
                    _currentReason = LoginReason.Ok;
                    return await Task.FromResult(true);
                }
                else
                {
                    _incorrectAttemptsCount++;
                    if (_incorrectAttemptsCount > 3)
                    {
                        _currentReason = LoginReason.Incorrect;
                        _blockTime += _blockIncrementTime;
                    }
                    return await Task.FromResult(false);
                }
            }
        }
    }
}
