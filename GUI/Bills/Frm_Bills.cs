using System;
using BLL;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DTO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace GUI.Bills
{
    public partial class Frm_Bills : Form
    {
        protected BLL_Bills _BLL_Bills = new BLL_Bills();
        bill _bill = null;
        int _status = 0;
        long _id = 0;

        public Frm_Bills()
        {
            InitializeComponent();
            this.Load += Frm_Bills_Load;
        }

        private void Frm_Bills_Load(object sender, EventArgs e)
        {
            LoadData();

            dgvMain.CellClick += DgvMain_CellClick;
            dgvMain.AllowUserToAddRows = false;

            cbxStatus.Items.Add("Chờ xác nhận");
            cbxStatus.Items.Add("Đang xử lý");
            cbxStatus.Items.Add("Đang giao hàng");
            cbxStatus.Items.Add("Đã giao hàng");
            cbxStatus.Items.Add("Đã hủy");
            cbxStatus.SelectedIndex = 0;
            cbxStatus.SelectedValueChanged += CbxStatus_SelectedValueChanged;
            btnLoadData.Click += BtnLoadData_Click;
        }

        private void BtnLoadData_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void CbxStatus_SelectedValueChanged(object sender, EventArgs e)
        {
            if (_id == 0) return;
            if (_status == cbxStatus.SelectedIndex + 1) return;

            var bill = new bill();
            bill.id = _id;
            bill.status = (byte)(cbxStatus.SelectedIndex + 1);
            if(cbxStatus.SelectedIndex + 1 == 3)
                bill.delivery_date = DateTime.Now;

            _BLL_Bills.UpdateStatus(_id, bill);
            LoadData();
        }

        private void LoadData()
        {
            dgvMain.Rows.Clear();

            dgvMain.Columns.Clear();
            dgvMain.Columns.Add("id", "ID");
            dgvMain.Columns.Add("user_name", "Người nhận");
            dgvMain.Columns.Add("payment_method", "Hình thức thanh toán");
            dgvMain.Columns.Add("payment_status", "Trình trạng thanh toán");
            dgvMain.Columns.Add("status", "Trạng thái");
            dgvMain.Columns.Add("total", "Tổng tiền");
            dgvMain.Columns.Add("delivery_date", "Ngày giao");

            var data = _BLL_Bills.GetList();
            foreach (var item in data)
            {
                dgvMain.Rows.Add(item.id, item.user.name, mapDataPaymentMethod(item.payment_method),
                    mapDataPaymentStatus(item.payment_status), mapDataStatus(item.status),
                    item.total, item.delivery_date?.ToString("yyyy-MM-dd") ?? "__");
            }

        }
        private void DgvMain_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            _id = long.Parse(dgvMain.Rows[e.RowIndex].Cells["id"].Value.ToString());
            if (e.ColumnIndex == 0 || e.ColumnIndex == 1)
            {
                _bill = _BLL_Bills.GetByID(_id);
                Frm_Bill_Details frm = new Frm_Bill_Details(_bill);
                frm.ShowDialog();
            }

            txtID.Text = dgvMain.Rows[e.RowIndex].Cells["id"].Value.ToString();
            var status = dgvMain.Rows[e.RowIndex].Cells["status"].Value.ToString();
            _status = getDataStatus(status);
            cbxStatus.SelectedItem = status;
        }


        public static string mapDataPaymentMethod(byte data)
        {
            switch (data)
            {
                case 1:
                    return "Thanh toán khi nhận hàng";
                case 2:
                    return "Thanh toán qua thẻ";
                default:
                    return "__";
            }
        }

        public static string mapDataStatus(byte data)
        {
            switch (data)
            {
                case 1:
                    return "Chờ xác nhận";
                case 2:
                    return "Đang xử lý";
                case 3:
                    return "Đang giao hàng";
                case 4:
                    return "Đã giao hàng";
                case 5:
                    return "Đã hủy";
                default:
                    return "__";
            }
        }

        public static byte getDataStatus(string data)
        {
            switch (data)
            {
                case "Chờ xác nhận":
                    return 1;
                case "Đang xử lý":
                    return 2;
                case "Đang giao hàng":
                    return 3;
                case "Đã giao hàng":
                    return 4;
                case "Đã hủy":
                    return 5;
                default:
                    return 0;
            }
        }

        public static string mapDataPaymentStatus(byte data)
        {
            switch(data)
            {
                case 1:
                    return "Đã thanh toán";
                case 2:
                    return "Chưa thanh toán";
                default:
                    return "__";
            }
        }
    }
}
