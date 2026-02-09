using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace LightON_Final.ViewModels
{
    public class LightSettingsViewModel : INotifyPropertyChanged
    {
        private double _intensity = 0.8;
        private double _colorTemperature = 0.5; 
        private System.Windows.Media.Color _lightColor;
        private bool _isOn = true;
        private double _ringSize = 0.6;  // Start at 60% of screen
        private double _ringThickness = 0.5;  // Medium thickness
        private const string SETTINGS_FILE = "lighton_settings.json";

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

        public double RingSize
        {
            get => _ringSize;
            set { _ringSize = value; OnPropertyChanged(); }
        }

        public double RingThickness
        {
            get => _ringThickness;
            set { _ringThickness = value; OnPropertyChanged(); }
        }

        public System.Windows.Media.Color LightColor
        {
            get => _lightColor;
            private set { _lightColor = value; OnPropertyChanged(); }
        }

        private void UpdateColor()
        {
            // Warm amber to pure white
            System.Windows.Media.Color warm = System.Windows.Media.Color.FromRgb(255, 180, 50);   // Warm amber
            System.Windows.Media.Color cool = System.Windows.Media.Color.FromRgb(255, 255, 255); // Pure white
            LightColor = Interpolate(warm, cool, _colorTemperature);
        }

        private System.Windows.Media.Color Interpolate(System.Windows.Media.Color c1, System.Windows.Media.Color c2, double factor)
        {
            byte r = (byte)(c1.R + (c2.R - c1.R) * factor);
            byte g = (byte)(c1.G + (c2.G - c1.G) * factor);
            byte b = (byte)(c1.B + (c2.B - c1.B) * factor);
            return System.Windows.Media.Color.FromArgb(255, r, g, b);
        }

        public void Save()
        {
            try
            {
                var data = new { Intensity, ColorTemperature, IsOn, RingSize, RingThickness };
                var json = System.Text.Json.JsonSerializer.Serialize(data);
                System.IO.File.WriteAllText(SETTINGS_FILE, json);
            }
            catch { }
        }

        public void Load()
        {
            try
            {
                if (System.IO.File.Exists(SETTINGS_FILE))
                {
                    var json = System.IO.File.ReadAllText(SETTINGS_FILE);
                    var data = System.Text.Json.JsonSerializer.Deserialize<SavedSettings>(json);
                    if (data != null)
                    {
                        Intensity = data.Intensity;
                        ColorTemperature = data.ColorTemperature;
                        IsOn = data.IsOn;
                        RingSize = data.RingSize;
                        RingThickness = data.RingThickness;
                    }
                }
            }
            catch { }
        }

        private class SavedSettings
        {
            public double Intensity { get; set; }
            public double ColorTemperature { get; set; }
            public bool IsOn { get; set; }
            public double RingSize { get; set; } = 0.6;
            public double RingThickness { get; set; } = 0.5;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
