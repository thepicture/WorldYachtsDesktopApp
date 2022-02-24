using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorldYachtsDesktopApp.Models;

namespace WorldYachtsDesktopApp.Services
{
    public class MockLoginProvider : ILoginProvider<MockLoginPasswordPair>
    {
        public async Task<IEnumerable<MockLoginPasswordPair>> GetAllAsync()
        {
            return await Task.FromResult<IEnumerable<MockLoginPasswordPair>>(new List<MockLoginPasswordPair>
            {
                new MockLoginPasswordPair
                {
                    Login = "123",
                    Password = "321",
                    LastInteractionDate = DateTime.Now,
                    LastChangePasswordDate = DateTime.Now,
                },
                new MockLoginPasswordPair
                {
                    Login = "abc",
                    Password = "cba",
                    LastInteractionDate = DateTime.Now,
                    LastChangePasswordDate = DateTime.Now.Subtract(TimeSpan.FromDays(14)),
                },
                new MockLoginPasswordPair
                {
                    Login = "xyz",
                    Password = "zyx",
                    LastInteractionDate = DateTime.Now.Subtract(TimeSpan.FromDays(31)),
                    LastChangePasswordDate = DateTime.Now,
                },
                new MockLoginPasswordPair
                {
                    Login = "cDe",
                    Password = "123",
                    LastInteractionDate = DateTime.Now,
                    LastChangePasswordDate = DateTime.Now,
                },
            });
        }

        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
