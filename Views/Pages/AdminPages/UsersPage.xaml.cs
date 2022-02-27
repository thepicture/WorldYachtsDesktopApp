using System;
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
    /// Interaction logic for UsersPage.xaml
    /// </summary>
    public partial class UsersPage : Page
    {
        private readonly IFeedbackService feedbackService =
            new MessageBoxFeedbackService();
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
            await LoadUsers();
        }

        /// <summary>
        /// Подгружает список пользователей.
        /// </summary>
        /// <returns></returns>
        private async Task LoadUsers()
        {
            UsersGrid.ItemsSource = await Task.Run(() =>
            {
                using (WorldYachtsBaseEntities context =
                new WorldYachtsBaseEntities())
                {
                    return context.User
                    .Include(u => u.Role)
                    .Where(u => !u.IsDeleted)
                    .ToList();
                }
            });
        }

        private async void OnUserDelete(object sender, RoutedEventArgs e)
        {
            User user = (sender as Button).DataContext as User;
            if (!await feedbackService
                .AskAsync($"Удалить пользователя {user.Login}?"))
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
                        context.User.Find(user.UserId).IsDeleted = true;
                        context.SaveChanges();
                    }
                });
                await feedbackService
                    .InformAsync($"Пользователь {user.Login} удалён");
                await LoadUsers();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
            }
        }
    }
}
