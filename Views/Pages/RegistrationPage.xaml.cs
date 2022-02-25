using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WorldYachtsDesktopApp.Models.Entities;

namespace WorldYachtsDesktopApp.Views.Pages
{
    /// <summary>
    /// Interaction logic for RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {
        public RegistrationPage()
        {
            InitializeComponent();
        }

        private async Task<List<Role>> GetRolesAsync()
        {
            return await Task.Run(() =>
            {
                using (WorldYachtsBaseEntities context = new WorldYachtsBaseEntities())
                {
                    return context.Role.ToList();
                }
            });
        }

        /// <summary>
        /// Ввести нового пользователя.
        /// </summary>
        private void PerformRegistrationAsync(object sender, RoutedEventArgs e)
        {

        }

        private async void OnLoad(object sender, RoutedEventArgs e)
        {
            List<Role> roles = await GetRolesAsync();
            Role.ItemsSource = roles;
            Role.SelectedItem = roles.FirstOrDefault();
        }
    }
}
