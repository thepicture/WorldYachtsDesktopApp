using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WorldYachtsDesktopApp.Models.LoginModels;

namespace WorldYachtsDesktopApp.Services
{
    public class UserAuthenticatonService : IAuthenticationService
    {
        private TimeSpan blockTime = TimeSpan.FromSeconds(15);
        public TimeSpan BlockTime
        {
            get
            {
                return _incorrectAttemptsCount < 3
                    ? TimeSpan.Zero
                    : blockTime;
            }
            set => blockTime = value;
        }

        public TimeSpan BlockIncrementTime { get; } =
            TimeSpan.FromSeconds(20);
        private readonly UIElement owner;
        private int _incorrectAttemptsCount = 0;
        public ITimeoutBlocker<UIElement> Blocker { get; }

        public UserAuthenticatonService(UIElement owner, ITimeoutBlocker<UIElement> blocker)
        {
            this.owner = owner;
            Blocker = blocker;
        }

        public UserAuthenticatonService()
        {
        }


        public async Task<ILoginResponse> LoginAsync(string login, string password)
        {
            using (ILoginProvider<MockLoginPasswordPair> context =
                new StubLoginProvider())
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
                        return new BlockedLoginResponse();
                    }

                    if (DateTime.Now.Subtract(currentPair.LastChangePasswordDate).TotalDays >= 14)
                    {
                        return new OkButChangePasswordResponse();
                    }

                    _incorrectAttemptsCount = 0;
                    BlockTime = TimeSpan.FromSeconds(15);
                    return new OkLoginResponse();
                }
                else
                {
                    _incorrectAttemptsCount++;
                    if (_incorrectAttemptsCount > 3)
                    {
                        if (owner != null)
                        {
                            Blocker.Block(owner, BlockTime);
                        }
                        BlockTime += BlockIncrementTime;
                    }
                    return new IncorrectLoginResponse();
                }
            }
        }
    }
}
