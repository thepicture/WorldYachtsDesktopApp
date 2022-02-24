﻿using System;
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
            if (_incorrectAttemptsCount < 3)
            {
                return TimeSpan.Zero;
            }
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
                MockLoginPasswordPair currentPair = credentials.FirstOrDefault(c =>
                {
                    return c.Login.ToLower()
                    == login.ToLower()
                    && c.Password == password;
                });
                if (currentPair != null)
                {
                    if (DateTime.Now.Subtract(currentPair.LastInteractionDate).TotalDays >= 31)
                    {
                        _currentReason = LoginReason.IsBlocked;
                        return await Task.FromResult(false);
                    }

                    if (DateTime.Now.Subtract(currentPair.LastChangePasswordDate).TotalDays >= 14)
                    {
                        _currentReason = LoginReason.NeedToChangePasswordButOk;
                        return await Task.FromResult(false);
                    }

                    _incorrectAttemptsCount = 0;
                    _blockTime = TimeSpan.FromSeconds(15);
                    _currentReason = LoginReason.Ok;
                    return await Task.FromResult(true);
                }
                else
                {
                    _incorrectAttemptsCount++;
                    _currentReason = LoginReason.Incorrect;
                    if (_incorrectAttemptsCount > 3)
                    {
                        _blockTime += _blockIncrementTime;
                    }
                    return await Task.FromResult(false);
                }
            }
        }
    }
}
