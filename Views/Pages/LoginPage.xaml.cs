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
        private readonly IAuthenticationService authenticationService;
        private readonly IFeedbackService feedbackService =
            new MessageBoxFeedbackService();


        public LoginPage()
        {
            InitializeComponent();
            authenticationService = new UserAuthenticatonService(this,
                                                                 new UIElementTimeoutBlocker(),
                                                                 new ContextUserRepository());
        }

        /// <summary>
        /// Выполнить аутентификацию.
        /// </summary>
        private async void PerformAuthenticationAsync(object sender, RoutedEventArgs e)
        {
            ILoginResponse response = await authenticationService
                .LoginAsync(Login.Text,
                            Password.Password);
            await response.ExplainToAsync();
            if (response is OkLoginResponse)
            {

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

        /// <summary>
        /// Перейти к странице ввода нового пользователя.
        /// </summary>
        private void NavigateToRegistrationPage(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegistrationPage());
        }
    }
}
