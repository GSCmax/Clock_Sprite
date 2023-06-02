using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace Clock_Sprite.View
{
    public partial class MainSprite : Window
    {
        public MainSprite()
        {
            InitializeComponent();
        }

        //Not use
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
            switch (e.ChangedButton)
            {
                case MouseButton.Left:
                    infoBTN.Visibility = Visibility.Collapsed;
                    closeBTN.Visibility = Visibility.Visible;
                    break;
                case MouseButton.Right:
                    infoBTN.Visibility = Visibility.Collapsed;
                    exitBTN.Visibility = Visibility.Visible;
                    break;
                default:
                    break;
            }
        }

        private void close_or_exit_MouseLeave(object sender, MouseEventArgs e)
        {
            closeBTN.Visibility = Visibility.Collapsed;
            exitBTN.Visibility = Visibility.Collapsed;
            infoBTN.Visibility = Visibility.Visible;
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

        private void close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }

    public class Bool2ResourceConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value)
                return Application.Current.FindResource(((string)parameter).Split('|')[0]);
            else
                return Application.Current.FindResource(((string)parameter).Split('|')[1]);
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
