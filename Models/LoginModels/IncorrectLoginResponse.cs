using System.Threading.Tasks;
using WorldYachtsDesktopApp.Services;

namespace WorldYachtsDesktopApp.Models.LoginModels
{
    public class IncorrectLoginResponse : ILoginResponse
    {
        private readonly IFeedbackService feedbackService =
            new MessageBoxFeedbackService();
        public async Task ExplainToAsync()
        {
            await feedbackService.WarnAsync("Неверный логин или пароль");
        }
    }
}
