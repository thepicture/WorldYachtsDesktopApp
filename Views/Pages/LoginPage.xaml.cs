using System.Windows;
using System.Windows.Controls;
using WorldYachtsDesktopApp.Models.LoginModels;
using WorldYachtsDesktopApp.Services;

namespace WorldYachtsDesktopApp.Views.Pages
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        private readonly IAuthenticationService authenticationService =
            new UserAuthenticatonService();
        private readonly IFeedbackService feedbackService =
            new MessageBoxFeedbackService();
        private readonly ITimeoutBlocker<UIElement> blocker =
            new UIElementTimeoutBlocker();

        public LoginPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Выполнить аутентификацию.
        /// </summary>
        private async void PerformAuthenticationAsync(object sender, RoutedEventArgs e)
        {
            await authenticationService.LoginAsync(Login.Text, Password.Password);
            switch (authenticationService.GetReason())
            {
                case LoginReason.Incorrect:
                    await feedbackService.WarnAsync("Неверный логин или пароль");
                    blocker.Block(this, authenticationService.GetBlockTime());
                    break;
                case LoginReason.IsBlocked:
                    await feedbackService.InformAsync("Вы заблокированы. " +
                        "Пользователь системы не заходил в неё в течении 1 месяца");
                    break;
                case LoginReason.NeedToChangePasswordButOk:
                    await feedbackService.InformAsync("пользователь не менял пароль " +
                        "в течении 14 дней. Сейчас появится форма для смены пароля");
                    break;
                case LoginReason.Ok:
                    await feedbackService.InformAsync("Вы успешно авторизованы");
                    break;
                case LoginReason.Empty:
                default:
                    break;
            }
        }

        /// <summary>
        /// Выключить текущее приложение.
        /// </summary>
        private async void PerformExitAsync(object sender, RoutedEventArgs e)
        {
            if (await feedbackService.AskAsync("Действительно выключить приложение?"))
            {
                App.Current.Shutdown();
            }
        }
    }
}
