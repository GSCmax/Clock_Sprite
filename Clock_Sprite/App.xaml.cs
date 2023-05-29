using Clock_Sprite.View;
using System.Windows;

namespace Clock_Sprite
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        int offset = 15;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            foreach (System.Windows.Forms.Screen screen in System.Windows.Forms.Screen.AllScreens)
            {
                MainSprite ms = new MainSprite();
                ms.Opacity = 0;
                ms.Show();

                ms.Left = screen.WorkingArea.Right - ms.ActualWidth - offset;
                ms.Top = screen.WorkingArea.Top + offset;

                ms.Opacity = 1;
            }
        }
    }
}
