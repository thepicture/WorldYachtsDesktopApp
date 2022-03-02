using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
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
        public IEnumerable<BoatColor> Colors { get; set; }
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
            Colors = await Task.Run(() =>
            {
                using (WorldYachtsBaseEntities context =
                new WorldYachtsBaseEntities())
                {
                    return context.BoatColor.ToList();
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
                    .Include(b => b.BoatColor)
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
                Debug.WriteLine(ex.StackTrace);
            }
        }

        /// <summary>
        /// Вызывается в момент завершения изменений данных 
        /// о лодке.
        /// </summary>
        private async void OnRowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            Boat boat = e.Row.DataContext as Boat;

            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(boat.Model) || boat.Model.Length > 50)
            {
                errors.AppendLine("Наименование лодки - это обязательное " +
                    "поле до 50 символов");
            }
            if (!int.TryParse(
                boat.NumberOfRowers.ToString(), out _
                              )
                || int.Parse(
                    boat.NumberOfRowers.ToString()
                    ) < 1)
            {
                errors.AppendLine("Количество мест - это обязательное " +
                    "целое положительное число");
            }
            if (boat.ColorId == 0)
            {
                errors.AppendLine("Укажите цвет лодки");
            }
            if (!decimal.TryParse(
              boat.BasePrice.ToString(), out _
                            )
              || decimal.Parse(
                  boat.BasePrice.ToString()
                  ) <= 0)
            {
                errors.AppendLine("Цена лодки - " +
                    "это положительное целое число в рублях");
            }
            if (boat.WoodId == 0)
            {
                errors.AppendLine("Укажите тип дерева");
            }
            if (boat.BoatTypeId == 0)
            {
                errors.AppendLine("Укажите тип лодки");
            }
            if (errors.Length > 0)
            {
                await feedbackService.WarnAsync(errors.ToString());
                BoatsGrid.ItemsSource = await GetBoats();
                return;
            }

            string reason = boat.BoatId == 0
                ? "добавление"
                : "изменение";
            if (!await feedbackService.AskAsync("Вы завершили " +
                $"{reason} лодки. Применить изменения?"))
            {
                BoatsGrid.ItemsSource = await GetBoats();
                return;
            }
            try
            {
                if (boat.BoatId == 0)
                {
                    await Task.Run(() =>
                    {
                        using (WorldYachtsBaseEntities context =
                        new WorldYachtsBaseEntities())
                        {
                            context.Boat.Add(boat);
                            context.SaveChanges();
                        }
                    });
                }
                else
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
                }
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
                Debug.WriteLine(ex.StackTrace);
            }
        }

        /// <summary>
        /// Перейти на страницу изменения цены лодки.
        /// </summary>
        private void GoToChangeBoatsPrice(object sender, RoutedEventArgs e)
        {
            IEnumerable<Boat> boats = BoatsGrid.SelectedItems.Cast<Boat>();
            NavigationService.Navigate(new ChangeBoatsPricePage(boats));
        }
    }
}
