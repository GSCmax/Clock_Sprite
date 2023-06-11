using Clock_Sprite.View;
using System;
using System.Globalization;
using System.Timers;
using System.Windows;

namespace Clock_Sprite
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        Timer clockTimer = new Timer()
        {
            Interval = 1000,
            AutoReset = true,
        };

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var ss = System.Windows.Forms.Screen.AllScreens;
            int offset = 5;

            for (int i = 0; i < ss.Length; i++)
            {
                MainSprite ms = new MainSprite();
                ms.Opacity = 0;
                ms.Show();

                ms.Left = ss[i].WorkingArea.Right - ms.ActualWidth - offset;
                ms.Top = ss[i].WorkingArea.Top + offset;
                WindowAttach.SetSnapDistance(ms, offset);

                ms.Opacity = 1;
            }

            clockTimer.Elapsed += ClockTimer_Elapsed;
            clockTimer.Start();
        }

        private void ClockTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            DateTime nowDateTime = DateTime.Now;
            Current.Dispatcher.Invoke(() =>
            {
                foreach (var w in Current.Windows)
                {
                    (w as MainSprite).timeTB.Text = nowDateTime.ToString("HH:mm:ss");
                    (w as MainSprite).dateTB.Text = nowDateTime.ToString("yy.MM.dd", new CultureInfo("en-US"));
                    (w as MainSprite).weekTB.Text = nowDateTime.ToString("ddd", new CultureInfo("en-US"));
                }
            });
        }
    }
}
