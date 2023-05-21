using Clock_Sprite.View;
using System.Windows;

namespace Clock_Sprite
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        MainSprite ms;
        int offset = 15;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            ms = new MainSprite();
            ms.Opacity = 0;
            ms.Show();

            var desktopWorkingArea = SystemParameters.WorkArea;
            ms.Left = desktopWorkingArea.Width - ms.ActualWidth - offset;
            ms.Top = offset - ms.Padding.Top;

            ms.Opacity = 1;
        }
    }
}
