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

namespace GUI
{
    public partial class Main : Form
    {
        BLL_User bll_user = new BLL_User();
        public Main()
        {
            InitializeComponent();
            this.Load += Main_Load;
        }

        private void Main_Load(object sender, EventArgs e)
        {
            //chia nhỏ công việc ra các tiểu trình
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += (s, args) =>
            {
                // Tải dữ liệu từ cơ sở dữ liệu
                var data = bll_user.GetList();
                args.Result = data;
            };

            worker.RunWorkerCompleted += (s, args) =>
            {
                // Cập nhật giao diện người dùng
                dataGridView1.DataSource = args.Result as List<user>;
            };

            worker.RunWorkerAsync();

        }
    }
}
