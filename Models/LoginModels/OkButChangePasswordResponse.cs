using System.Threading.Tasks;
using WorldYachtsDesktopApp.Models.LoginModels;
using WorldYachtsDesktopApp.Services;
using WorldYachtsDesktopApp.Views.Pages;

namespace WorldYachtsDesktopApp
{
    public class OkButChangePasswordResponse : ILoginResponse
    {
        private readonly IFeedbackService feedbackService =
         new MessageBoxFeedbackService();
        public async Task ExplainToAsync()
        {
            await feedbackService.InformAsync("Пользователь не менял пароль " +
                       "в течении 14 дней. " +
                       "Сейчас появится форма для смены пароля");
            (App.Current.MainWindow as NavigationWindow).MainFrame.Navigate(new ChangePasswordPage());
        }
    }
}