# Onyx Gate â€” C# WinForms Example

> Official C# SDK and WinForms example for [Onyx Gate Auth](https://auth.script-kittens.com) â€” the authentication platform built for cheat developers.

![C#](https://img.shields.io/badge/C%23-.NET%208-178600?style=flat&logo=csharp&logoColor=white)
![Platform](https://img.shields.io/badge/Platform-Windows-0078d4?style=flat&logo=windows&logoColor=white)
![NuGet](https://img.shields.io/badge/NuGet-System.Management-004880?style=flat)
![License](https://img.shields.io/badge/License-MIT-yellow?style=flat)

---

## What is Onyx Gate?

Onyx Gate is a KeyAuth-style authentication system built by Script Kittens. It gives your cheat or tool:

- **HWID Lock** â€” bind each user to one machine
- **License Keys** â€” generate, sell, and track keys from the dashboard
- **Live Sessions** â€” see who's online right now, kick them instantly
- **Blacklist** â€” ban HWIDs, IPs, or usernames with one click
- **Variables** â€” push values to your app at runtime without recompiling
- **Plan Gating** â€” free vs paid feature separation built in

---

## Files

| File | Purpose |
|---|---|
| `SKAuth.cs` | Core SDK â€” drop into **any** C# project |
| `LoginForm.cs` + `LoginForm.Designer.cs` | Login & Register form with live validation |
| `MainForm.cs` + `MainForm.Designer.cs` | Main cheat menu with plan-gated features |
| `Program.cs` | Entry point |
| `OnyxGate-CSharp.sln` | Visual Studio solution â€” open this |
| `OnyxGate-CSharp.csproj` | Project file (auto-restores NuGet) |

---

## Requirements

- Visual Studio 2022
- .NET 8 SDK â€” [download free at dot.net](https://dotnet.microsoft.com/download)
- NuGet: `System.Management` â€” **auto-restored on build, no manual install needed**

---

## Quick Start

**1. Open the solution**
```
OnyxGate-CSharp.sln â†’ Visual Studio 2022
```

**2. Set your App ID** â€” open `LoginForm.cs` and change line 11:
```csharp
private const string APP_ID = "YOUR_APP_ID_HERE";
```
Get your App ID from [auth.script-kittens.com](https://auth.script-kittens.com) â†’ Manage Apps â†’ Credentials.

**3. Build & Run**
```
Build â†’ Rebuild Solution â†’ F5
```
NuGet packages restore automatically on first build.

---

## What customers see

| Screen | Features |
|---|---|
| **Login tab** | Username + password, Enter key submits, live error clearing |
| **Register tab** | Live username validation, password strength meter, auto-formats license key as `SK-XXXX-XXXX-XXXX-XXXX` |
| **Main form** | Sidebar navigation, feature checkboxes, paid features locked for free users |

---

## Integrate SKAuth.cs into your own project

Copy `SKAuth.cs` into your project and add `System.Management` NuGet, then:

```csharp
// 1. Create auth object (once, at form load)
private SKAuth auth = new SKAuth("YOUR_APP_ID", "1.0");

// 2. Login button handler
var result = await auth.Login(txtUsername.Text, txtPassword.Text);
if (result.GetProperty("ok").GetBoolean()) {
    // auth.GetUsername() â€” logged in user
    // auth.GetPlan()     â€” "free" / "paid" / "vip" / "lifetime"
    // auth.IsPaid()      â€” true if paid plan
}

// 3. Gate features
if (auth.IsPaid()) {
    btnAimbot.Enabled = true;
} else {
    MessageBox.Show("Upgrade required.");
}
```

---

## Dashboard

Manage your users, keys, sessions, and blacklist at:
**[auth.script-kittens.com](https://auth.script-kittens.com)**

Buy keys or upgrade plans on our Discord:
**[discord.gg/tWwUSPh5GT](https://discord.gg/tWwUSPh5GT)**

---

## Other SDKs

| Language | Repo |
|---|---|
| C++ | [Onyx-Auth-CPP-Example](https://github.com/1shot-1moan/Onyx-Auth-CPP-Example) |
| Python | [Onyx-Auth-Python-Example](https://github.com/1shot-1moan/Onyx-Auth-Python-Example) |
