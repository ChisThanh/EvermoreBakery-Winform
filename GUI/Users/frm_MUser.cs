using BLL;
using DTO;
using GUI.Account;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI.Users
{
    public partial class frm_MUser : Form
    {
        BLL_User _BLL_User = new BLL_User();
        BLL_AccessManagement _BLL_AccessManagement = new BLL_AccessManagement();

        user _user = null;
        user _userAuth = Program.userAuth;

        long _id = 0;

        public frm_MUser()
        {
            InitializeComponent();
            this.Load += Frm_MUser_Load;
        }

        private void Frm_MUser_Load(object sender, EventArgs e)
        {
            LoadData();
            LoadDataCBX();
            dgvMain.AllowUserToAddRows = false;
            dgvMain.CellClick += DgvMain_CellClick;
            btnAdd.Click += BtnAdd_Click;
            btnPerRole.Click += BtnPerRole_Click;
        }

        private void BtnPerRole_Click(object sender, EventArgs e)
        {
            if (!_userAuth.HasPermissions("role-approve"))
            {
                MessageBox.Show("Bạn không có quyền thực hiện hành động này");
                return;
            }

            var frm = new Frm_AddPermissionRole();
            frm.ShowDialog();
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (!_userAuth.HasPermissions("user-create"))
            {
                MessageBox.Show("Bạn không có quyền thực hiện hành động này");
                return;
            }

            var txt = GetTxt();
            user user = new user
            {
                name = txt.Name,
                email = txt.Email,
                password = txt.Password,
                email_verified_at = DateTime.Now,
                created_at = DateTime.Now,
                updated_at = DateTime.Now,
            };

            if (_BLL_User.Add(user, txt.Role))
            {
                MessageBox.Show("Thêm thành công");
                LoadData();
            }
            else MessageBox.Show("Thêm thất bại hỏi thằng nào làm ra cái này");
        }

        private void LoadDataCBX()
        {
            var data = _BLL_AccessManagement.GetRoleList();
            cbxRole.DataSource = data;
            cbxRole.DisplayMember = "display_name";
            cbxRole.ValueMember = "id";
        }

        private void DgvMain_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                 _id = long.Parse(dgvMain.Rows[e.RowIndex].Cells[0].Value.ToString());
                var name = dgvMain.Rows[e.RowIndex].Cells[1].Value.ToString();
                var email = dgvMain.Rows[e.RowIndex].Cells[2].Value.ToString();
                txtName.Text = name;
                txtEmail.Text = email;

                if (e.ColumnIndex == 6)
                {
                    if (!_userAuth.HasPermissions("role-approve"))
                    {
                        MessageBox.Show("Bạn không có quyền thực hiện hành động này");
                        return;
                    }
                    var id = dgvMain.Rows[e.RowIndex].Cells["#"].Value.ToString();
                    _user = _BLL_User.GetById(int.Parse(id));
                    Frm_ChangeRoleUser frm = new Frm_ChangeRoleUser(_user);
                    frm.RoleChanged += Frm_RoleChanged;
                    frm.ShowDialog();
                }
                else if (e.ColumnIndex == 5)
                {
                    if (!_userAuth.HasPermissions("user-delete"))
                    {
                        MessageBox.Show("Bạn không có quyền thực hiện hành động này");
                        return;
                    }
                    var id = dgvMain.Rows[e.RowIndex].Cells["#"].Value.ToString();
                    if (_BLL_User.Delete(long.Parse(id)))
                    {
                        MessageBox.Show("Xóa thành công");
                        LoadData();
                    }
                    else MessageBox.Show("Xóa thất bại hỏi thằng nào làm ra cái này");
                }
            }
        }

        private void Frm_RoleChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            var data = _BLL_User.GetList();

            dgvMain.Columns.Clear();

            dgvMain.Columns.Add("#", "Id");
            dgvMain.Columns.Add("Tên", "Name");
            dgvMain.Columns.Add("Email", "Email");
            dgvMain.Columns.Add("Vai Trò", "Role");
            dgvMain.Columns.Add("Ngày Tạo", "Created At");

            var ColumnDel = new DataGridViewImageColumn
            {
                Image = Properties.Resources.cake_primary,
                ImageLayout = DataGridViewImageCellLayout.Zoom,
                ReadOnly = true,
                Name = "Xóa",
            };
            dgvMain.Columns.Add(ColumnDel);

            var actionColumn = new DataGridViewImageColumn
            {
                Image = Properties.Resources.cake_primary,
                ImageLayout = DataGridViewImageCellLayout.Zoom,
                ReadOnly = true,
                Name = "Vai Trò",
            };
            dgvMain.Columns.Add(actionColumn);

            var preparedData = data.Select(item => new
            {
                Id = item.id,
                Name = item.name,
                Email = item.email,
                DisplayName = item.role_user?.FirstOrDefault()?.role?.display_name ?? "N/A",
                CreatedAt = item.created_at
            }).ToList();


            foreach (var item in preparedData)
            {
                dgvMain.Rows.Add(item.Id, item.Name, item.Email, item.DisplayName, item.CreatedAt);
            }
        }

        

        public dynamic GetTxt()
        {
            return new
            {
                Name = txtName.Text,
                Email = txtEmail.Text,
                Password = txtPass.Text,
                Role = cbxRole.SelectedValue
            };
        }
    }

}
