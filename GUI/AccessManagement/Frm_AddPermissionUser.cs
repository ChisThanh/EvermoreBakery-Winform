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

namespace GUI.AccessManagement
{
    public partial class Frm_AddPermissionUser : Form
    {
        private BLL_Permission BLL_Permission = new BLL_Permission();
        user _user = EvermoreBakeryContext.Instance.users.First();
        public Frm_AddPermissionUser()
        {
            InitializeComponent();
            this.Load += Frm_AddPermission_Load;
        }

        private void Frm_AddPermission_Load(object sender, EventArgs e)
        {
            var list = BLL_Permission.GetList();
            RenderUI(list, pnMain);
            txtUserName.Text = _user.name;
        }

        public static void RenderUI(List<permission> list, Panel panel)
        {
            panel.Controls.Clear();
            var checkBoxWidth = 200; 
            var horizontalSpacing = 10; 
            var verticalSpacing = 30;  

            var startTop = 10;  
            var startLeft = 10;

            int itemsPerRow = 3; 
            int currentRow = 0;  

            foreach (var item in list)
            {
                Guna2CheckBox guna2CheckBox = new Guna2CheckBox();
                guna2CheckBox.Text = item.display_name;
                guna2CheckBox.Tag = item.id;
                guna2CheckBox.Checked = false;

                guna2CheckBox.Top = startTop;  
                guna2CheckBox.Left = startLeft + horizontalSpacing; 

                if(item.isChecked) guna2CheckBox.Checked = true;

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

                panel.Controls.Add(guna2CheckBox);
            }
        }

        
    }
}
