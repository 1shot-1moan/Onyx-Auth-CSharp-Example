namespace OnyxGateExample
{
    partial class LoginForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage    tabLogin;
        private System.Windows.Forms.TabPage    tabRegister;

        // Login tab
        private System.Windows.Forms.Label   lblWelcome;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.TextBox txtPass;
        private System.Windows.Forms.Button  btnLogin;
        private System.Windows.Forms.Label   lblError;

        // Register tab
        private System.Windows.Forms.Label   lblRegNote;
        private System.Windows.Forms.TextBox txtRegUser;
        private System.Windows.Forms.TextBox txtRegPass;
        private System.Windows.Forms.Label   lblPassStrength;
        private System.Windows.Forms.TextBox txtRegEmail;
        private System.Windows.Forms.TextBox txtRegKey;
        private System.Windows.Forms.Button  btnRegister;
        private System.Windows.Forms.Label   lblRegMsg;

        private void InitializeComponent()
        {
            tabMain = new TabControl();
            tabLogin = new TabPage();
            lblWelcome = new Label();
            txtUser = new TextBox();
            txtPass = new TextBox();
            btnLogin = new Button();
            lblError = new Label();
            tabRegister = new TabPage();
            lblRegNote = new Label();
            txtRegUser = new TextBox();
            txtRegPass = new TextBox();
            lblPassStrength = new Label();
            txtRegEmail = new TextBox();
            txtRegKey = new TextBox();
            btnRegister = new Button();
            lblRegMsg = new Label();
            tabMain.SuspendLayout();
            tabLogin.SuspendLayout();
            tabRegister.SuspendLayout();
            SuspendLayout();
            // 
            // tabMain
            // 
            tabMain.Controls.Add(tabLogin);
            tabMain.Controls.Add(tabRegister);
            tabMain.ForeColor = Color.FromArgb(215, 200, 175);
            tabMain.Location = new Point(0, 0);
            tabMain.Name = "tabMain";
            tabMain.SelectedIndex = 0;
            tabMain.Size = new Size(420, 360);
            tabMain.TabIndex = 0;
            tabMain.SelectedIndexChanged += tabMain_SelectedIndexChanged;
            // 
            // tabLogin
            // 
            tabLogin.BackColor = Color.FromArgb(28, 25, 22);
            tabLogin.Controls.Add(lblWelcome);
            tabLogin.Controls.Add(txtUser);
            tabLogin.Controls.Add(txtPass);
            tabLogin.Controls.Add(btnLogin);
            tabLogin.Controls.Add(lblError);
            tabLogin.Location = new Point(4, 26);
            tabLogin.Name = "tabLogin";
            tabLogin.Size = new Size(412, 330);
            tabLogin.TabIndex = 0;
            tabLogin.Text = "  Login  ";
            // 
            // lblWelcome
            // 
            lblWelcome.AutoSize = true;
            lblWelcome.ForeColor = Color.FromArgb(155, 140, 118);
            lblWelcome.Location = new Point(20, 18);
            lblWelcome.Name = "lblWelcome";
            lblWelcome.Size = new Size(116, 17);
            lblWelcome.TabIndex = 0;
            lblWelcome.Text = "Sign in to continue";
            // 
            // txtUser
            // 
            txtUser.BackColor = Color.FromArgb(40, 36, 32);
            txtUser.BorderStyle = BorderStyle.FixedSingle;
            txtUser.Font = new Font("Segoe UI", 10.5F);
            txtUser.ForeColor = Color.FromArgb(215, 200, 175);
            txtUser.Location = new Point(20, 48);
            txtUser.Name = "txtUser";
            txtUser.PlaceholderText = "Username";
            txtUser.Size = new Size(360, 26);
            txtUser.TabIndex = 1;
            txtUser.TextChanged += txtUser_TextChanged;
            // 
            // txtPass
            // 
            txtPass.BackColor = Color.FromArgb(40, 36, 32);
            txtPass.BorderStyle = BorderStyle.FixedSingle;
            txtPass.Font = new Font("Segoe UI", 10.5F);
            txtPass.ForeColor = Color.FromArgb(215, 200, 175);
            txtPass.Location = new Point(20, 92);
            txtPass.Name = "txtPass";
            txtPass.PlaceholderText = "Password";
            txtPass.Size = new Size(360, 26);
            txtPass.TabIndex = 2;
            txtPass.UseSystemPasswordChar = true;
            txtPass.TextChanged += txtPass_TextChanged;
            // 
            // btnLogin
            // 
            btnLogin.BackColor = Color.FromArgb(185, 85, 50);
            btnLogin.Cursor = Cursors.Hand;
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            btnLogin.ForeColor = Color.White;
            btnLogin.Location = new Point(20, 138);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(360, 40);
            btnLogin.TabIndex = 3;
            btnLogin.Text = "Sign In";
            btnLogin.UseVisualStyleBackColor = false;
            btnLogin.Click += btnLogin_Click;
            // 
            // lblError
            // 
            lblError.ForeColor = Color.FromArgb(210, 70, 50);
            lblError.Location = new Point(20, 190);
            lblError.Name = "lblError";
            lblError.Size = new Size(360, 20);
            lblError.TabIndex = 4;
            // 
            // tabRegister
            // 
            tabRegister.BackColor = Color.FromArgb(28, 25, 22);
            tabRegister.Controls.Add(lblRegNote);
            tabRegister.Controls.Add(txtRegUser);
            tabRegister.Controls.Add(txtRegPass);
            tabRegister.Controls.Add(lblPassStrength);
            tabRegister.Controls.Add(txtRegEmail);
            tabRegister.Controls.Add(txtRegKey);
            tabRegister.Controls.Add(btnRegister);
            tabRegister.Controls.Add(lblRegMsg);
            tabRegister.Location = new Point(4, 26);
            tabRegister.Name = "tabRegister";
            tabRegister.Size = new Size(412, 330);
            tabRegister.TabIndex = 1;
            tabRegister.Text = "  Register  ";
            // 
            // lblRegNote
            // 
            lblRegNote.AutoSize = true;
            lblRegNote.ForeColor = Color.FromArgb(155, 140, 118);
            lblRegNote.Location = new Point(20, 12);
            lblRegNote.Name = "lblRegNote";
            lblRegNote.Size = new Size(236, 17);
            lblRegNote.TabIndex = 0;
            lblRegNote.Text = "Create an account with your license key";
            // 
            // txtRegUser
            // 
            txtRegUser.BackColor = Color.FromArgb(40, 36, 32);
            txtRegUser.BorderStyle = BorderStyle.FixedSingle;
            txtRegUser.Font = new Font("Segoe UI", 10.5F);
            txtRegUser.ForeColor = Color.FromArgb(215, 200, 175);
            txtRegUser.Location = new Point(20, 38);
            txtRegUser.Name = "txtRegUser";
            txtRegUser.PlaceholderText = "Choose a username  (min 3 chars)";
            txtRegUser.Size = new Size(360, 26);
            txtRegUser.TabIndex = 1;
            txtRegUser.TextChanged += txtRegUser_TextChanged;
            // 
            // txtRegPass
            // 
            txtRegPass.BackColor = Color.FromArgb(40, 36, 32);
            txtRegPass.BorderStyle = BorderStyle.FixedSingle;
            txtRegPass.Font = new Font("Segoe UI", 10.5F);
            txtRegPass.ForeColor = Color.FromArgb(215, 200, 175);
            txtRegPass.Location = new Point(20, 76);
            txtRegPass.Name = "txtRegPass";
            txtRegPass.PlaceholderText = "Choose a password";
            txtRegPass.Size = new Size(360, 26);
            txtRegPass.TabIndex = 2;
            txtRegPass.UseSystemPasswordChar = true;
            txtRegPass.TextChanged += txtRegPass_TextChanged;
            // 
            // lblPassStrength
            // 
            lblPassStrength.Font = new Font("Segoe UI", 8.5F);
            lblPassStrength.ForeColor = Color.FromArgb(130, 200, 100);
            lblPassStrength.Location = new Point(20, 112);
            lblPassStrength.Name = "lblPassStrength";
            lblPassStrength.Size = new Size(360, 18);
            lblPassStrength.TabIndex = 3;
            // 
            // txtRegEmail
            // 
            txtRegEmail.BackColor = Color.FromArgb(40, 36, 32);
            txtRegEmail.BorderStyle = BorderStyle.FixedSingle;
            txtRegEmail.Font = new Font("Segoe UI", 10.5F);
            txtRegEmail.ForeColor = Color.FromArgb(215, 200, 175);
            txtRegEmail.Location = new Point(20, 134);
            txtRegEmail.Name = "txtRegEmail";
            txtRegEmail.PlaceholderText = "Email  (optional)";
            txtRegEmail.Size = new Size(360, 26);
            txtRegEmail.TabIndex = 4;
            txtRegEmail.TextChanged += txtRegEmail_TextChanged;
            // 
            // txtRegKey
            // 
            txtRegKey.BackColor = Color.FromArgb(40, 36, 32);
            txtRegKey.BorderStyle = BorderStyle.FixedSingle;
            txtRegKey.CharacterCasing = CharacterCasing.Upper;
            txtRegKey.Font = new Font("Consolas", 11F);
            txtRegKey.ForeColor = Color.FromArgb(215, 200, 175);
            txtRegKey.Location = new Point(20, 172);
            txtRegKey.Name = "txtRegKey";
            txtRegKey.PlaceholderText = "SK-XXXX-XXXX-XXXX-XXXX";
            txtRegKey.Size = new Size(360, 25);
            txtRegKey.TabIndex = 5;
            txtRegKey.TextChanged += txtRegKey_TextChanged;
            // 
            // btnRegister
            // 
            btnRegister.BackColor = Color.FromArgb(185, 85, 50);
            btnRegister.Cursor = Cursors.Hand;
            btnRegister.FlatAppearance.BorderSize = 0;
            btnRegister.FlatStyle = FlatStyle.Flat;
            btnRegister.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            btnRegister.ForeColor = Color.White;
            btnRegister.Location = new Point(20, 214);
            btnRegister.Name = "btnRegister";
            btnRegister.Size = new Size(360, 38);
            btnRegister.TabIndex = 6;
            btnRegister.Text = "Create Account";
            btnRegister.UseVisualStyleBackColor = false;
            btnRegister.Click += btnRegister_Click;
            // 
            // lblRegMsg
            // 
            lblRegMsg.Location = new Point(20, 260);
            lblRegMsg.Name = "lblRegMsg";
            lblRegMsg.Size = new Size(360, 20);
            lblRegMsg.TabIndex = 7;
            // 
            // LoginForm
            // 
            BackColor = Color.FromArgb(28, 25, 22);
            ClientSize = new Size(420, 360);
            Controls.Add(tabMain);
            Font = new Font("Segoe UI", 9.5F);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "LoginForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Onyx Gate — Auth";
            tabMain.ResumeLayout(false);
            tabLogin.ResumeLayout(false);
            tabLogin.PerformLayout();
            tabRegister.ResumeLayout(false);
            tabRegister.PerformLayout();
            ResumeLayout(false);
        }
    }
}
