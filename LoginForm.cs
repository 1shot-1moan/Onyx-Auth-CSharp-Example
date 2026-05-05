using System;
using System.Linq;
using System.Windows.Forms;

namespace OnyxGateExample
{
    public partial class LoginForm : Form
    {
        // ╔══════════════════════════════════════════╗
        // ║  ONLY CHANGE THESE TWO LINES             ║
        private const string APP_ID = "YOUR_APP_ID_HERE";
        private const string APP_VERSION = "1.0";
        // ╚══════════════════════════════════════════╝

        private readonly SKAuth _auth = new(APP_ID, APP_VERSION);

        public LoginForm()
        {
            InitializeComponent();
            AcceptButton = btnLogin;
        }

        // ─────────────────────────────────────────────────────────────────────
        //  LOGIN TAB — event handlers
        // ─────────────────────────────────────────────────────────────────────

        // Clears any error message as soon as the user starts typing
        private void txtUser_TextChanged(object sender, EventArgs e)
        {
            lblError.Text = "";
        }

        private void txtPass_TextChanged(object sender, EventArgs e)
        {
            lblError.Text = "";
        }

        // Submits login when Sign In is clicked
        private async void btnLogin_Click(object sender, EventArgs e)
        {
            lblError.Text = "";

            if (string.IsNullOrWhiteSpace(txtUser.Text) ||
                string.IsNullOrWhiteSpace(txtPass.Text))
            {
                lblError.Text = "Enter your username and password.";
                return;
            }

            btnLogin.Enabled = false;
            btnLogin.Text    = "Authenticating...";

            var result = await _auth.Login(txtUser.Text.Trim(), txtPass.Text);

            if (result.GetProperty("ok").GetBoolean())
            {
                // ✓ Open main form — swap MainForm with your own form name if needed
                var main = new MainForm(_auth);
                // When main form closes, exit the whole app (prevents invisible ghost process)
                main.FormClosed += (_, _) => System.Windows.Forms.Application.Exit();
                main.Show();
                Hide();
            }
            else
            {
                lblError.Text    = "✗  " + result.GetProperty("message").GetString();
                btnLogin.Enabled = true;
                btnLogin.Text    = "Sign In";
            }
        }

        // ─────────────────────────────────────────────────────────────────────
        //  REGISTER TAB — event handlers
        // ─────────────────────────────────────────────────────────────────────

        // Validates username as the user types (min 3 chars, letters/numbers only)
        private void txtRegUser_TextChanged(object sender, EventArgs e)
        {
            string val = txtRegUser.Text;
            if (val.Length == 0)
            {
                lblRegMsg.Text = "";
                return;
            }
            bool valid = val.Length >= 3 && val.All(c => char.IsLetterOrDigit(c) || c == '_');
            lblRegMsg.ForeColor = valid
                ? System.Drawing.Color.FromArgb(80, 190, 110)
                : System.Drawing.Color.FromArgb(210, 70, 50);
            lblRegMsg.Text = valid
                ? "✓  Username looks good"
                : "✗  Min 3 chars, letters and numbers only";
        }

        // Shows password strength as the user types
        private void txtRegPass_TextChanged(object sender, EventArgs e)
        {
            string p = txtRegPass.Text;
            if (p.Length == 0) { lblPassStrength.Text = ""; return; }

            int score = 0;
            if (p.Length >= 8) score++;
            if (p.Any(char.IsUpper)) score++;
            if (p.Any(char.IsDigit)) score++;
            if (p.Any(c => "!@#$%^&*".Contains(c))) score++;

            (lblPassStrength.Text, lblPassStrength.ForeColor) = score switch
            {
                0 or 1 => ("Strength: Weak", System.Drawing.Color.FromArgb(210, 70, 50)),
                2 => ("Strength: Fair", System.Drawing.Color.FromArgb(200, 155, 55)),
                3 => ("Strength: Good", System.Drawing.Color.FromArgb(80, 190, 110)),
                _ => ("Strength: Strong", System.Drawing.Color.FromArgb(50, 200, 130)),
            };
        }

        // Auto-formats license key as user types: SK-XXXX-XXXX-XXXX-XXXX
        private void txtRegKey_TextChanged(object sender, EventArgs e)
        {
            // Strip everything except letters and digits
            string raw = "";
            foreach (char c in txtRegKey.Text.ToUpper())
                if (char.IsLetterOrDigit(c)) raw += c;

            // Max 18 chars: "SK" + 16 hex chars
            if (raw.Length > 18) raw = raw[..18];

            // Build: SK-XXXX-XXXX-XXXX-XXXX
            string formatted;
            if (raw.Length >= 2 && raw[..2] == "SK")
            {
                formatted = "SK";
                string rest = raw[2..];
                for (int i = 0; i < rest.Length; i++)
                {
                    if (i % 4 == 0) formatted += "-";
                    formatted += rest[i];
                }
            }
            else
            {
                formatted = "";
                for (int i = 0; i < raw.Length; i++)
                {
                    if (i > 0 && i % 4 == 0) formatted += "-";
                    formatted += raw[i];
                }
            }

            // Only update if changed — prevents infinite loop
            if (txtRegKey.Text != formatted)
            {
                txtRegKey.Text = formatted;
                txtRegKey.SelectionStart = formatted.Length;
            }
        }

        // Submits registration when Create Account is clicked
        private async void btnRegister_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtRegUser.Text) ||
                string.IsNullOrWhiteSpace(txtRegPass.Text) ||
                string.IsNullOrWhiteSpace(txtRegKey.Text))
            {
                lblRegMsg.ForeColor = System.Drawing.Color.FromArgb(210, 70, 50);
                lblRegMsg.Text = "✗  Username, password and key are required.";
                return;
            }

            btnRegister.Enabled = false;
            btnRegister.Text = "Creating account...";

            var result = await _auth.Register(
                txtRegUser.Text.Trim(),
                txtRegPass.Text,
                txtRegEmail.Text.Trim(),
                txtRegKey.Text.Trim()
            );

            bool ok = result.GetProperty("ok").GetBoolean();
            string msg = result.GetProperty("message").GetString()!;

            lblRegMsg.ForeColor = ok
                ? System.Drawing.Color.FromArgb(80, 190, 110)
                : System.Drawing.Color.FromArgb(210, 70, 50);
            lblRegMsg.Text = (ok ? "✓  " : "✗  ") + msg;

            if (ok)
            {
                txtUser.Text = txtRegUser.Text;
                tabMain.SelectedTab = tabLogin;
            }

            btnRegister.Enabled = true;
            btnRegister.Text = "Create Account";
        }

        // Switches AcceptButton (Enter key) depending on active tab
        private void tabMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            AcceptButton = tabMain.SelectedIndex == 0 ? btnLogin : btnRegister;
            lblError.Text = "";
            lblRegMsg.Text = "";
            lblPassStrength.Text = "";
        }

        // Email field — shows format hint in the password strength label (separate from username msg)
        private void txtRegEmail_TextChanged(object sender, EventArgs e)
        {
            string email = txtRegEmail.Text.Trim();
            if (email.Length == 0) { lblPassStrength.Text = ""; return; }

            bool valid = email.Contains('@') && email.Contains('.');
            lblPassStrength.ForeColor = valid
                ? System.Drawing.Color.FromArgb(80, 190, 110)
                : System.Drawing.Color.FromArgb(200, 155, 55);
            lblPassStrength.Text = valid ? "✓  Email looks good" : "Email format: you@example.com";
        }
    }
}
