using LightON_Final.Services;
using LightON_Final.ViewModels;
using System;
using System.Windows;
using System.Windows.Interop;

namespace LightON_Final
{
    public partial class ControlPanel : Window
    {
        private const int HOTKEY_ID = 9000;
        private bool _isExplicitExit = false;

        public ControlPanel()
        {
            InitializeComponent();
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            var helper = new WindowInteropHelper(this);
            var source = HwndSource.FromHwnd(helper.Handle);
            source?.AddHook(HwndHook);
            RegisterHotKey(helper.Handle);
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            if (!_isExplicitExit)
            {
                e.Cancel = true;
                this.WindowState = WindowState.Minimized;
            }
            base.OnClosing(e);
        }

        public void ForceClose()
        {
            _isExplicitExit = true;
            this.Close();
        }

        private void TitleBar_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            ForceClose();
            System.Windows.Application.Current.Shutdown();
        }

        protected override void OnClosed(EventArgs e)
        {
            var helper = new WindowInteropHelper(this);
            WindowServices.UnregisterHotKey(helper.Handle, HOTKEY_ID);
            base.OnClosed(e);
        }

        private void RegisterHotKey(IntPtr hwnd)
        {
             // Ctrl + Shift + L
             WindowServices.RegisterHotKey(hwnd, HOTKEY_ID, WindowServices.MOD_CONTROL | WindowServices.MOD_SHIFT, WindowServices.VK_L);
        }

        private IntPtr HwndHook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (msg == WindowServices.WM_HOTKEY)
            {
                if (wParam.ToInt32() == HOTKEY_ID)
                {
                    var vm = DataContext as LightSettingsViewModel;
                    if (vm != null)
                    {
                        vm.IsOn = !vm.IsOn;
                    }
                    handled = true;
                }
            }
            return IntPtr.Zero;
        }
    }
}
