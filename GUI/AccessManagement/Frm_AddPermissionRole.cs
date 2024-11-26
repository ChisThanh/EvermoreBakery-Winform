using BLL;
using DTO;
using System;
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
            var roleId = (long)cbxRole.SelectedValue;
            var dataPer = bLL_AccessManagement.GetPerList();
            var dataPerRole = bLL_AccessManagement.GetPermissionsByRoleId(roleId);

            var dataAllPer = dataPer.Select(p => new permission
            {
                id = p.id,
                display_name = p.display_name,
                isChecked = dataPerRole.Any(p2 => p2.id == p.id)
            }).ToList();

            var dataPerRoleNew = dataAllPer.Where(p => p.isChecked).ToList();

            //bLL_AccessManagement.AddPermissionRole(roleId, dataPerRoleNew);

            MessageBox.Show("Add permission role success");
        }

        private void CbxRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            var roleId = (long)cbxRole.SelectedValue;

            var datePer = bLL_AccessManagement.GetPerList();
            var dataPerRole = bLL_AccessManagement.GetPermissionsByRoleId(roleId);

            var dataAllPer = datePer.Select(p => new permission
            {
                id = p.id,
                display_name = p.display_name,
                isChecked = dataPerRole.Any(p2 => p2.id == p.id)
            }).ToList();

            Frm_AddPermission.RenderUI(dataAllPer, pnMain);
        }

        
    }

}
