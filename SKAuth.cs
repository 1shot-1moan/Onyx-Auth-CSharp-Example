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
        private string _plan    = "free";
        private string _expires = "";

        [DllImport("kernel32.dll", ExactSpelling = true, SetLastError = true)]
        private static extern bool IsDebuggerPresent();

        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        private static extern bool CheckRemoteDebuggerPresent(IntPtr hProcess, ref bool isDebuggerPresent);

        public SKAuth(string appId, string version = "1.0")
        {
            _appId   = appId;
            _version = version;
            _hwid    = GetHWID();
            _ = Task.Run(() => CheckSecurity());
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

        public async Task<bool> CheckSecurity()
        {
            try
            {
                if (Debugger.IsAttached || IsDebuggerPresent())
                {
                    await ReportSecurityFlag("debugger_detected", "Win32/CLR Debugger detected");
                    return false;
                }

                bool isRemotePresent = false;
                CheckRemoteDebuggerPresent(Process.GetCurrentProcess().Handle, ref isRemotePresent);
                if (isRemotePresent)
                {
                    await ReportSecurityFlag("debugger_detected", "Win32 CheckRemoteDebuggerPresent detected");
                    return false;
                }

                string[] blacklisted = { "x64dbg", "x32dbg", "cheatengine-x86_64", "ida64", "processhacker" };
                foreach (var proc in Process.GetProcesses())
                {
                    foreach (var name in blacklisted)
                    {
                        if (proc.ProcessName.ToLower().Contains(name))
                        {
                            await ReportSecurityFlag("blacklisted_process", $"Detected process: {proc.ProcessName}");
                            return false;
                        }
                    }
                }
            }
            catch { }
            return true;
        }

        private async Task<JsonElement> Post(string endpoint, object body)
        {
            try
            {
                var json    = JsonSerializer.Serialize(body);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var res     = await _http.PostAsync(BASE + endpoint, content);
                var raw     = await res.Content.ReadAsStringAsync();
                return JsonDocument.Parse(raw).RootElement;
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
            var r = await Post("/sdk/login", new
            {
                appId    = _appId,
                username,
                password,
                hwid     = _hwid,
                version  = _version
            });
            if (r.GetProperty("ok").GetBoolean())
            {
                User      = r.GetProperty("user");
                SessionId = r.TryGetProperty("sessionId", out var s) ? s.GetString() : null;
                _plan     = User?.TryGetProperty("plan", out var pv) == true ? pv.GetString() ?? "free" : "free";
            }
            return r;
        }

        public async Task<JsonElement> Validate()
        {
            await CheckSecurity();
            var apiKey = User.HasValue && User.Value.TryGetProperty("apiKey", out var k)
                ? k.GetString() ?? "" : "";
            var r = await Post("/sdk/validate", new { appId = _appId, apiKey, hwid = _hwid });
            if (r.GetProperty("ok").GetBoolean())
            {
                if (r.TryGetProperty("plan",    out var p)) _plan    = p.GetString() ?? _plan;
                if (r.TryGetProperty("expires", out var e)) _expires = e.GetString() ?? _expires;
            }
            return r;
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
                var res = await _http.GetAsync(
                    $"{BASE}/sdk/variable?appId={_appId}&name={name}");
                var doc = JsonDocument.Parse(await res.Content.ReadAsStringAsync());
                return doc.RootElement.TryGetProperty("value", out var v) ? v.GetString() : null;
            }
            catch { return null; }
        }

        public string GetUsername() => User?.GetProperty("username").GetString() ?? "";
        public string GetPlan()     => _plan;
        public bool   IsPaid()
        {
            var p = GetPlan();
            return p != "free" && p != "";
        }
    }
}

// ── Quick start ──────────────────────────────────────────────────────────────
// var auth = new SKAuth("6a6356f72c9481f42186ef1b", "1.0");
// var result = await auth.Login("username", "password");
// if (result.GetProperty("ok").GetBoolean())
//     Console.WriteLine("Welcome " + auth.GetUsername() + "! Plan: " + auth.GetPlan());
