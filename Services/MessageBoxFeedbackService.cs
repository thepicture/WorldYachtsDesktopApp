using System.Threading.Tasks;
using System.Windows;

namespace WorldYachtsDesktopApp.Services
{
    public class MessageBoxFeedbackService : IFeedbackService
    {
        public async Task<bool> AskAsync(string question)
        {
            bool result = MessageBox.Show(question,
                                         "Вопрос",
                                         MessageBoxButton.YesNo,
                                         MessageBoxImage.Question) == MessageBoxResult.Yes;
            return await Task.FromResult(result);
        }

        public async Task InformAsync(string message)
        {
            MessageBoxResult result = MessageBox.Show(message,
                                         "Информация",
                                         MessageBoxButton.OK,
                                         MessageBoxImage.Question);
            await Task.FromResult(result);
        }

        public async Task InformErrorAsync(string message)
        {
            MessageBoxResult result = MessageBox.Show(message,
                                         "Ошибка",
                                         MessageBoxButton.OK,
                                         MessageBoxImage.Question);
            await Task.FromResult(result);
        }

        public async Task WarnAsync(string message)
        {
            MessageBoxResult result = MessageBox.Show(message,
                                         "Предупреждение",
                                         MessageBoxButton.OK,
                                         MessageBoxImage.Question);
            await Task.FromResult(result);
        }
    }
}
