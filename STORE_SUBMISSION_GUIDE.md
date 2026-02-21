# Microsoft Store Submission Guide for LightON

## Prerequisites

1. **Visual Studio 2022** with ".NET Desktop Development" workload
2. **Microsoft Partner Center Account** - [Register here](https://partner.microsoft.com/dashboard) ($19 one-time fee)
3. **Windows 10/11 SDK**

---

## Step 1: Create Windows Application Packaging Project

1. Open Visual Studio 2022
2. File → New → Project
3. Search for "Windows Application Packaging Project"
4. Name it: `LightON.Package`
5. Set Location: `C:\Extensions\LightON\`
6. Click Create
7. Set Target Version: **Windows 10, version 2004 (10.0.19041.0)** or higher
8. Set Minimum Version: **Windows 10, version 1903 (10.0.18362.0)**

## Step 2: Add LightON to Package

1. In Solution Explorer, right-click **Applications** folder (under `LightON.Package`)
2. Click **Add Reference**
3. Check `LightON_Final`
4. Click OK

## Step 3: Configure Package.appxmanifest

1. Double-click `Package.appxmanifest`
2. Fill in:
   - **Display Name**: LightON - Virtual Ring Light
   - **Publisher Display Name**: SATVIK
   - **Description**: A virtual ring light for video calls
   - **Package Name**: LightON.VirtualRingLight

3. **Visual Assets** tab:
   - Generate all required icon sizes from a 400x400 PNG
   - Splash screen, tile icons, etc.

4. **Capabilities** tab:
   - Leave unchecked unless needed (this app needs no special permissions)

## Step 4: Reserve App Name in Partner Center

1. Go to [Partner Center](https://partner.microsoft.com/dashboard)
2. Click **Apps and games** → **New product** → **App**
3. Reserve name: "LightON - Virtual Ring Light"
4. Copy the **Package/Identity/Name** and **Package/Identity/Publisher** values
5. Paste these into your `Package.appxmanifest`

## Step 5: Create MSIX Package

1. In Visual Studio, right-click `LightON.Package`
2. Click **Publish** → **Create App Packages**
3. Select **Microsoft Store as a new app name** or **Sideloading**
4. Sign in with your Partner Center account
5. Select Release configuration (x64)
6. Click **Create**

## Step 6: Test with Windows App Certification Kit

1. After package creation, click **Launch Windows App Certification Kit**
2. Wait for tests to complete (15-30 minutes)
3. Fix any failures before submitting

## Step 7: Upload to Partner Center

1. Go to Partner Center → Your App → **Packages**
2. Upload the `.msixupload` file
3. Complete all store listing sections:
   - Description
   - Screenshots (1366x768 minimum, 4+ recommended)
   - Age rating (select 3+, no mature content)
   - Category: **Utilities & Tools**
   - Pricing: **Free** or set price

## Step 8: Submit for Certification

1. Review all sections
2. Click **Submit to the Store**
3. Wait 1-3 business days for review

---

## Store Listing Content

### Short Description (200 chars)
A virtual ring light that adds soft lighting to your screen for better video calls and streaming.

### Full Description
LightON creates a glowing ring light effect around your screen to improve lighting in webcam footage. Perfect for:

• Video calls (Zoom, Teams, Google Meet)
• Live streaming (Twitch, YouTube)
• Content creation and recording

**Features:**
✓ Adjustable brightness
✓ Color temperature control (warm to cool)
✓ Customizable ring size and thickness
✓ Global hotkey (Ctrl+Shift+L)
✓ System tray integration
✓ Settings auto-save

No external light needed - uses your screen as the light source!

### Keywords
ring light, virtual light, webcam light, video call, zoom lighting, streaming light, screen light

### Privacy Policy
Not required - this app does not collect any user data.

---

## Compliance Checklist

- [ ] App works without internet connection
- [ ] App doesn't collect user data (no privacy policy needed)
- [ ] App doesn't require admin privileges
- [ ] All assets are original or properly licensed
- [ ] App installs and uninstalls cleanly
- [ ] App passes Windows App Certification Kit
- [ ] Age rating: 3+ (no mature content)
- [ ] No cryptocurrency mining or malicious behavior

