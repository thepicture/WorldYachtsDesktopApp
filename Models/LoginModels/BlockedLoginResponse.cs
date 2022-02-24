using System.Threading.Tasks;
using WorldYachtsDesktopApp.Models.LoginModels;
using WorldYachtsDesktopApp.Services;

namespace WorldYachtsDesktopApp
{
    public class BlockedLoginResponse : ILoginResponse
    {
        private readonly IFeedbackService feedbackService =
          new MessageBoxFeedbackService();
        public async Task ExplainToAsync()
        {
            await feedbackService.InformAsync("Вы заблокированы. " +
                          "Пользователь системы не заходил в неё в течении 1 месяца");
        }
    }
}