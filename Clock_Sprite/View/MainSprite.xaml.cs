using System;
using System.Timers;
using System.Windows;
using System.Windows.Input;

namespace Clock_Sprite.View
{
    public partial class MainSprite : Window
    {
        Timer clockTimer;

        public MainSprite()
        {
            InitializeComponent();

            clockTimer = new Timer()
            {
                Interval = 1000,
                AutoReset = true,
            };

            clockTimer.Elapsed += clockTimer_Elapsed;
            clockTimer.Start();
        }

        private void clockTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() => { clockTB.Text = DateTime.Now.ToString("HH:mm:ss"); });
        }

        private void clock_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            var o = Math.Round(clockTB.FontSize, 0);
            if (e.Delta > 0)
            {
                if (o < 50)
                    clockTB.FontSize += 1;
                else
                    clockTB.FontSize = 50;
            }
            else
            {
                if (o > 20)
                    clockTB.FontSize -= 1;
                else
                    clockTB.FontSize = 20;
            }
        }

        private void info_Click(object sender, RoutedEventArgs e)
        {
            Topmost = !Topmost;
        }

        private void info_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            infoBTN.Visibility = Visibility.Collapsed;
            exitBTN.Visibility = Visibility.Visible;
        }

        private void info_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            var o = Math.Round(Opacity, 2);
            if (e.Delta > 0)
            {
                if (o < 1.0)
                    Opacity += 0.05;
                else
                    Opacity = 1.0;
            }
            else
            {
                if (o > 0.5)
                    Opacity -= 0.05;
                else
                    Opacity = 0.5;
            }
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void exit_MouseLeave(object sender, MouseEventArgs e)
        {
            infoBTN.Visibility = Visibility.Visible;
            exitBTN.Visibility = Visibility.Collapsed;
        }
    }
}
