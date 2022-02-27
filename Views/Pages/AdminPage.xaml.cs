using System.Windows;
using System.Windows.Controls;
using WorldYachtsDesktopApp.Views.Pages.AdminPages;

namespace WorldYachtsDesktopApp.Views.Pages
{
    /// <summary>
    /// Interaction logic for AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Page
    {
        public AdminPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Произвести навигацию на страницу управления пользователями.
        /// </summary>
        private void PerformGoToUsersPage(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new UsersPage());
        }
    }
}
