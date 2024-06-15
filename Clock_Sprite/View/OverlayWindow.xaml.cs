using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace Clock_Sprite.View
{
    /// <summary>
    /// OverlayWindow.xaml 的交互逻辑
    /// </summary>
    public partial class OverlayWindow : Window
    {
        public OverlayWindow()
        {
            InitializeComponent();
            StartBlinking();
        }

        private void StartBlinking()
        {
            var blinkAnimation = new DoubleAnimation
            {
                From = 0.5,
                To = 0.25,
                Duration = TimeSpan.FromSeconds(0.5),
                AutoReverse = true,
                RepeatBehavior = RepeatBehavior.Forever
            };

            BeginAnimation(OpacityProperty, blinkAnimation);
        }
    }
}
