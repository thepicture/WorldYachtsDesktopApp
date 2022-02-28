using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using WorldYachtsDesktopApp.Models.Entities;

namespace WorldYachtsDesktopApp.Views.Pages.AdminPages
{
    /// <summary>
    /// Interaction logic for BoatsPage.xaml
    /// </summary>
    public partial class BoatsPage : Page
    {
        public IEnumerable<BoatType> BoatTypes { get; set; }
        public IEnumerable<Wood> WoodTypes { get; set; }
        public BoatsPage()
        {
            InitializeComponent();
            DataContext = this;
        }

        /// <summary>
        /// Вызывается в момент открытия страницы.
        /// </summary>
        private async void OnLoaded(object sender, System.Windows.RoutedEventArgs e)
        {
            WoodTypes = await Task.Run(() =>
            {
                using (WorldYachtsBaseEntities context =
                new WorldYachtsBaseEntities())
                {
                    return context.Wood.ToList();
                }
            });
            BoatTypes = await Task.Run(() =>
            {
                using (WorldYachtsBaseEntities context =
                new WorldYachtsBaseEntities())
                {
                    return context.BoatType.ToList();
                }
            });
            BoatsGrid.ItemsSource = await Task.Run(() =>
            {
                using (WorldYachtsBaseEntities context =
                new WorldYachtsBaseEntities())
                {
                    return context.Boat
                    .Include(b => b.Wood)
                    .Include(b => b.BoatType)
                    .Where(b => !b.IsDeleted)
                    .ToList();
                }
            });
        }
    }
}
