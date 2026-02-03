using LightON_Final.ViewModels;
using System.Windows;

namespace LightON_Final
{
    public partial class App : System.Windows.Application
    {
        private static System.Threading.Mutex? _mutex;
        private const string MutexName = "LightON_SingleInstance_Mutex";
        
        private LightSettingsViewModel _viewModel = null!;
        private MainWindow _mainWindow = null!;
        private ControlPanel _controlPanel = null!;
        private System.Windows.Forms.NotifyIcon _notifyIcon = null!;

        protected override void OnStartup(StartupEventArgs e)
        {
            // Kill any existing LightON processes before starting
            var currentProcess = System.Diagnostics.Process.GetCurrentProcess();
            var existingProcesses = System.Diagnostics.Process.GetProcessesByName("LightON");
            foreach (var process in existingProcesses)
            {
                if (process.Id != currentProcess.Id)
                {
                    try
                    {
                        process.Kill();
                        process.WaitForExit(1000);
                    }
                    catch { }
                }
            }

            // Now acquire the mutex for this instance
            bool createdNew;
            _mutex = new System.Threading.Mutex(true, MutexName, out createdNew);

            base.OnStartup(e);

            _viewModel = new LightSettingsViewModel();

            _mainWindow = new MainWindow
            {
                DataContext = _viewModel
            };
            _mainWindow.Show();

            _controlPanel = new ControlPanel
            {
                DataContext = _viewModel
            };
            _controlPanel.Show();

            SetupTrayIcon();
            _viewModel.Load();
        }

        private void SetupTrayIcon()
        {
            _notifyIcon = new System.Windows.Forms.NotifyIcon();
            _notifyIcon.Icon = System.Drawing.SystemIcons.Information;
            _notifyIcon.Visible = true;
            _notifyIcon.Text = "LightON";
            
            var contextMenu = new System.Windows.Forms.ContextMenuStrip();
            contextMenu.Items.Add("Toggle Light", null, (s, e) => _viewModel.IsOn = !_viewModel.IsOn);
            contextMenu.Items.Add("Show Controls", null, (s, e) => { _controlPanel.Show(); _controlPanel.Activate(); });
            contextMenu.Items.Add("-");
            contextMenu.Items.Add("Exit", null, (s, e) => { _controlPanel.ForceClose(); this.Shutdown(); });

            _notifyIcon.ContextMenuStrip = contextMenu;
            _notifyIcon.DoubleClick += (s, e) => { _controlPanel.Show(); _controlPanel.Activate(); };
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _notifyIcon?.Dispose();
            _viewModel?.Save();
            _mutex?.ReleaseMutex();
            _mutex?.Dispose();
            base.OnExit(e);
        }
    }
}
