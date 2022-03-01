using System.Collections.Generic;
using System.Windows.Controls;
using WorldYachtsDesktopApp.Models.Entities;

namespace WorldYachtsDesktopApp.Views.Pages.AdminPages
{
    /// <summary>
    /// Interaction logic for ChangeBoatsPricePage.xaml
    /// </summary>
    public partial class ChangeBoatsPricePage : Page
    {
        public ChangeBoatsPricePage(IEnumerable<Boat> boats)
        {
            InitializeComponent();
            Boats = boats;
        }

        public IEnumerable<Boat> Boats { get; set; }
    }
}
