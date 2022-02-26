using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WorldYachtsDesktopApp.Models.Entities;
using WorldYachtsDesktopApp.Services;

namespace WorldYachtsDesktopApp.Views.Pages
{
    /// <summary>
    /// Interaction logic for RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {
        public IUserRepository repository = new ContextUserRepository();
        private readonly IFeedbackService feedbackService =
         new MessageBoxFeedbackService();

        public RegistrationPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Получить роли пользователей.
        /// </summary>
        /// <returns></returns>
        private async Task<List<Role>> GetRolesAsync()
        {
            return await Task.Run(() =>
            {
                using (WorldYachtsBaseEntities context =
                new WorldYachtsBaseEntities())
                {
                    return context.Role.ToList();
                }
            });
        }

        /// <summary>
        /// Ввести нового пользователя.
        /// </summary>
        private async void PerformRegistrationAsync(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(Login.Text)
                || Login.Text.Length < 1
                || Login.Text.Length > 50)
            {
                errors.AppendLine("Логин - это обязательное текстовое поле "
                                  + "до 50 символов");
            }
            else if (await repository.IsExistsAsync(Login.Text))
            {
                errors.AppendLine($"Пользователь {Login.Text} "
                                  + "в системе уже существует");
            }
            if (string.IsNullOrWhiteSpace(Password.Password)
              || Password.Password.Length < 1
              || Password.Password.Length > 50)
            {
                errors.AppendLine("Пароль - это обязательное текстовое поле "
                                  + "до 50 символов");
            }
            if (Role.SelectedItem is null)
            {
                errors.AppendLine("Роль обязательна. Если список пустой, " +
                    "то обратитесь к системному администратору");
            }

            if (errors.Length > 0)
            {
                await feedbackService.WarnAsync(errors.ToString());
                return;
            }

            User user = new User
            {
                Login = Login.Text,
                Password = Password.Password,
                RoleId = (Role.SelectedItem as Role).RoleId,
                LastInteractionDate = DateTime.Now,
                LastChangePasswordDate = DateTime.Now,
            };

            await repository.AddUserAsync(user);
            try
            {
                await repository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
                await feedbackService.InformAsync("Произошла ошибка ввода нового пользователя. " +
                    "Перезайдите на страницу и попробуйте ещё раз");
                return;
            }
            await feedbackService.InformAsync("Пользователь успешно введён");
            NavigationService.GoBack();
        }

        private async void OnLoad(object sender, RoutedEventArgs e)
        {
            List<Role> roles = await GetRolesAsync();
            Role.ItemsSource = roles;
            Role.SelectedItem = roles.FirstOrDefault();
        }
    }
}
