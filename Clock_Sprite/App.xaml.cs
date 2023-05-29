using Clock_Sprite.View;
using System.Windows;

namespace Clock_Sprite
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var ss = System.Windows.Forms.Screen.AllScreens;
            int offset = 15;

            for (int i = 0; i < ss.Length; i++)
            {
                MainSprite ms = new MainSprite();
                ms.Opacity = 0;
                ms.Show();

                ms.Left = ss[i].WorkingArea.Right - ms.ActualWidth - offset;
                ms.Top = ss[i].WorkingArea.Top + offset;
                ms.title_Run.Text = "Clock Sprite [" + (i + 1) + "/" + ss.Length + "] 使用技巧：";

                ms.Opacity = 1;
            }
        }
    }
}
