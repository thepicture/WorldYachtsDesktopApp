using System.Threading.Tasks;
using WorldYachtsDesktopApp.Models.LoginModels;
using WorldYachtsDesktopApp.Services;

namespace WorldYachtsDesktopApp
{
    public class OkLoginResponse : ILoginResponse
    {
        private readonly IFeedbackService feedbackService =
        new MessageBoxFeedbackService();
        public async Task ExplainToAsync()
        {
            await feedbackService.InformAsync("Вы успешно авторизованы");
        }
    }
}