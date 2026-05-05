# Onyx Gate — C# WinForms Example

## Requirements
- Visual Studio 2022
- .NET 8 SDK (free download at dot.net)
- No NuGet packages needed

## Setup (2 steps)
1. Open `OnyxGate-CSharp.sln` in Visual Studio
2. Open `LoginForm.cs` — change `APP_ID` to your App ID from the dashboard
3. Build → Release | x64 → Run

## Files
| File | Purpose |
|------|---------|
| `SKAuth.cs` | SDK — drop into any C# project |
| `LoginForm.cs` | Login + Register form |
| `MainForm.cs` | Main cheat menu — edit this |
| `Program.cs` | Entry point — don't edit |
| `OnyxGate-CSharp.sln` | Visual Studio solution |
| `OnyxGate-CSharp.csproj` | Project file |

## Add to your own WinForms project
```csharp
// 1. Copy SKAuth.cs into your project
// 2. Create the auth object:
private SKAuth auth = new SKAuth("YOUR_APP_ID", "1.0");

// 3. Call login:
var result = await auth.Login(username, password);
if (result.GetProperty("ok").GetBoolean()) {
    // success
}

// 4. Check plan:
if (auth.IsPaid()) { /* paid only */ }
```
