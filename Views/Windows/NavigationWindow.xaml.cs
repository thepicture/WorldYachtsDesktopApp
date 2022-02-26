using System.Windows;
using WorldYachtsDesktopApp.Views.Pages;

namespace WorldYachtsDesktopApp
{
    /// <summary>
    /// Interaction logic for NavigationWindow.xaml
    /// </summary>
    public partial class NavigationWindow : Window
    {
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
    }
}
