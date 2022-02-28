using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WorldYachtsDesktopApp.Models.Entities;
using WorldYachtsDesktopApp.Services;

namespace WorldYachtsDesktopApp.Views.Pages.AdminPages
{
    /// <summary>
    /// Interaction logic for BoatsPage.xaml
    /// </summary>
    public partial class BoatsPage : Page
    {
        public IEnumerable<BoatType> BoatTypes { get; set; }
        public IEnumerable<Wood> WoodTypes { get; set; }
        private readonly IFeedbackService feedbackService =
            new MessageBoxFeedbackService();

        public BoatsPage()
        {
            InitializeComponent();
            DataContext = this;
        }

        /// <summary>
        /// Вызывается в момент открытия страницы.
        /// </summary>
        private async void OnLoaded(object sender, RoutedEventArgs e)
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
            BoatsGrid.ItemsSource = await GetBoats();
        }

        private async Task<List<Boat>> GetBoats()
        {
            return await Task.Run(() =>
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

        /// <summary>
        /// Удалить выбранную лодку.
        /// </summary>
        private async void DeleteBoat(object sender, RoutedEventArgs e)
        {
            Boat boat = (sender as Button).DataContext as Boat;
            if (!await feedbackService
                .AskAsync($"Удалить лодку {boat.Model}?"))
            {
                return;
            }
            try
            {
                await Task.Run(() =>
                {
                    using (WorldYachtsBaseEntities context =
                    new WorldYachtsBaseEntities())
                    {
                        context.Boat.Find(boat.BoatId)
                        .IsDeleted = true;
                        context.SaveChanges();
                    }
                });
                BoatsGrid.ItemsSource = await GetBoats();
                await feedbackService.InformAsync("Лодка удалена");
            }
            catch (Exception ex)
            {
                await feedbackService.InformErrorAsync("Не удалось "
                                                       + "удалить лодку. "
                                                       + "Перезагрузите "
                                                       + "страницу");
                Debug.Write(ex.StackTrace);
            }
        }

        /// <summary>
        /// Вызывается в момент завершения изменений данных 
        /// о лодке.
        /// </summary>
        private async void OnRowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            if (!await feedbackService.AskAsync("Вы завершили " +
                "изменение лодки. Применить изменения?"))
            {
                BoatsGrid.ItemsSource = await GetBoats();
                return;
            }
            Boat boat = e.Row.DataContext as Boat;
            try
            {
                await Task.Run(() =>
                {
                    using (WorldYachtsBaseEntities context =
                    new WorldYachtsBaseEntities())
                    {
                        context.Entry(
                            context.Boat.Find(boat.BoatId)
                            )
                        .CurrentValues
                        .SetValues(boat);
                        context.SaveChanges();
                    }
                });
                BoatsGrid.ItemsSource = await GetBoats();
                await feedbackService.InformAsync("Лодка изменена");
            }
            catch (Exception ex)
            {
                await feedbackService.InformErrorAsync("Не удалось "
                                                       + "изменить данные "
                                                       + "о лодке. "
                                                       + "Перезагрузите "
                                                       + "страницу");
                Debug.Write(ex.StackTrace);
            }
        }
    }
}
