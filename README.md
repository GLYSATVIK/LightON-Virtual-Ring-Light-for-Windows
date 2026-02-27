# LightON - Virtual Ring Light for Windows

[![Platform](https://img.shields.io/badge/platform-Windows%2010%20%7C%2011-blue.svg)](https://microsoft.com/windows)
[![.NET](https://img.shields.io/badge/.NET-8.0--windows-purple.svg)](https://dotnet.microsoft.com/)
[![License](https://img.shields.io/badge/license-MIT-green.svg)](LICENSE)
[![PRs Welcome](https://img.shields.io/badge/PRs-welcome-brightgreen.svg)](https://github.com/GLYSATVIK/LightON-Virtual-Ring-Light-for-Windows/pulls)

A sleek, lightweight virtual ring light utility for Windows designed to illuminate your face during video calls, streaming, and content creation using your screen as a soft, customizable light source. No physical ring light or external hardware required!

---

## ✨ Features

- 🌟 **5-Layer Concentric Soft Glow Ring**: Custom multi-stage blur borders create an authentic ring light glow without harsh specular artifacts.
- 🎨 **Color Temperature Tuning**: Fluidly slide between warm amber (3000K) and pure cool daylight (6500K).
- 💡 **Precision Dimmer**: Adjust light intensity from subtle ambient fill to high-output key lighting.
- 📐 **Customizable Geometry**: Dynamically modify both ring diameter (screen coverage) and stroke thickness in real-time.
- 👻 **Click-Through Screen Overlay**: Utilizes Win32 `WS_EX_TRANSPARENT` and `WS_EX_TOOLWINDOW` styles so the light never interferes with your mouse clicks or workflow.
- ⌨️ **Global Hotkey**: Press **`Ctrl + Shift + L`** anywhere in Windows to toggle the light on or off instantly.
- 📌 **System Tray Daemon**: Minimizes cleanly to the Windows notification tray with quick toggle, control launcher, and exit options.
- 💾 **Automatic Settings Persistence**: Automatically saves and restores your preferred brightness, color temperature, and ring geometry via JSON.
- 🔒 **Single-Instance Protection**: Enforced with a Windows Mutex to ensure optimal resource utilization and prevent duplicate instances.

---

## 🖥️ Keyboard Shortcuts

| Shortcut | Action | Description |
| :--- | :--- | :--- |
| **`Ctrl + Shift + L`** | **Toggle Ring Light** | Turn the overlay on or off globally from any application |
| **Double-Click Tray** | **Show Controls** | Bring the floating control panel to the foreground |

---

## 🏛️ Architecture & Project Structure

The project follows a clean **MVVM (Model-View-ViewModel)** architectural pattern built on top of modern .NET 8 WPF:

```
LightON/
├── LightON/                          # Main Application Project
│   ├── Converters/
│   │   └── ThicknessMultiplierConverter.cs # Dynamic ring layer scaling
│   ├── Services/
│   │   └── WindowServices.cs         # Win32 P/Invoke interop (Click-through & Hotkeys)
│   ├── ViewModels/
│   │   └── LightSettingsViewModel.cs # Reactive MVVM state & color interpolation
│   ├── App.xaml / App.xaml.cs        # Mutex, System Tray, Hotkey hooks, Lifecycle
│   ├── MainWindow.xaml / .cs         # Full-screen multi-layer glow overlay
│   ├── ControlPanel.xaml / .cs       # Glassmorphism floating controller UI
│   └── lighton_settings.json         # User preferences state store
├── LightON.Package/                  # Windows Application Packaging (MSIX)
│   ├── Images/                       # Complete Store asset suite (Tiles, Icons, Splash)
│   ├── Package.appxmanifest          # UWP/Win32 MSIX manifest & capabilities
│   └── Package.StoreAssociation.xml  # Microsoft Partner Center store association
├── STORE_SUBMISSION_GUIDE.md         # Step-by-step Microsoft Store deployment guide
└── LightON.sln                       # Solution entry point
```

---

## 🚀 Getting Started

### Prerequisites

- **Windows 10** (build 19041+) or **Windows 11**
- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) or later
- Visual Studio 2022 (with *.NET Desktop Development* and *Windows Application Packaging Project* workloads)

### Building from Command Line

```powershell
# Clone the repository
git clone https://github.com/GLYSATVIK/LightON-Virtual-Ring-Light-for-Windows.git
cd LightON-Virtual-Ring-Light-for-Windows

# Build the project
dotnet build LightON\LightON.csproj -c Release

# Run the executable
.\LightON\bin\Release\net8.0-windows\LightON.exe
```

---

## 📦 Store Packaging & Submission

LightON includes a dedicated **Windows Application Packaging Project (`LightON.Package`)** for creating signed `.msix` and `.msixupload` store bundles.

Refer to [`STORE_SUBMISSION_GUIDE.md`](STORE_SUBMISSION_GUIDE.md) for full instructions on Microsoft Partner Center onboarding, Windows App Certification Kit (WACK) validation, and store release checklists.

---

## 📄 License

This project is licensed under the [MIT License](LICENSE).
