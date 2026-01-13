using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;

namespace LightON.ViewModels
{
    public class LightSettingsViewModel : INotifyPropertyChanged
    {
        private double _intensity = 0.8;
        private double _colorTemperature = 0.5; // 0.0 = Warm, 1.0 = Cool
        private System.Windows.Media.Color _lightColor;
        private bool _isOn = true;

        public LightSettingsViewModel()
        {
            UpdateColor();
        }

        public bool IsOn
        {
            get => _isOn;
            set { _isOn = value; OnPropertyChanged(); OnPropertyChanged(nameof(EffectiveOpacity)); }
        }

        public double Intensity
        {
            get => _intensity;
            set { _intensity = value; OnPropertyChanged(); OnPropertyChanged(nameof(EffectiveOpacity)); }
        }

        public double EffectiveOpacity => IsOn ? Intensity : 0;

        public double ColorTemperature
        {
            get => _colorTemperature;
            set
            {
                _colorTemperature = value;
                OnPropertyChanged();
                UpdateColor();
            }
        }

        public System.Windows.Media.Color LightColor
        {
            get => _lightColor;
            private set { _lightColor = value; OnPropertyChanged(); }
        }

        private void UpdateColor()
        {
            // Warm amber (255, 180, 50) to pure white (255, 255, 255)
            System.Windows.Media.Color warm = System.Windows.Media.Color.FromRgb(255, 180, 50);
            System.Windows.Media.Color cool = System.Windows.Media.Color.FromRgb(255, 255, 255);
            LightColor = Interpolate(warm, cool, _colorTemperature);
        }

        private System.Windows.Media.Color Interpolate(System.Windows.Media.Color c1, System.Windows.Media.Color c2, double factor)
        {
            byte r = (byte)(c1.R + (c2.R - c1.R) * factor);
            byte g = (byte)(c1.G + (c2.G - c1.G) * factor);
            byte b = (byte)(c1.B + (c2.B - c1.B) * factor);
            return System.Windows.Media.Color.FromArgb(255, r, g, b);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
