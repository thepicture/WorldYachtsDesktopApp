using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WorldYachtsDesktopApp.Models.Entities;

namespace WorldYachtsDesktopApp.Views.Pages.AdminPages
{
    /// <summary>
    /// Interaction logic for UsersPage.xaml
    /// </summary>
    public partial class UsersPage : Page
    {
        public UsersPage()
        {
            InitializeComponent();
            DataContext = this;
        }

        /// <summary>
        /// Вызывается в момент загрузки страницы.
        /// </summary>
        private async void OnLoaded(object sender, RoutedEventArgs e)
        {
            UsersGrid.ItemsSource = await Task.Run(() =>
            {
                using (WorldYachtsBaseEntities context =
                new WorldYachtsBaseEntities())
                {
                    return context.User
                    .Include(u => u.Role)
                    .ToList();
                }
            });
        }
    }
}
