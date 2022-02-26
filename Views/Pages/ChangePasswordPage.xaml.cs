using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WorldYachtsDesktopApp.Models.Entities;
using WorldYachtsDesktopApp.Services;

namespace WorldYachtsDesktopApp.Views.Pages
{
    /// <summary>
    /// Interaction logic for ChangePasswordPage.xaml
    /// </summary>
    public partial class ChangePasswordPage : Page
    {
        private readonly IFeedbackService feedbackService =
            new MessageBoxFeedbackService();
        public ChangePasswordPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Изменяет пароль пользователя.
        /// </summary>
        private async void PerformChangePassword(object sender,
                                                 RoutedEventArgs e)
        {
            if ((App.Current as App).User.Password != CurrentPassword.Password
                || NewPassword.Password != NewPasswordCheck.Password)
            {
                await feedbackService.WarnAsync("текущий пароль "
                                                + "введен неверно "
                                                + "или новый пароль "
                                                + "не совпадает "
                                                + "с подтверждением");
                return;
            }
            try
            {
                await Task.Run(() =>
                {
                    using (WorldYachtsBaseEntities context =
                    new WorldYachtsBaseEntities())
                    {
                        User user = context.User.Find((App.Current as App).User.UserId);
                        user.Password = NewPassword.Password;
                        user.LastChangePasswordDate = DateTime.Now;
                        user.LastInteractionDate = DateTime.Now;
                        context.SaveChanges();
                    }
                });
                await feedbackService.InformAsync("Пароль изменён");
                NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
                await feedbackService.InformErrorAsync("Не удалось " +
                            "изменить пароль. Перезагрузите " +
                            "страницу и попробуйте " +
                            "ещё раз");
            }
        }
    }
}
