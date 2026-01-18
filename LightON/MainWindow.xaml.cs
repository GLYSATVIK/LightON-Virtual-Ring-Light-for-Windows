using LightON_Final.Services;
using System;
using System.Windows;
using System.Windows.Interop;

namespace LightON_Final
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            // Make window size match the primary screen
            this.Width = SystemParameters.PrimaryScreenWidth;
            this.Height = SystemParameters.PrimaryScreenHeight;
            
            // Allow dragging the window by clicking anywhere on it
            this.MouseLeftButtonDown += (s, e) => this.DragMove();
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            // Enable click-through so users can click through the ring
            WindowServices.SetClickThrough(this);
        }
    }
}