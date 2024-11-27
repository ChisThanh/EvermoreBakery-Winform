namespace GUI.Component
{
    partial class cpn_Product
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnl_Background = new Guna.UI2.WinForms.Guna2GradientPanel();
            this.pbx_Image = new Guna.UI2.WinForms.Guna2CirclePictureBox();
            this.lbl_Product = new System.Windows.Forms.Label();
            this.pnl_Click = new Guna.UI2.WinForms.Guna2Panel();
            this.pnl_Background.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbx_Image)).BeginInit();
            this.SuspendLayout();
            // 
            // pnl_Background
            // 
            this.pnl_Background.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(28)))), ((int)(((byte)(13)))));
            this.pnl_Background.BorderRadius = 10;
            this.pnl_Background.BorderThickness = 2;
            this.pnl_Background.Controls.Add(this.pnl_Click);
            this.pnl_Background.Controls.Add(this.lbl_Product);
            this.pnl_Background.Controls.Add(this.pbx_Image);
            this.pnl_Background.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_Background.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(184)))), ((int)(((byte)(148)))));
            this.pnl_Background.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(249)))), ((int)(((byte)(237)))));
            this.pnl_Background.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.pnl_Background.Location = new System.Drawing.Point(0, 0);
            this.pnl_Background.Name = "pnl_Background";
            this.pnl_Background.Size = new System.Drawing.Size(150, 175);
            this.pnl_Background.TabIndex = 0;
            // 
            // pbx_Image
            // 
            this.pbx_Image.ImageRotate = 0F;
            this.pbx_Image.Location = new System.Drawing.Point(43, 13);
            this.pbx_Image.Name = "pbx_Image";
            this.pbx_Image.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.pbx_Image.Size = new System.Drawing.Size(64, 64);
            this.pbx_Image.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbx_Image.TabIndex = 0;
            this.pbx_Image.TabStop = false;
            // 
            // lbl_Product
            // 
            this.lbl_Product.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lbl_Product.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(28)))), ((int)(((byte)(13)))));
            this.lbl_Product.Location = new System.Drawing.Point(15, 92);
            this.lbl_Product.Name = "lbl_Product";
            this.lbl_Product.Size = new System.Drawing.Size(121, 64);
            this.lbl_Product.TabIndex = 2;
            this.lbl_Product.Text = "Product Name That\'s Long That\'s Crazy";
            this.lbl_Product.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnl_Click
            // 
            this.pnl_Click.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pnl_Click.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_Click.Location = new System.Drawing.Point(0, 0);
            this.pnl_Click.Name = "pnl_Click";
            this.pnl_Click.Size = new System.Drawing.Size(150, 175);
            this.pnl_Click.TabIndex = 3;
            this.pnl_Click.UseTransparentBackground = true;
            // 
            // cpn_Product
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.pnl_Background);
            this.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "cpn_Product";
            this.Size = new System.Drawing.Size(150, 175);
            this.pnl_Background.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbx_Image)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2GradientPanel pnl_Background;
        private System.Windows.Forms.Label lbl_Product;
        private Guna.UI2.WinForms.Guna2CirclePictureBox pbx_Image;
        private Guna.UI2.WinForms.Guna2Panel pnl_Click;
    }
}
