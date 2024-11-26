using BLL;
using DTO;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI.AccessManagement
{
    public partial class Frm_AddPermissionRole : Form
    {
        BLL_AccessManagement bLL_AccessManagement = new BLL_AccessManagement();
        List<long> ids = new List<long>();
        List<long> idsPerRole = new List<long>();
        int selectedIndex = 0;

        public Frm_AddPermissionRole()
        {
            InitializeComponent();
            this.Load += Frm_AddPermissionRole_Load;
        }

        private void Frm_AddPermissionRole_Load(object sender, EventArgs e)
        {
            var data = bLL_AccessManagement.GetRoleList();
            cbxRole.DataSource = data;
            cbxRole.DisplayMember = "display_name";
            cbxRole.ValueMember = "id";

            cbxRole.SelectedIndexChanged += CbxRole_SelectedIndexChanged;
            btnSave.Click += BtnSave_Click;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            var idsCheck = new List<long>();
            var roleId = (long)cbxRole.SelectedValue;
            GetCheckedIds();
            foreach (var item in ids)
            {
                if (!idsPerRole.Contains(item))
                    bLL_AccessManagement.AddPermissionRole(roleId, item);
                else
                    idsCheck.Add(item);
            }

            if (idsCheck.Count != idsPerRole.Count)
            {
                var differentInIdsPerRole = idsPerRole.Except(idsCheck).ToList();
                foreach (var item in differentInIdsPerRole)
                    bLL_AccessManagement.DeletePermissionRole(roleId, item);
            }

            ids.Clear();
            cbxRole.SelectedIndex = selectedIndex;
            MessageBox.Show("Chỉnh sửa quyên thành công vào vai trò");
        }

        private void CbxRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedIndex = cbxRole.SelectedIndex;
            var roleId = (long)cbxRole.SelectedValue;

            var datePer = bLL_AccessManagement.GetPerList();
            var dataPerRole = bLL_AccessManagement.GetPermissionsByRoleId(roleId);

            idsPerRole.Clear();
            foreach (var item in dataPerRole)
                idsPerRole.Add(item.id);

            var dataAllPer = datePer.Select(p => new permission
            {
                id = p.id,
                display_name = p.display_name,
                isChecked = dataPerRole.Any(p2 => p2.id == p.id)
            }).ToList();

            Frm_AddPermissionUser.RenderUI(dataAllPer, pnMain);
        }

        private void GetCheckedIds()
        {
            var pnl = pnMain;
            foreach (var item in pnl.Controls)
            {
                if (item is CheckBox)
                {
                    var cb = item as CheckBox;
                    if (cb.Checked)
                    {
                        ids.Add((long)cb.Tag);
                    }
                }
            }
        }
    }

}
