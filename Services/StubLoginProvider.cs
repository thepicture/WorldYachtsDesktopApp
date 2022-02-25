﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorldYachtsDesktopApp.Models.Entities;

namespace WorldYachtsDesktopApp.Services
{
    public class StubLoginProvider : ILoginProvider<User>
    {
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await Task.FromResult<IEnumerable<User>>(new List<User>
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
                    LastChangePasswordDate = DateTime.Now.Subtract(TimeSpan.FromDays(14)),
                },
                new User
                {
                    Login = "xyz",
                    Password = "zyx",
                    LastInteractionDate = DateTime.Now.Subtract(TimeSpan.FromDays(31)),
                    LastChangePasswordDate = DateTime.Now,
                },
                new User
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
