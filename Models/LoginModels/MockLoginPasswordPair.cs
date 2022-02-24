using System;

namespace WorldYachtsDesktopApp.Models.LoginModels
{
    public class MockLoginPasswordPair
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public DateTime LastInteractionDate { get; set; }
        public DateTime LastChangePasswordDate { get; set; }
    }
}
