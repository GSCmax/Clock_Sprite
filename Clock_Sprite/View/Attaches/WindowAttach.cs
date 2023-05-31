using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;

namespace Clock_Sprite.View
{
    public struct WindowRectangle
    {
        public double X;
        public double Y;
        public double Width;
        public double Height;
    }

    public static class WindowAttach
    {
        #region IsDragElement
        public static readonly DependencyProperty IsDragElementProperty = DependencyProperty.RegisterAttached("IsDragElement", typeof(bool), typeof(WindowAttach), new PropertyMetadata(false, OnIsDragElementChanged));

        public static void SetIsDragElement(DependencyObject element, bool value) => element.SetValue(IsDragElementProperty, value);

        public static bool GetIsDragElement(DependencyObject element) => (bool)element.GetValue(IsDragElementProperty);

        private static void OnIsDragElementChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is UIElement ctl)
            {
                if ((bool)e.NewValue)
                {
                    //ctl.MouseLeftButtonDown += DragElement_MouseLeftButtonDown;

                    ctl.PreviewMouseLeftButtonDown += DragElement_PreviewMouseLeftButtonDown;
                    ctl.PreviewMouseMove += DragElement_PreviewMouseMove;
                    ctl.PreviewMouseLeftButtonUp += DragElement_PreviewMouseLeftButtonUp;
                    //ctl.KeyDown += DragElement_KeyDown;
                }
                else
                {
                    //ctl.MouseLeftButtonDown -= DragElement_MouseLeftButtonDown;

                    ctl.PreviewMouseLeftButtonDown -= DragElement_PreviewMouseLeftButtonDown;
                    ctl.PreviewMouseMove -= DragElement_PreviewMouseMove;
                    ctl.PreviewMouseLeftButtonUp -= DragElement_PreviewMouseLeftButtonUp;
                    //ctl.KeyDown -= DragElement_KeyDown;
                }
            }
        }

        private static void DragElement_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is DependencyObject obj && e.ButtonState == MouseButtonState.Pressed)
            {
                System.Windows.Window.GetWindow(obj).DragMove();
            }
        }

        private static Point _pressedPosition;
        private static bool _isDragMoved = false;

        private static void DragElement_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (sender is DependencyObject obj)
            {
                _pressedPosition = e.GetPosition(System.Windows.Window.GetWindow(obj));
            }
        }

        private static void DragElement_PreviewMouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (sender is DependencyObject obj && e.LeftButton == MouseButtonState.Pressed && _pressedPosition != e.GetPosition(System.Windows.Window.GetWindow(obj)))
            {
                _isDragMoved = true;
                System.Windows.Window.GetWindow(obj).DragMove();
            }
        }

        private static void DragElement_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (_isDragMoved)
            {
                _isDragMoved = false;
                e.Handled = true;
            }
        }

        private static void DragElement_KeyDown(object sender, KeyEventArgs e)
        {
            if (sender is DependencyObject obj)
            {
                switch (e.Key)
                {
                    case Key.Up:
                        System.Windows.Window.GetWindow(obj).Top--;
                        e.Handled = true;
                        break;
                    case Key.Down:
                        System.Windows.Window.GetWindow(obj).Top++;
                        e.Handled = true;
                        break;
                    case Key.Left:
                        System.Windows.Window.GetWindow(obj).Left--;
                        e.Handled = true;
                        break;
                    case Key.Right:
                        System.Windows.Window.GetWindow(obj).Left++;
                        e.Handled = true;
                        break;
                }
            }
        }
        #endregion

        #region IgnoreAltF4
        public static readonly DependencyProperty IgnoreAltF4Property = DependencyProperty.RegisterAttached("IgnoreAltF4", typeof(bool), typeof(WindowAttach), new PropertyMetadata(false, OnIgnoreAltF4Changed));

        public static void SetIgnoreAltF4(DependencyObject element, bool value) => element.SetValue(IgnoreAltF4Property, value);

        public static bool GetIgnoreAltF4(DependencyObject element) => (bool)element.GetValue(IgnoreAltF4Property);

        private static void OnIgnoreAltF4Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is System.Windows.Window window)
            {
                if ((bool)e.NewValue)
                {
                    window.PreviewKeyDown += Window_PreviewKeyDown;
                }
                else
                {
                    window.PreviewKeyDown -= Window_PreviewKeyDown;
                }
            }
        }

        private static void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.System && e.SystemKey == Key.F4)
            {
                e.Handled = true;
            }
        }
        #endregion

        #region SnapToScreenEdge
        public static readonly DependencyProperty SnapToScreenEdgeProperty = DependencyProperty.RegisterAttached("SnapToScreenEdge", typeof(bool), typeof(WindowAttach), new PropertyMetadata(false, OnSnapToScreenEdgeChanged));

        public static void SetSnapToScreenEdge(DependencyObject element, bool value) => element.SetValue(SnapToScreenEdgeProperty, value);

        public static bool GetSnapToScreenEdge(DependencyObject element) => (bool)element.GetValue(SnapToScreenEdgeProperty);

        private static void OnSnapToScreenEdgeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is System.Windows.Window window)
            {
                if ((bool)e.NewValue)
                {
                    window.SizeChanged += Window_SizeChanged;
                    window.MouseMove += Window_MouseMove;
                }
                else
                {
                    window.SizeChanged -= Window_SizeChanged;
                    window.MouseMove -= Window_MouseMove;
                }
            }
        }

        private static void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            SnapToScreenEdge(sender as System.Windows.Window);
        }

        private static void Window_MouseMove(object sender, MouseEventArgs e)
        {
            SnapToScreenEdge(sender as System.Windows.Window);
        }

        private static WindowRectangle prevRectangle = new WindowRectangle { X = .0, Y = .0, Width = .0, Height = .0 };

        private static void SnapToScreenEdge(System.Windows.Window sender)
        {
            // 获取当前窗口的位置和大小
            double windowLeft = sender.Left;
            double windowTop = sender.Top;
            double windowWidth = sender.ActualWidth;
            double windowHeight = sender.ActualHeight;

            // 判断较上一次是否有更新
            if (windowLeft != prevRectangle.X || windowTop != prevRectangle.Y || windowWidth != prevRectangle.Width || windowHeight != prevRectangle.Height)
            {
                // 获取当前窗口所在的工作区大小（不包括任务栏）
                IntPtr hwnd = new WindowInteropHelper(sender).Handle;
                var workArea = System.Windows.Forms.Screen.FromHandle(hwnd).WorkingArea;

                // 计算边缘吸附的阈值范围
                double snapLeft = workArea.Left + GetSnapDistance(sender);
                double snapTop = workArea.Top + GetSnapDistance(sender);
                double snapRight = workArea.Right - windowWidth - GetSnapDistance(sender);
                double snapBottom = workArea.Bottom - windowHeight - GetSnapDistance(sender);

                // 判断窗口是否需要进行边缘吸附
                bool shouldSnap = false;

                if (windowLeft < snapLeft)
                {
                    windowLeft = snapLeft;
                    shouldSnap = true;
                }
                else if (windowLeft > snapRight)
                {
                    windowLeft = snapRight;
                    shouldSnap = true;
                }

                if (windowTop < snapTop)
                {
                    windowTop = snapTop;
                    shouldSnap = true;
                }
                else if (windowTop > snapBottom)
                {
                    windowTop = snapBottom;
                    shouldSnap = true;
                }

                // 如果需要进行边缘吸附，则更新窗口的位置
                if (shouldSnap)
                {
                    sender.Left = windowLeft;
                    sender.Top = windowTop;
                }

                prevRectangle = new WindowRectangle { X = windowLeft, Y = windowTop, Width = windowWidth, Height = windowHeight };
            }
        }
        #endregion

        #region SnapDistance
        public static readonly DependencyProperty SnapDistanceProperty = DependencyProperty.RegisterAttached("SnapDistance", typeof(int), typeof(WindowAttach), new PropertyMetadata(20));

        public static void SetSnapDistance(DependencyObject element, int value) => element.SetValue(SnapDistanceProperty, value);

        public static int GetSnapDistance(DependencyObject element) => (int)element.GetValue(SnapDistanceProperty);
        #endregion

        #region ShowInTaskManager
        public static readonly DependencyProperty ShowInTaskManagerProperty = DependencyProperty.RegisterAttached("ShowInTaskManager", typeof(bool), typeof(WindowAttach), new PropertyMetadata(true, OnShowInTaskManagerChanged));

        public static void SetShowInTaskManager(DependencyObject element, bool value) => element.SetValue(ShowInTaskManagerProperty, value);

        public static bool GetShowInTaskManager(DependencyObject element) => (bool)element.GetValue(ShowInTaskManagerProperty);

        private static void OnShowInTaskManagerChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is System.Windows.Window window)
            {
                var v = (bool)e.NewValue;
                window.SetCurrentValue(System.Windows.Window.ShowInTaskbarProperty, v);

                if (v)
                {
                    window.SourceInitialized -= Window_SourceInitialized;
                }
                else
                {
                    window.SourceInitialized += Window_SourceInitialized;
                }
            }
        }

        private static void Window_SourceInitialized(object sender, EventArgs e)
        {
            if (sender is System.Windows.Window window)
            {
                var _ = new WindowInteropHelper(window)
                {
                    Owner = GetDesktopWindow()
                };
            }
        }

        [DllImport("user32.dll")]
        internal static extern IntPtr GetDesktopWindow();
        #endregion

        #region HideWhenClosing
        public static readonly DependencyProperty HideWhenClosingProperty = DependencyProperty.RegisterAttached("HideWhenClosing", typeof(bool), typeof(WindowAttach), new PropertyMetadata(false, OnHideWhenClosingChanged));

        public static void SetHideWhenClosing(DependencyObject element, bool value) => element.SetValue(HideWhenClosingProperty, value);

        public static bool GetHideWhenClosing(DependencyObject element) => (bool)element.GetValue(HideWhenClosingProperty);

        private static void OnHideWhenClosingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is System.Windows.Window window)
            {
                var v = (bool)e.NewValue;
                if (v)
                {
                    window.Closing += Window_Closing;
                }
                else
                {
                    window.Closing -= Window_Closing;
                }
            }
        }

        private static void Window_Closing(object sender, CancelEventArgs e)
        {
            if (sender is System.Windows.Window window)
            {
                window.Hide();
                e.Cancel = true;
            }
        }
        #endregion

        #region ExtendContentToNonClientArea
        public static readonly DependencyProperty ExtendContentToNonClientAreaProperty = DependencyProperty.RegisterAttached("ExtendContentToNonClientArea", typeof(bool), typeof(WindowAttach), new PropertyMetadata(false));

        public static void SetExtendContentToNonClientArea(DependencyObject element, bool value) => element.SetValue(ExtendContentToNonClientAreaProperty, value);

        public static bool GetExtendContentToNonClientArea(DependencyObject element) => (bool)element.GetValue(ExtendContentToNonClientAreaProperty);
        #endregion
    }
}
