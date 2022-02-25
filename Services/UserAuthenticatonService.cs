using System;
using System.Threading.Tasks;
using System.Windows;
using WorldYachtsDesktopApp.Models.Entities;
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
        private readonly IUserRepository repository;
        private int _incorrectAttemptsCount = 0;
        public ITimeoutBlocker<UIElement> Blocker { get; }

        public UserAuthenticatonService(UIElement owner,
                                        ITimeoutBlocker<UIElement> blocker,
                                        IUserRepository repository)
        {
            this.owner = owner;
            Blocker = blocker;
            this.repository = repository;
        }

        public async Task<ILoginResponse> LoginAsync(string login, string password)
        {
            using (WorldYachtsBaseEntities context = new WorldYachtsBaseEntities())
            {
                User currentUser = await repository.GetUserByLoginPasswordAsync(login, password);
                if (currentUser != null)
                {
                    if (DateTime.Now.Subtract(currentUser.LastInteractionDate).TotalDays >= 31)
                    {
                        return new BlockedLoginResponse();
                    }

                    if (DateTime.Now.Subtract(currentUser.LastChangePasswordDate).TotalDays >= 14)
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
