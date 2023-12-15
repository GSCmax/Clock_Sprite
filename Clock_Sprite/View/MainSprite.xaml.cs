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
                if (o < 120)
                    o += 5;
                else
                    o = 120;
            }
            else
            {
                if (o > 25)
                    o -= 5;
                else
                    o = 25;
            }
            clockTB.FontSize = o;
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
                    o += 0.05;
                else
                    o = 1.0;
            }
            else
            {
                if (o > 0.5)
                    o -= 0.05;
                else
                    o = 0.5;
            }
            Opacity = o;
        }

        private void close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Border_ToolTipOpening(object sender, System.Windows.Controls.ToolTipEventArgs e)
        {
            int t = Application.Current.Windows.Count;
            for (int i = 0; i < t; i++)
                (Application.Current.Windows[i] as MainSprite).title_Run.Text = $"Clock Sprite [{i + 1}/{t}] 使用技巧：";
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

    public class FontSize2PaddingConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (double)value / 5;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class FontSize2CornerRadiusConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double cr = (double)value / 5 * 1.6;
            return new CornerRadius(0, cr, cr, cr);
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
