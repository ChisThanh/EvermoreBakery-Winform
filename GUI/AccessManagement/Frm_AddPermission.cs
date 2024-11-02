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
    public partial class Frm_AddPermission : Form
    {
        //BLL_Permission bll_Permission = new BLL_Permission(new DAL.DAL_Permission());
        public Frm_AddPermission()
        {
            InitializeComponent();
            this.Load += Frm_AddPermission_Load;
        }

        private void Frm_AddPermission_Load(object sender, EventArgs e)
        {
            
        }
    }
}
