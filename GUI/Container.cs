using GUI.Account;
using GUI.Bills;
using GUI.Profits;
using GUI.Sales;
using GUI.Users;
using Guna.UI2.WinForms;
using System;
using GUI.Account;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using GUI.Auth;
using GUI.ProductReview;

namespace GUI
{
    public partial class frm_Container : Form
    {
        frm_Login frm_Login = new frm_Login();
        public frm_Container()
        {
            InitializeComponent();

            btn_Logout.Click += (s, e) => Logout();
            btn_Product.Click += Btn_Product_Click;
            btn_Event.Click += Btn_Event_Click;
            btn_Ingredient.Click += Btn_Ingredient_Click;
            btn_Invoice.Click += Btn_Invoice_Click;
            btn_Satistic.Click += Btn_Satistic_Click;
            btn_Account.Click += Btn_Account_Click;
            btnPreview.Click += BtnPreview_Click;

            btn_Minimize.Click += (s, e) => WindowState = FormWindowState.Minimized;
            btn_Maximize.Click += (s, e) => 
            {
                if (WindowState == FormWindowState.Normal) WindowState = FormWindowState.Maximized;
                else WindowState = FormWindowState.Normal;
            };
            btn_Close.Click += (s, e) => Close();

            btn_Maximize.Enabled = false;

            if (Program.userAuth == null)
            {
                ShowLoginForm();
            }
        }

        private void BtnPreview_Click(object sender, EventArgs e)
        {
            UncheckButtons();
            btnPreview.Checked = true;
            lbl_Header.Text = "ĐÁNH GIÁ";
            OpenForm(new Frm_ProductReview());
        }

        private void ShowLoginForm()
        {
            this.Hide();
            frm_Login.LoginSuccess += Frm_Login_LoginSuccess; 
            frm_Login.ShowDialog();
        }

        private void Logout()
        {
            this.Hide();
            frm_Login.Show();
            Program.userAuth = null;
        }
        private void Frm_Login_LoginSuccess(object sender, EventArgs e)
        {
            frm_Login.Hide();
            this.Show();
            lbl_Name.Text = Program.userAuth.name;
            lbl_Role.Text = Program.userAuth.role_user.FirstOrDefault().role.name;
        }

        private void Btn_Account_Click(object sender, EventArgs e)
        {
            UncheckButtons();
            btn_Account.Checked = true;
            lbl_Header.Text = "TÀI KHOẢN";
            if(!Program.userAuth.HasPermissions("user-read")) MessageBox.Show("Bạn không có quyền thực hiện hành động này");
            else OpenForm(new frm_MUser());
        }

        private void Btn_Satistic_Click(object sender, EventArgs e)
        {
            UncheckButtons();
            btn_Satistic.Checked = true;
            lbl_Header.Text = "THỐNG KÊ";
            OpenForm(new frm_Invoice());
        }

        private void Btn_Invoice_Click(object sender, EventArgs e)
        {
            UncheckButtons();
            btn_Invoice.Checked = true;
            lbl_Header.Text = "HÓA ĐƠN";
            OpenForm(new frm_Invoice());
        }

        private void Btn_Ingredient_Click(object sender, EventArgs e)
        {
            UncheckButtons();
            btn_Ingredient.Checked = true;
            lbl_Header.Text = "NGUYÊN LIỆU";
            OpenForm(new frm_Ingredient());
        }

        private void Btn_Event_Click(object sender, EventArgs e)
        {
            UncheckButtons();
            btn_Event.Checked = true;
            lbl_Header.Text = "SỰ KIỆN";
            OpenForm(new frm_Event());
        }

        private void Btn_Product_Click(object sender, EventArgs e)
        {
            UncheckButtons();
            btn_Product.Checked = true;
            lbl_Header.Text = "SẢN PHẨM";
            OpenForm(new pnl_PaddingMiddle());
        }

        public void OpenForm(Form form)
        {
            if (pnl_Content.Controls.Count > 0)
            {
                foreach (Control control in pnl_Content.Controls)
                {
                    if (control is Form) ((Form)control).Close();
                }
                pnl_Content.Controls.Clear();
            }

            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
            pnl_Content.Controls.Add(form);
            form.Show();
        }

        public void UncheckButtons()
        {
            foreach(Control control in pnl_Sidebar.Controls)
            {
                if(control is Guna2Panel panel)
                {
                    foreach(Control controlChild in panel.Controls)
                    {
                        if (controlChild is Guna2Button button) button.Checked = false;
                    }
                }

            }
        }
    }
}
