using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;
using WorldYachtsDesktopApp.Services;

namespace WorldYachtsDesktopApp.Controls
{
    /// <summary>
    /// <see cref="TextBox"/>, принимающий только целочисленные значения.
    /// </summary>
    public class IntegerTextBox : TextBox
    {
        protected override async void OnTextInput(TextCompositionEventArgs e)
        {
            base.OnTextInput(e);
            if (!Regex.IsMatch(e.Text, @"[0-9]+"))
            {
                e.Handled = true;
                await new MessageBoxFeedbackService().WarnAsync("В данном поле " +
                    "допустимы только целочисленные значения");
            }
        }
    }
}
