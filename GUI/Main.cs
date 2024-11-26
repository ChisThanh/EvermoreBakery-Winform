using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DTO;
using BLL;
using GUI.Auth;
using GUI.AccessManagement;

namespace GUI
{
    public partial class Main : Form
    {
        BLL_User bll_user = new BLL_User();
        Frm_Login loginForm = new Frm_Login();


        public Main()
        {
            InitializeComponent();
            btnLogout.Click += BtnLogout_Click;
            ShowLoginForm();
        }


        private void BtnLogout_Click(object sender, EventArgs e)
        {
            Logout();
        }

        private void ShowLoginForm()
        {
            loginForm.LoginSuccess += LoginForm_LoginSuccess;
            loginForm.ShowDialog();
        }

        private void LoginForm_LoginSuccess(object sender, EventArgs e)
        {
            this.Show();
            LoadData();
            loginForm.Hide();
        }

        private void LoadData()
        {
            //// Chia nhỏ công việc ra các tiểu trình
            //BackgroundWorker worker = new BackgroundWorker();
            //worker.DoWork += (s, args) =>
            //{
            //    // Tải dữ liệu từ cơ sở dữ liệu
            //    var data = bll_user.GetList();
            //    args.Result = data;
            //};

            //worker.RunWorkerCompleted += (s, args) =>
            //{
            //    // Cập nhật giao diện người dùng
            //    dataGridView1.DataSource = args.Result as List<user>;
            //};

            //worker.RunWorkerAsync();

        }

        private void Logout()
        {
            this.Hide();
            loginForm = new Frm_Login(); 
            loginForm.LoginSuccess += LoginForm_LoginSuccess; 
            loginForm.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Frm_AddPermissionUser frm = new Frm_AddPermissionUser();
            frm.ShowDialog();
        }
    }
}
