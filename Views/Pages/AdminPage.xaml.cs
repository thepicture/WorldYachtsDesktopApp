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

        /// <summary>
        /// Произвести навигацию 
        /// на страницу управления производством лодок.
        /// </summary>
        private void PerformGoToBoatsPage(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new BoatsPage());
        }

        /// <summary>
        /// Произвести навигацию 
        /// на страницу управления производством аксессуаров.
        /// </summary>
        private void PerformGoToAccessoriesPage(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AccessoriesPage());
        }
    }
}
