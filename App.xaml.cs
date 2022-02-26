using System.ComponentModel;
using System.Windows;
using WorldYachtsDesktopApp.Models.Entities;

namespace WorldYachtsDesktopApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application, INotifyPropertyChanged
    {
        private User user;

        public User User
        {
            get => user;
            set
            {
                user = value;
                PropertyChanged?
                    .Invoke(this,
                            new PropertyChangedEventArgs(nameof(User)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
