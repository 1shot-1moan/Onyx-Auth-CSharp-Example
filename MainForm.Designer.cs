namespace OnyxGateExample
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private System.Windows.Forms.Panel  panelTop;
        private System.Windows.Forms.Label  lblTitle;
        private System.Windows.Forms.Label  lblUser;
        private System.Windows.Forms.Panel  panelSidebar;
        private System.Windows.Forms.Panel  panelContent;
        private System.Windows.Forms.Button btnAimbot;
        private System.Windows.Forms.Button btnESP;
        private System.Windows.Forms.Button btnVisuals;
        private System.Windows.Forms.Button btnMisc;
        private System.Windows.Forms.Button btnSettings;

        private void InitializeComponent()
        {
            this.panelTop     = new System.Windows.Forms.Panel();
            this.lblTitle     = new System.Windows.Forms.Label();
            this.lblUser      = new System.Windows.Forms.Label();
            this.panelSidebar = new System.Windows.Forms.Panel();
            this.panelContent = new System.Windows.Forms.Panel();
            this.btnAimbot    = new System.Windows.Forms.Button();
            this.btnESP       = new System.Windows.Forms.Button();
            this.btnVisuals   = new System.Windows.Forms.Button();
            this.btnMisc      = new System.Windows.Forms.Button();
            this.btnSettings  = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // ── Form ──────────────────────────────────────────────────────────
            this.Text            = "Script Kittens — Internal Panel";
            this.ClientSize      = new System.Drawing.Size(700, 500);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox     = false;
            this.StartPosition   = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.BackColor       = System.Drawing.Color.FromArgb(22, 20, 18);
            this.Font            = new System.Drawing.Font("Segoe UI", 9.5F);

            // ── Top bar ───────────────────────────────────────────────────────
            this.panelTop.Dock      = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Height    = 52;
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(30, 27, 24);

            this.lblTitle.Text      = "Internal Panel";
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(215, 195, 165);
            this.lblTitle.Font      = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location  = new System.Drawing.Point(18, 14);
            this.lblTitle.AutoSize  = true;

            this.lblUser.Text      = "Loading...";
            this.lblUser.ForeColor = System.Drawing.Color.FromArgb(130, 115, 95);
            this.lblUser.Font      = new System.Drawing.Font("Segoe UI", 9F);
            this.lblUser.Location  = new System.Drawing.Point(500, 18);
            this.lblUser.AutoSize  = true;

            this.panelTop.Controls.Add(this.lblTitle);
            this.panelTop.Controls.Add(this.lblUser);

            // ── Sidebar ───────────────────────────────────────────────────────
            this.panelSidebar.Dock      = System.Windows.Forms.DockStyle.Left;
            this.panelSidebar.Width     = 175;
            this.panelSidebar.BackColor = System.Drawing.Color.FromArgb(27, 24, 21);

            // btnAimbot
            this.btnAimbot.Text      = "  Aimbot";
            this.btnAimbot.Location  = new System.Drawing.Point(8, 10);
            this.btnAimbot.Size      = new System.Drawing.Size(159, 38);
            this.btnAimbot.BackColor = System.Drawing.Color.FromArgb(40, 36, 32);
            this.btnAimbot.ForeColor = System.Drawing.Color.FromArgb(195, 180, 155);
            this.btnAimbot.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAimbot.Font      = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnAimbot.Cursor    = System.Windows.Forms.Cursors.Hand;
            this.btnAimbot.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAimbot.FlatAppearance.BorderSize = 0;
            this.btnAimbot.Click    += new System.EventHandler(this.btnAimbot_Click);

            // btnESP
            this.btnESP.Text      = "  ESP";
            this.btnESP.Location  = new System.Drawing.Point(8, 54);
            this.btnESP.Size      = new System.Drawing.Size(159, 38);
            this.btnESP.BackColor = System.Drawing.Color.FromArgb(40, 36, 32);
            this.btnESP.ForeColor = System.Drawing.Color.FromArgb(195, 180, 155);
            this.btnESP.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnESP.Font      = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnESP.Cursor    = System.Windows.Forms.Cursors.Hand;
            this.btnESP.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnESP.FlatAppearance.BorderSize = 0;
            this.btnESP.Click    += new System.EventHandler(this.btnESP_Click);

            // btnVisuals
            this.btnVisuals.Text      = "  Visuals";
            this.btnVisuals.Location  = new System.Drawing.Point(8, 98);
            this.btnVisuals.Size      = new System.Drawing.Size(159, 38);
            this.btnVisuals.BackColor = System.Drawing.Color.FromArgb(40, 36, 32);
            this.btnVisuals.ForeColor = System.Drawing.Color.FromArgb(195, 180, 155);
            this.btnVisuals.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVisuals.Font      = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnVisuals.Cursor    = System.Windows.Forms.Cursors.Hand;
            this.btnVisuals.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnVisuals.FlatAppearance.BorderSize = 0;
            this.btnVisuals.Click    += new System.EventHandler(this.btnVisuals_Click);

            // btnMisc
            this.btnMisc.Text      = "  Misc";
            this.btnMisc.Location  = new System.Drawing.Point(8, 142);
            this.btnMisc.Size      = new System.Drawing.Size(159, 38);
            this.btnMisc.BackColor = System.Drawing.Color.FromArgb(40, 36, 32);
            this.btnMisc.ForeColor = System.Drawing.Color.FromArgb(195, 180, 155);
            this.btnMisc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMisc.Font      = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnMisc.Cursor    = System.Windows.Forms.Cursors.Hand;
            this.btnMisc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMisc.FlatAppearance.BorderSize = 0;
            this.btnMisc.Click    += new System.EventHandler(this.btnMisc_Click);

            // btnSettings
            this.btnSettings.Text      = "  Settings";
            this.btnSettings.Location  = new System.Drawing.Point(8, 186);
            this.btnSettings.Size      = new System.Drawing.Size(159, 38);
            this.btnSettings.BackColor = System.Drawing.Color.FromArgb(40, 36, 32);
            this.btnSettings.ForeColor = System.Drawing.Color.FromArgb(195, 180, 155);
            this.btnSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSettings.Font      = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnSettings.Cursor    = System.Windows.Forms.Cursors.Hand;
            this.btnSettings.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSettings.FlatAppearance.BorderSize = 0;
            this.btnSettings.Click    += new System.EventHandler(this.btnSettings_Click);

            this.panelSidebar.Controls.Add(this.btnAimbot);
            this.panelSidebar.Controls.Add(this.btnESP);
            this.panelSidebar.Controls.Add(this.btnVisuals);
            this.panelSidebar.Controls.Add(this.btnMisc);
            this.panelSidebar.Controls.Add(this.btnSettings);

            // ── Content panel ─────────────────────────────────────────────────
            this.panelContent.Dock      = System.Windows.Forms.DockStyle.Fill;
            this.panelContent.BackColor = System.Drawing.Color.FromArgb(22, 20, 18);
            this.panelContent.Padding   = new System.Windows.Forms.Padding(16);

            // ── Assemble ──────────────────────────────────────────────────────
            this.Controls.Add(this.panelContent);
            this.Controls.Add(this.panelSidebar);
            this.Controls.Add(this.panelTop);

            this.ResumeLayout(false);
        }
    }
}
