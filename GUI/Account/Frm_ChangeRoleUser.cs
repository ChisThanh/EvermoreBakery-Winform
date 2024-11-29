using BLL;
using DTO;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI.Account
{
    public partial class Frm_ChangeRoleUser : Form
    {
        BLL_AccessManagement BLL_AccessManagement = new BLL_AccessManagement();

        user _user = null;

        string roleUser = "";

        public event EventHandler RoleChanged;


        public Frm_ChangeRoleUser(user __user = null)
        {
            if(__user == null) __user = EvermoreBakeryContext.Instance.users.First();
            _user = __user;

            InitializeComponent();
            this.Load += Frm_ChangeRoleUser_Load;
            txtUserName.Text = _user.name;
            btnSave.Click += BtnSave_Click;
            btn_Close.Click += Btn_Close_Click;
        }

        private void Btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            GetChecked();

            if(roleUser == "")
            {
                MessageBox.Show("Vui lòng chọn quyền");
                return;
            }

            var check =  BLL_AccessManagement.ChangeRoleUser(_user.id, roleUser);
            if(!check)
            {
                MessageBox.Show("Thay đổi quyền thất bại. Vui lòng liên hệ Anh Trai Làm Phần Này");
                return;
            }

            RoleChanged?.Invoke(this, EventArgs.Empty);

            this.Close();
        }

        private void Frm_ChangeRoleUser_Load(object sender, EventArgs e)
        {
            var data = BLL_AccessManagement.GetRoleList();

            var roleUser = _user.GetRoles();

            if (roleUser != null)
            {
                foreach (var item in data)
                {
                    if (item.name == roleUser)
                        item.isChecked = true;
                    else item.isChecked = false;
                }
            }

            RenderUI(data, pnMain);
        }

        public static void RenderUI(List<role> list, Panel panel)
        {
            panel.Controls.Clear();
            var checkBoxWidth = 200;
            var horizontalSpacing = 10;
            var verticalSpacing = 30;

            var startTop = 10;
            var startLeft = 10;

            int itemsPerRow = 2;
            int currentRow = 0;

            foreach (var item in list)
            {
                Guna2RadioButton guna2RadioButton = new Guna2RadioButton();
                guna2RadioButton.Text = item.display_name;
                guna2RadioButton.Tag = item.id;
                guna2RadioButton.Checked = false;

                guna2RadioButton.Top = startTop;
                guna2RadioButton.Left = startLeft + horizontalSpacing;

                if (item.isChecked) guna2RadioButton.Checked = true;

                currentRow++;

                if (currentRow == itemsPerRow)
                {
                    startTop += verticalSpacing;
                    startLeft = 10;
                    currentRow = 0;
                }
                else
                {
                    startLeft += checkBoxWidth + horizontalSpacing;
                }

                panel.Controls.Add(guna2RadioButton);
            }
        }

        private void GetChecked()
        {
            var pnl = pnMain;
            foreach (var item in pnl.Controls)
            {
                if (item is RadioButton)
                {
                    var cb = item as RadioButton;
                    if (cb.Checked)
                        roleUser = cb.Text;
                }
            }
        }
    }
}
