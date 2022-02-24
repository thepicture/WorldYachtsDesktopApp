using System;
using System.Windows;
using System.Windows.Threading;

namespace WorldYachtsDesktopApp.Services
{
    public class UIElementTimeoutBlocker : ITimeoutBlocker<UIElement>
    {
        public void Block(UIElement target, TimeSpan timeout)
        {
            target.IsEnabled = false;
            DispatcherTimer timer = new DispatcherTimer(DispatcherPriority.Normal)
            {
                Interval = timeout,
            };
            timer.Tick += (s, e) =>
            {
                target.IsEnabled = true;
                timer.Stop();
            };
            timer.Start();
        }
    }
}
