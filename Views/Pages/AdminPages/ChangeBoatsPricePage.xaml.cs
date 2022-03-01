using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WorldYachtsDesktopApp.Models.Entities;
using WorldYachtsDesktopApp.Services;

namespace WorldYachtsDesktopApp.Views.Pages.AdminPages
{
    /// <summary>
    /// Interaction logic for ChangeBoatsPricePage.xaml
    /// </summary>
    public partial class ChangeBoatsPricePage : Page
    {
        readonly IFeedbackService feedbackService =
            new MessageBoxFeedbackService();
        public ChangeBoatsPricePage(IEnumerable<Boat> boats)
        {
            InitializeComponent();
            Boats = boats;
        }

        public IEnumerable<Boat> Boats { get; set; }

        /// <summary>
        /// Изменяет цену лодок.
        /// </summary>
        private async void PerformPriceChange(object sender, RoutedEventArgs e)
        {
            string action = (PriceActions.SelectedItem as ComboBoxItem)
                .Content
                .ToString();
            switch (action)
            {
                case "увеличить":
                    try
                    {
                        double howMuchPercent =
                            Convert.ToInt32(HowMuchBox.Text)
                            * 1.0
                            / 100;
                        await Task.Run(() =>
                        {
                            using (WorldYachtsBaseEntities context =
                            new WorldYachtsBaseEntities())
                            {
                                foreach (Boat boat in Boats)
                                {
                                    context.Boat
                                    .Find(boat.BoatId)
                                    .BasePrice += Convert
                                    .ToInt32(
                                        Math.Ceiling(
                                            howMuchPercent
                                        * Convert.ToDouble(boat.BasePrice)
                                        )
                                        );
                                }
                                context.SaveChanges();
                            }
                        });
                        await feedbackService.InformAsync("Цена успешно изменена");
                        NavigationService.GoBack();
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.ToString());
                        await feedbackService
                            .InformErrorAsync("Не удалось "
                                              + "изменить цену. "
                                              + "Попробуйте "
                                              + "перезагрузить страницу "
                                              + "и попробовать ещё раз");
                    }
                    break;
                case "уменьшить":
                    try
                    {
                        double howMuchPercent =
                           Convert.ToInt32(HowMuchBox.Text)
                           * 1.0
                           / 100;
                        await Task.Run(() =>
                        {
                            using (WorldYachtsBaseEntities context =
                            new WorldYachtsBaseEntities())
                            {
                                foreach (Boat boat in Boats)
                                {
                                    context.Boat
                                    .Find(boat.BoatId)
                                    .BasePrice -= Convert
                                    .ToInt32(
                                        Math.Ceiling(
                                            howMuchPercent
                                        * Convert.ToDouble(boat.BasePrice)
                                        ));
                                }
                                context.SaveChanges();
                            }
                        });
                        await feedbackService.InformAsync("Цена успешно изменена");
                        NavigationService.GoBack();
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.ToString());
                        await feedbackService
                            .InformErrorAsync("Не удалось "
                                              + "изменить цену. "
                                              + "Попробуйте "
                                              + "перезагрузить страницу "
                                              + "и попробовать ещё раз");
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
