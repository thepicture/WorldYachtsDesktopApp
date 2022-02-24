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
            await Task.Delay(TimeSpan.FromSeconds(5));
            return await Task.FromResult<IEnumerable<MockLoginPasswordPair>>(new List<MockLoginPasswordPair>
            {
                new MockLoginPasswordPair
                {
                    Login = "123",
                    Password = "321",
                },
                new MockLoginPasswordPair
                {
                    Login = "abc",
                    Password = "cba",
                },
                new MockLoginPasswordPair
                {
                    Login = "cDe",
                    Password = "123",
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
