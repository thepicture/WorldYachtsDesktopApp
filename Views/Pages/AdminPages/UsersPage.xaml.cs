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
        private readonly IUserRepository userRepository =
            new ContextUserRepository();
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

        /// <summary>
        /// Вызывается в момент удаления пользователя.
        /// </summary>
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
                await feedbackService.InformErrorAsync("Не удалось "
                                                       + "удалить пользователя. "
                                                       + "Попробуйте ещё раз");
            }
        }

        /// <summary>
        /// Вызывается в момент завершения редактирования данных пользователя.
        /// </summary>
        private async void OnRowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            User user = e.Row.DataContext as User;

            if (user.UserId == 0)
            {
                if (!await feedbackService
               .AskAsync($"Вы добавили нового пользователя. Сохранить его?"))
                {
                    await LoadUsers();
                    return;
                }
                else
                {
                    await AddNewUser(user);
                    return;
                }
            }

            if (!await feedbackService
                .AskAsync($"Вы изменили данные пользователя. Сохранить их?"))
            {
                await LoadUsers();
                return;
            }

            try
            {
                await Task.Run(() =>
                {
                    using (WorldYachtsBaseEntities context =
                    new WorldYachtsBaseEntities())
                    {
                        User userFromDatabase = context.User.Find(user.UserId);
                        context
                        .Entry(userFromDatabase)
                        .CurrentValues
                        .SetValues(user);
                        context.SaveChanges();
                    }
                });
                await feedbackService
                    .InformAsync($"Пользователь {user.Login} изменён");
                await LoadUsers();
            }
            catch (Exception ex)
            {
                await feedbackService.InformErrorAsync("Не удалось "
                                                       + "изменить данные. "
                                                       + "Попробуйте ещё раз");
                Debug.WriteLine(ex.StackTrace);
            }
        }

        /// <summary>
        /// Добавить нового пользователя.
        /// </summary>
        /// <param name="user">Пользователь.</param>
        /// <returns></returns>
        private async Task AddNewUser(User user)
        {
            user.LastChangePasswordDate = DateTime.Now;
            user.LastInteractionDate = DateTime.Now;
            user.RoleId = 3;
            try
            {
                await userRepository.AddUserAsync(user);
                await LoadUsers();
                await feedbackService
                    .InformAsync($"Пользователь {user.Login} добавлен");
            }
            catch (Exception ex)
            {
                await feedbackService.InformErrorAsync("Не удалось "
                                                                      + "добавить пользователя. "
                                                                      + "Попробуйте ещё раз");
                Debug.WriteLine(ex.StackTrace);
            }
        }
    }
}
