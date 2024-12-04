namespace GUI.Auth
{
    partial class frm_Login
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btn_Close = new Guna.UI2.WinForms.Guna2ControlBox();
            this.btnSubmit = new Guna.UI2.WinForms.Guna2Button();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.guna2WinProgressIndicator1 = new Guna.UI2.WinForms.Guna2WinProgressIndicator();
            this.frm_BorderlessForm = new Guna.UI2.WinForms.Guna2BorderlessForm(this.components);
            this.pnl_MarginLeft = new Guna.UI2.WinForms.Guna2Panel();
            this.pbx_Logo = new Guna.UI2.WinForms.Guna2CirclePictureBox();
            this.tbx_Account = new Guna.UI2.WinForms.Guna2TextBox();
            this.tbx_Password = new Guna.UI2.WinForms.Guna2TextBox();
            this.lbl_Account = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lbl_Password = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.chk_Remember = new Guna.UI2.WinForms.Guna2CustomCheckBox();
            this.lbl_Remember = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lbl_Title = new Guna.UI2.WinForms.Guna2HtmlLabel();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbx_Logo)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_Close
            // 
            this.btn_Close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Close.FillColor = System.Drawing.Color.LightCoral;
            this.btn_Close.IconColor = System.Drawing.Color.White;
            this.btn_Close.Location = new System.Drawing.Point(655, 11);
            this.btn_Close.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(34, 24);
            this.btn_Close.TabIndex = 0;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // btnSubmit
            // 
            this.btnSubmit.BorderRadius = 15;
            this.btnSubmit.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnSubmit.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnSubmit.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnSubmit.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnSubmit.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(28)))), ((int)(((byte)(13)))));
            this.btnSubmit.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSubmit.ForeColor = System.Drawing.Color.White;
            this.btnSubmit.Location = new System.Drawing.Point(458, 376);
            this.btnSubmit.Margin = new System.Windows.Forms.Padding(2);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(135, 37);
            this.btnSubmit.TabIndex = 4;
            this.btnSubmit.Text = "Đăng nhập";
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // guna2WinProgressIndicator1
            // 
            this.guna2WinProgressIndicator1.Location = new System.Drawing.Point(488, 358);
            this.guna2WinProgressIndicator1.Margin = new System.Windows.Forms.Padding(2);
            this.guna2WinProgressIndicator1.Name = "guna2WinProgressIndicator1";
            this.guna2WinProgressIndicator1.ProgressColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(28)))), ((int)(((byte)(13)))));
            this.guna2WinProgressIndicator1.Size = new System.Drawing.Size(68, 68);
            this.guna2WinProgressIndicator1.TabIndex = 8;
            this.guna2WinProgressIndicator1.Visible = false;
            // 
            // frm_BorderlessForm
            // 
            this.frm_BorderlessForm.BorderRadius = 40;
            this.frm_BorderlessForm.ContainerControl = this;
            this.frm_BorderlessForm.DockIndicatorTransparencyValue = 0.6D;
            this.frm_BorderlessForm.TransparentWhileDrag = true;
            // 
            // pnl_MarginLeft
            // 
            this.pnl_MarginLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnl_MarginLeft.Location = new System.Drawing.Point(0, 0);
            this.pnl_MarginLeft.Name = "pnl_MarginLeft";
            this.pnl_MarginLeft.Size = new System.Drawing.Size(20, 500);
            this.pnl_MarginLeft.TabIndex = 10;
            // 
            // pbx_Logo
            // 
            this.pbx_Logo.Dock = System.Windows.Forms.DockStyle.Left;
            this.pbx_Logo.Image = global::GUI.Properties.Resources.square_logo;
            this.pbx_Logo.ImageRotate = 0F;
            this.pbx_Logo.Location = new System.Drawing.Point(20, 0);
            this.pbx_Logo.Name = "pbx_Logo";
            this.pbx_Logo.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.pbx_Logo.Size = new System.Drawing.Size(340, 500);
            this.pbx_Logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbx_Logo.TabIndex = 11;
            this.pbx_Logo.TabStop = false;
            // 
            // tbx_Account
            // 
            this.tbx_Account.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(28)))), ((int)(((byte)(13)))));
            this.tbx_Account.BorderRadius = 10;
            this.tbx_Account.BorderThickness = 2;
            this.tbx_Account.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.tbx_Account.DefaultText = "";
            this.tbx_Account.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.tbx_Account.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.tbx_Account.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.tbx_Account.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.tbx_Account.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(165)))), ((int)(((byte)(74)))));
            this.tbx_Account.Font = new System.Drawing.Font("Consolas", 10F);
            this.tbx_Account.ForeColor = System.Drawing.Color.Black;
            this.tbx_Account.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(165)))), ((int)(((byte)(74)))));
            this.tbx_Account.Location = new System.Drawing.Point(432, 167);
            this.tbx_Account.Margin = new System.Windows.Forms.Padding(4);
            this.tbx_Account.Name = "tbx_Account";
            this.tbx_Account.PasswordChar = '\0';
            this.tbx_Account.PlaceholderForeColor = System.Drawing.Color.Black;
            this.tbx_Account.PlaceholderText = "";
            this.tbx_Account.SelectedText = "";
            this.tbx_Account.Size = new System.Drawing.Size(180, 36);
            this.tbx_Account.TabIndex = 13;
            this.tbx_Account.TextOffset = new System.Drawing.Point(5, 0);
            // 
            // tbx_Password
            // 
            this.tbx_Password.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(28)))), ((int)(((byte)(13)))));
            this.tbx_Password.BorderRadius = 10;
            this.tbx_Password.BorderThickness = 2;
            this.tbx_Password.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.tbx_Password.DefaultText = "";
            this.tbx_Password.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.tbx_Password.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.tbx_Password.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.tbx_Password.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.tbx_Password.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(165)))), ((int)(((byte)(74)))));
            this.tbx_Password.Font = new System.Drawing.Font("Consolas", 10F);
            this.tbx_Password.ForeColor = System.Drawing.Color.Black;
            this.tbx_Password.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(165)))), ((int)(((byte)(74)))));
            this.tbx_Password.Location = new System.Drawing.Point(432, 244);
            this.tbx_Password.Margin = new System.Windows.Forms.Padding(4);
            this.tbx_Password.Name = "tbx_Password";
            this.tbx_Password.PasswordChar = '^';
            this.tbx_Password.PlaceholderForeColor = System.Drawing.Color.Black;
            this.tbx_Password.PlaceholderText = "";
            this.tbx_Password.SelectedText = "";
            this.tbx_Password.Size = new System.Drawing.Size(180, 36);
            this.tbx_Password.TabIndex = 14;
            this.tbx_Password.TextOffset = new System.Drawing.Point(5, 0);
            // 
            // lbl_Account
            // 
            this.lbl_Account.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Account.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Account.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(28)))), ((int)(((byte)(13)))));
            this.lbl_Account.Location = new System.Drawing.Point(432, 139);
            this.lbl_Account.Name = "lbl_Account";
            this.lbl_Account.Size = new System.Drawing.Size(84, 21);
            this.lbl_Account.TabIndex = 15;
            this.lbl_Account.Text = "Tài Khoản";
            // 
            // lbl_Password
            // 
            this.lbl_Password.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Password.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Password.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(28)))), ((int)(((byte)(13)))));
            this.lbl_Password.Location = new System.Drawing.Point(432, 216);
            this.lbl_Password.Name = "lbl_Password";
            this.lbl_Password.Size = new System.Drawing.Size(75, 21);
            this.lbl_Password.TabIndex = 16;
            this.lbl_Password.Text = "Mật Khẩu";
            // 
            // chk_Remember
            // 
            this.chk_Remember.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(28)))), ((int)(((byte)(13)))));
            this.chk_Remember.CheckedState.BorderRadius = 10;
            this.chk_Remember.CheckedState.BorderThickness = 2;
            this.chk_Remember.CheckedState.FillColor = System.Drawing.Color.White;
            this.chk_Remember.CheckMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(28)))), ((int)(((byte)(13)))));
            this.chk_Remember.Location = new System.Drawing.Point(432, 293);
            this.chk_Remember.Name = "chk_Remember";
            this.chk_Remember.Size = new System.Drawing.Size(36, 36);
            this.chk_Remember.TabIndex = 17;
            this.chk_Remember.Text = "Ghi nhớ tài khoản";
            this.chk_Remember.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(28)))), ((int)(((byte)(13)))));
            this.chk_Remember.UncheckedState.BorderRadius = 10;
            this.chk_Remember.UncheckedState.BorderThickness = 2;
            this.chk_Remember.UncheckedState.FillColor = System.Drawing.Color.White;
            // 
            // lbl_Remember
            // 
            this.lbl_Remember.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Remember.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Remember.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(28)))), ((int)(((byte)(13)))));
            this.lbl_Remember.Location = new System.Drawing.Point(473, 301);
            this.lbl_Remember.Name = "lbl_Remember";
            this.lbl_Remember.Size = new System.Drawing.Size(156, 21);
            this.lbl_Remember.TabIndex = 18;
            this.lbl_Remember.Text = "Ghi Nhớ Tài Khoản";
            // 
            // lbl_Title
            // 
            this.lbl_Title.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Title.Font = new System.Drawing.Font("Consolas", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Title.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(28)))), ((int)(((byte)(13)))));
            this.lbl_Title.Location = new System.Drawing.Point(473, 91);
            this.lbl_Title.Name = "lbl_Title";
            this.lbl_Title.Size = new System.Drawing.Size(120, 30);
            this.lbl_Title.TabIndex = 19;
            this.lbl_Title.Text = "ĐĂNG NHẬP";
            // 
            // frm_Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(700, 500);
            this.Controls.Add(this.lbl_Title);
            this.Controls.Add(this.lbl_Remember);
            this.Controls.Add(this.chk_Remember);
            this.Controls.Add(this.lbl_Password);
            this.Controls.Add(this.lbl_Account);
            this.Controls.Add(this.tbx_Password);
            this.Controls.Add(this.tbx_Account);
            this.Controls.Add(this.pbx_Logo);
            this.Controls.Add(this.pnl_MarginLeft);
            this.Controls.Add(this.guna2WinProgressIndicator1);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.btn_Close);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frm_Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Frm_Login";
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbx_Logo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Guna.UI2.WinForms.Guna2ControlBox btn_Close;
        private Guna.UI2.WinForms.Guna2Button btnSubmit;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private Guna.UI2.WinForms.Guna2WinProgressIndicator guna2WinProgressIndicator1;
        private Guna.UI2.WinForms.Guna2BorderlessForm frm_BorderlessForm;
        private Guna.UI2.WinForms.Guna2CirclePictureBox pbx_Logo;
        private Guna.UI2.WinForms.Guna2Panel pnl_MarginLeft;
        private Guna.UI2.WinForms.Guna2TextBox tbx_Password;
        private Guna.UI2.WinForms.Guna2TextBox tbx_Account;
        private Guna.UI2.WinForms.Guna2HtmlLabel lbl_Password;
        private Guna.UI2.WinForms.Guna2HtmlLabel lbl_Account;
        private Guna.UI2.WinForms.Guna2HtmlLabel lbl_Remember;
        private Guna.UI2.WinForms.Guna2CustomCheckBox chk_Remember;
        private Guna.UI2.WinForms.Guna2HtmlLabel lbl_Title;
    }
}