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

        private void clock_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            var o = Math.Round(clockTB.FontSize, 0);
            if (e.Delta > 0)
            {
                if (o < 80)
                    clockTB.FontSize += 5;
                else
                    clockTB.FontSize = 80;
            }
            else
            {
                if (o > 25)
                    clockTB.FontSize -= 5;
                else
                    clockTB.FontSize = 25;
            }
            clockTB.Padding = new Thickness(clockTB.FontSize / 5);
        }

        private void info_Click(object sender, RoutedEventArgs e)
        {
            Topmost = !Topmost;
        }

        private void info_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            infoBTN.Visibility = Visibility.Collapsed;
            closeBTN.Visibility = Visibility.Visible;
        }

        private void close_MouseLeave(object sender, MouseEventArgs e)
        {
            closeBTN.Visibility = Visibility.Collapsed;
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
