namespace WorldYachtsDesktopApp.Models.LoginModels
{
    public enum LoginReason
    {
        Incorrect = 1,
        NeedToChangePasswordButOk = 3,
        Empty = 4,
        IsBlocked = 5,
        Ok = 6,
    }
}
