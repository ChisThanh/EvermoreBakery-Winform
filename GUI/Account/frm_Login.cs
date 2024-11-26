using BLL;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI.Auth
{
    public partial class frm_Login : Form
    {
        private string account;
        private string password;

        public delegate void LoginSuccessEventHandler(object sender, EventArgs e);
        public event LoginSuccessEventHandler LoginSuccess;

        public frm_Login()
        {
            InitializeComponent();
            btnSubmit.Click += BtnSubmit_Click;

            txtAccount.Text = "sadmin@mail.com";
            txtPassword.Text = "123";
        }

        private async void BtnSubmit_Click(object sender, EventArgs e)
        {
            guna2WinProgressIndicator1.Visible = true; 
            btnSubmit.Visible = false;                   
            guna2WinProgressIndicator1.Start();          

            if (!ValidateInputs())
            {
                guna2WinProgressIndicator1.Stop();       
                guna2WinProgressIndicator1.Visible = false;
                btnSubmit.Visible = true;                
                return;
            }

            try
            {
                var stopwatch = new System.Diagnostics.Stopwatch();
                stopwatch.Start();

                var user = await BLL_Auth.LoginAsync(account, password); 
                Program.userAuth = user;

                stopwatch.Stop(); 

                if (stopwatch.ElapsedMilliseconds < 500)
                    await Task.Delay(500); 

                OnLoginSuccess(EventArgs.Empty); 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                guna2WinProgressIndicator1.Stop();       
                guna2WinProgressIndicator1.Visible = false; 
                btnSubmit.Visible = true;               
            }
        }

        private bool ValidateInputs()
        {
            account = txtAccount.Text.Trim();
            password = txtPassword.Text.Trim();

            return ValidateField(txtAccount, account, "Tài khoản không được để trống") &&
                   ValidateField(txtPassword, password, "Mật khẩu không được để trống");
        }

        private bool ValidateField(Control control, string value, string errorMessage)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                errorProvider.SetError(control, errorMessage);
                return false;
            }
            errorProvider.SetError(control, "");
            return true;
        }

        protected virtual void OnLoginSuccess(EventArgs e)
        {
            LoginSuccess?.Invoke(this, e);
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
