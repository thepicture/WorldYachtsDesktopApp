using System.Linq;
using System.Windows;
using System.Windows.Navigation;
using WorldYachtsDesktopApp.Services;
using WorldYachtsDesktopApp.Views.Pages;

namespace WorldYachtsDesktopApp
{
    /// <summary>
    /// Interaction logic for NavigationWindow.xaml
    /// </summary>
    public partial class NavigationWindow : Window
    {
        private readonly IFeedbackService feedbackService =
            new MessageBoxFeedbackService();
        public NavigationWindow()
        {
            InitializeComponent();
            _ = MainFrame.Navigate(new LoginPage());
        }

        /// <summary>
        /// Перейти на предыдущую страницу.
        /// </summary>
        private void PerformGoBack(object sender, RoutedEventArgs e)
        {
            MainFrame.GoBack();
        }

        /// <summary>
        /// Завершить сессию.
        /// </summary>
        private async void PerformGoToLoginPage(object sender,
                                                RoutedEventArgs e)
        {
            if (!await feedbackService
                .AskAsync("Завершить сессию и перейти на окно авторизации?"))
            {
                return;
            }
            (App.Current as App).User = null;
            while (MainFrame.CanGoBack)
            {
                MainFrame.GoBack();
            }
        }

        /// <summary>
        /// Вызывается в момент окончания навигации.
        /// </summary>
        private void OnNavigated(object sender, NavigationEventArgs e)
        {
            if ((App.Current as App).User is null)
            {
                if (MainFrame.CanGoBack)
                    GoBack.Visibility = Visibility.Visible;
                else
                    GoBack.Visibility = Visibility.Collapsed;
            }
            else
            {
                if (MainFrame.BackStack.Cast<JournalEntry>().Count() < 2)
                    GoBack.Visibility = Visibility.Collapsed;
                else
                    GoBack.Visibility = Visibility.Visible;
            }
        }
    }
}
