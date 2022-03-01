using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WorldYachtsDesktopApp.Models.Entities;
using WorldYachtsDesktopApp.Services;

namespace WorldYachtsDesktopApp.Views.Pages.AdminPages
{
    /// <summary>
    /// Interaction logic for AccessoriesPage.xaml
    /// </summary>
    public partial class AccessoriesPage : Page
    {
        private readonly IFeedbackService feedbackService =
            new MessageBoxFeedbackService();
        public AccessoriesPage()
        {
            InitializeComponent();
            DataContext = this;
        }

        /// <summary>
        /// Вызывается после загрузки страницы.
        /// </summary>
        private async void OnLoaded(object sender, RoutedEventArgs e)
        {
            AccessoriesGrid.ItemsSource = await GetAccessories();
        }

        /// <summary>
        /// Получить аксессуары.
        /// </summary>
        /// <returns>Аксессуары.</returns>
        private async Task<IEnumerable<Accessory>> GetAccessories()
        {
            return await Task.Run(() =>
            {
                using (WorldYachtsBaseEntities context =
                new WorldYachtsBaseEntities())
                {
                    return context.Accessory
                    .Include(a => a.Fit)
                    .Where(a => !a.IsDeleted)
                    .ToList();
                }
            });
        }

        /// <summary>
        /// Удалить выбранный аксессуар.
        /// </summary>
        private void DeleteAccessory(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// Вызывается в момент 
        /// редактирования записи аксессуара.
        /// </summary>
        private void OnRowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {

        }

        /// <summary>
        /// Перейти на страницу изменения цены.
        /// </summary>
        private void GoToChangeAccessoriesPrice(object sender, RoutedEventArgs e)
        {

        }
    }
}
