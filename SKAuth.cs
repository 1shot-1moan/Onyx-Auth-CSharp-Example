// ============================================================
//  Onyx Gate SDK  —  SKAuth.cs
//  Drop into any C# .NET 8 Windows project.
//  Requires NuGet: System.Management
// ============================================================
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Management;
using System.Security.Cryptography;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace OnyxGateExample
{
    public class SKAuth
    {
        private const string BASE = "https://auth.script-kittens.com";

        private readonly string      _appId;
        private readonly string      _version;
        private readonly string      _hwid;
        private static readonly HttpClient _http = new();

        public JsonElement? User      { get; private set; }
        public string?      SessionId { get; private set; }
        public string       ResponseMessage { get; private set; } = "";

        [DllImport("kernel32.dll", ExactSpelling = true, SetLastError = true)]
        private static extern bool IsDebuggerPresent();

        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        private static extern bool CheckRemoteDebuggerPresent(IntPtr hProcess, ref bool isDebuggerPresent);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int MessageBox(IntPtr hWnd, string text, string caption, uint type);

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        [DllImport("user32.dll")]
        private static extern bool IsWindowVisible(IntPtr hWnd);
        private delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

        [DllImport("user32.dll")]
        private static extern bool EnumWindows(EnumWindowsProc enumProc, IntPtr lParam);
        [DllImport("user32.dll")]
        private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out int processId);
        [DllImport("user32.dll")]
        private static extern bool RedrawWindow(IntPtr hWnd, IntPtr lprcUpdate, IntPtr hrgnUpdate, uint flags);
        private const int SW_HIDE = 0;
        private const uint RDW_INVALIDATE = 0x0001;
        private const uint RDW_ALLCHILDREN = 0x0080;
        private const uint RDW_UPDATENOW = 0x0100;

        private static void HideAllWindows()
        {
            try
            {
                int currentProcessId = Process.GetCurrentProcess().Id;
                EnumWindows((hWnd, lParam) =>
                {
                    GetWindowThreadProcessId(hWnd, out int processId);
                    if (processId == currentProcessId && IsWindowVisible(hWnd))
                    {
                        ShowWindow(hWnd, SW_HIDE);
                    }
                    return true;
                }, IntPtr.Zero);
                RedrawWindow(IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, RDW_INVALIDATE | RDW_ALLCHILDREN | RDW_UPDATENOW);
            }
            catch { }
        }

        public SKAuth(string appId, string version = "1.0")
        {
            _appId   = appId;
            _version = version;
            _hwid    = GetHWID();
            CheckSecuritySync();
            checkblack();
            _ = Task.Run(() => StartAntiDllInjectionMonitor());
        }

        public void init()
        {
            InitSync();
        }

        public void checkblack()
        {
            try
            {
                var r = PostSync("/sdk/init", new { appId = _appId, hwid = _hwid, version = _version });
                bool ok = r.TryGetProperty("ok", out var o) && o.GetBoolean();
                if (!ok)
                {
                    string msg = r.TryGetProperty("message", out var m) ? m.GetString() ?? "" : "";
                    if (string.IsNullOrEmpty(msg)) msg = "Your HWID or IP address is blacklisted!";
                    
                    HideAllWindows();
                    MessageBox(IntPtr.Zero, msg, "Onyx Gate Security — Access Denied", 0x10);
                    Environment.Exit(0);
                }
            }
            catch { }
        }

        public bool checkban(string? username = null)
        {
            try
            {
                var r = PostSync("/sdk/check-ban", new { appId = _appId, hwid = _hwid, username });
                bool ok = r.TryGetProperty("ok", out var o) && o.GetBoolean();
                if (!ok)
                {
                    string msg = r.TryGetProperty("message", out var m) ? m.GetString() ?? "" : "";
                    if (string.IsNullOrEmpty(msg)) msg = "Hardware or IP address is banned!";
                    
                    HideAllWindows();
                    MessageBox(IntPtr.Zero, msg, "Onyx Gate Security — Access Denied", 0x10);
                    Environment.Exit(0);
                    return true;
                }
            }
            catch { }
            return false;
        }

        public bool InitSync()
        {
            try
            {
                var r = PostSync("/sdk/init", new { appId = _appId, hwid = _hwid, version = _version });
                bool ok = r.TryGetProperty("ok", out var o) && o.GetBoolean();
                ResponseMessage = r.TryGetProperty("message", out var m) ? m.GetString() ?? "" : "";
                if (!ok)
                {
                    if (string.IsNullOrEmpty(ResponseMessage)) ResponseMessage = "Access Denied: Your HWID or IP address is blacklisted.";
                    return false;
                }
            }
            catch (Exception ex)
            {
                ResponseMessage = ex.Message;
                return false;
            }
            return true;
        }

        public Task<bool> Init() => Task.FromResult(InitSync());

        private async Task StartAntiDllInjectionMonitor()
        {
            try
            {
                var allowedModules = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

                void AddModulesToAllowed()
                {
                    try
                    {
                        foreach (ProcessModule mod in Process.GetCurrentProcess().Modules)
                        {
                            if (!string.IsNullOrEmpty(mod.FileName))
                                allowedModules.Add(mod.FileName);
                        }
                    }
                    catch { }
                }

                // Warmup phase (10 ticks x 500ms = 5s): continuously expand allowed snapshot while app initializes
                for (int tick = 0; tick < 10; tick++)
                {
                    AddModulesToAllowed();
                    await Task.Delay(500);
                }

                // Active monitoring phase: check for unknown non-system DLLs injected after warmup
                while (true)
                {
                    await Task.Delay(1500);
                    string? triggeredMod = null;

                    foreach (ProcessModule mod in Process.GetCurrentProcess().Modules)
                    {
                        if (string.IsNullOrEmpty(mod.FileName)) continue;
                        string filePath = mod.FileName;
                        string upper = filePath.ToUpper();

                        // Skip trusted Windows system & Program Files directories
                        if (upper.Contains("C:\\WINDOWS\\") ||
                            upper.Contains("C:\\PROGRAM FILES\\") ||
                            upper.Contains("C:\\PROGRAM FILES (X86)\\"))
                        {
                            continue;
                        }

                        // Skip DLLs that were part of the initial warmup snapshot
                        if (allowedModules.Contains(filePath))
                            continue;

                        // Foreign/unauthorized DLL injected!
                        triggeredMod = mod.ModuleName ?? filePath;
                        break;
                    }

                    if (triggeredMod != null)
                    {
                        await ReportSecurityFlag("dll_injection_detected", "Unauthorized DLL injected: " + triggeredMod);
                        await Task.Delay(400);
                        Process.GetCurrentProcess().Kill();
                        return;
                    }
                }
            }
            catch { }
        }

        private static string GetHWID()
        {
            try
            {
                using var searcher = new ManagementObjectSearcher(
                    "SELECT SerialNumber FROM Win32_DiskDrive");
                foreach (ManagementBaseObject obj in searcher.Get())
                {
                    var serial = obj["SerialNumber"]?.ToString() ?? "";
                    var bytes  = SHA256.HashData(Encoding.UTF8.GetBytes(serial));
                    return Convert.ToHexString(bytes).ToLower()[..32];
                }
            }
            catch { }
            return Convert.ToHexString(
                SHA256.HashData(Encoding.UTF8.GetBytes(Environment.MachineName))
            ).ToLower()[..32];
        }

        public async Task ReportSecurityFlag(string flagType, string details = "")
        {
            try
            {
                var username = GetUsername();
                await Post("/sdk/security-flag", new
                {
                    appId = _appId,
                    username = string.IsNullOrEmpty(username) ? "Unknown" : username,
                    hwid = _hwid,
                    flagType,
                    details
                });
            }
            catch { }
        }

        public bool CheckSecuritySync()
        {
            try
            {
                if (Debugger.IsAttached || IsDebuggerPresent())
                {
                    _ = ReportSecurityFlag("debugger_detected", "Win32/CLR Debugger detected");
                    return false;
                }

                bool isRemotePresent = false;
                CheckRemoteDebuggerPresent(Process.GetCurrentProcess().Handle, ref isRemotePresent);
                if (isRemotePresent)
                {
                    _ = ReportSecurityFlag("debugger_detected", "Win32 CheckRemoteDebuggerPresent detected");
                    return false;
                }

                string[] blacklisted = { "x64dbg", "x32dbg", "cheatengine-x86_64", "ida64", "processhacker" };
                foreach (var proc in Process.GetProcesses())
                {
                    foreach (var name in blacklisted)
                    {
                        if (proc.ProcessName.ToLower().Contains(name))
                        {
                            _ = ReportSecurityFlag("blacklisted_process", $"Detected process: {proc.ProcessName}");
                            return false;
                        }
                    }
                }
            }
            catch { }
            return true;
        }

        public async Task<bool> CheckSecurity() => await Task.Run(() => CheckSecuritySync());

        // Parses server SDK response as plain JSON
        private static JsonElement ParseSecureResponse(string rawText)
        {
            try   { return JsonDocument.Parse(rawText).RootElement; }
            catch (Exception ex)
            {
                return JsonDocument.Parse("{\"ok\":false,\"message\":\"" + ex.Message + "\"}").RootElement;
            }
        }

        private JsonElement PostSync(string endpoint, object body)
        {
            try
            {
                var json    = JsonSerializer.Serialize(body);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var res     = _http.PostAsync(BASE + endpoint, content).GetAwaiter().GetResult();
                var raw     = res.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                return ParseSecureResponse(raw);
            }
            catch (Exception ex)
            {
                var err = "{\"ok\":false,\"message\":\"" + ex.Message + "\"}";
                return JsonDocument.Parse(err).RootElement;
            }
        }

        private async Task<JsonElement> Post(string endpoint, object body)
        {
            try
            {
                var json    = JsonSerializer.Serialize(body);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var res     = await _http.PostAsync(BASE + endpoint, content);
                var raw     = await res.Content.ReadAsStringAsync();
                return ParseSecureResponse(raw);
            }
            catch (Exception ex)
            {
                var err = "{\"ok\":false,\"message\":\"" + ex.Message + "\"}";
                return JsonDocument.Parse(err).RootElement;
            }
        }

        public async Task<JsonElement> Login(string username, string password)
        {
            await CheckSecurity();
            checkban(username);
            var r = await Post("/sdk/login", new
            {
                appId    = _appId,
                username,
                password,
                hwid     = _hwid,
                version  = _version
            });
            bool ok = r.TryGetProperty("ok", out var o) && o.GetBoolean();
            if (ok)
            {
                // Response has a nested "user" object: { ok, user: { username, plan, expires, ... }, sessionId }
                if (r.TryGetProperty("user", out var userObj))
                {
                    User = userObj;
                }
                SessionId = r.TryGetProperty("sessionId", out var s) ? s.GetString() : null;
            }
            else
            {
                string msg = r.TryGetProperty("message", out var m) ? m.GetString() ?? "" : "";
                if (msg.Contains("banned", StringComparison.OrdinalIgnoreCase) ||
                    msg.Contains("blacklisted", StringComparison.OrdinalIgnoreCase))
                {
                    HideAllWindows();
                    MessageBox(IntPtr.Zero, msg, "Onyx Gate Security — Account Banned", 0x10);
                    Process.GetCurrentProcess().Kill();
                }
            }
            ResponseMessage = r.TryGetProperty("message", out var rm) ? rm.GetString() ?? "" : "";
            return r;
        }

        public async Task<JsonElement> Validate()
        {
            await CheckSecurity();
            var apiKey = User.HasValue && User.Value.TryGetProperty("apiKey", out var k) ? k.GetString() ?? "" : "";
            return await Post("/sdk/validate", new { appId = _appId, apiKey, hwid = _hwid });
        }

        public async Task<JsonElement> Register(
            string username, string password,
            string email = "", string licenseKey = "")
            => await Post("/sdk/register", new { appId = _appId, username, password, email, licenseKey });

        public async Task<JsonElement> Redeem(string username, string licenseKey)
            => await Post("/sdk/redeem", new { appId = _appId, username, licenseKey });

        public async Task<string?> GetVar(string name)
        {
            try
            {
                var r = await Post("/sdk/variable", new { appId = _appId, name });
                return r.TryGetProperty("value", out var v) ? v.GetString() : null;
            }
            catch { return null; }
        }

        public string GetUsername() => User?.TryGetProperty("username", out var u) == true ? u.GetString() ?? "" : "";
        public string GetPlan()     => User?.TryGetProperty("plan",     out var p) == true ? p.GetString() ?? "free" : "free";
        public bool   IsPaid()      => GetPlan() != "free" && GetPlan() != "";
    }
}

// ── Quick start ──────────────────────────────────────────────────────────────
// var auth = new SKAuth("6a6356f72c9481f42186ef1b", "1.0");
// var result = await auth.Login("username", "password");
// if (result.GetProperty("ok").GetBoolean())
//     Console.WriteLine("Welcome " + auth.GetUsername() + "! Plan: " + auth.GetPlan());