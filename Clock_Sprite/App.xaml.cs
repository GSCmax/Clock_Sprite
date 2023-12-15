using Clock_Sprite.View;
using System;
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
            Interval = 100,
            AutoReset = true,
        };

        DateTime nowDateTime;

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

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

            clockTimer.Elapsed -= ClockTimer_Elapsed;
            clockTimer.Stop();
        }

        private void ClockTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            nowDateTime = DateTime.Now;
            Current.Dispatcher.Invoke(() =>
            {
                foreach (var w in Current.Windows)
                    if (w is MainSprite)
                        (w as MainSprite).clockTB.Text = nowDateTime.ToString((nowDateTime.Second % 2 == 0) ? "HH:mm:ss" : "HH mm ss");
            });
        }
    }
}
