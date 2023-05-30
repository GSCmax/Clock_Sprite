using System;
using System.Windows;
using System.Windows.Input;

namespace Clock_Sprite.View
{
    public partial class MainSprite : Window
    {
        public MainSprite()
        {
            InitializeComponent();
        }

        private void clock_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            var o = Math.Round(clockTB.FontSize, 0);
            if (e.Delta > 0)
            {
                if (o < 60)
                    clockTB.FontSize += 1;
                else
                    clockTB.FontSize = 60;
            }
            else
            {
                if (o > 24)
                    clockTB.FontSize -= 1;
                else
                    clockTB.FontSize = 24;
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

        private void exit_MouseLeave(object sender, MouseEventArgs e)
        {
            infoBTN.Visibility = Visibility.Visible;
            exitBTN.Visibility = Visibility.Collapsed;
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
            Close();
        }
    }
}
