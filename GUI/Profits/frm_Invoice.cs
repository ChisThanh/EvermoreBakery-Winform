using BLL;
using DTO;
using GUI.Bills;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI.Profits
{
    public partial class frm_Invoice : Form
    {
        protected BLL_Bills _BLL_Bills = new BLL_Bills();
        protected ApiClient _apiClient = new ApiClient();

        bill _bill = null;
        int _status = 0;
        long _id = 0;

        private CancellationTokenSource _cts;

        public frm_Invoice()
        {
            InitializeComponent();
            Load += Frm_Invoice_Load;
        }

        private void Frm_Invoice_Load(object sender, EventArgs e)
        {
            LoadData();

            dgv_Bills.CellClick += Dgv_Bills_CellClick; ;
            dgv_Bills.AllowUserToAddRows = false;
            dgv_Bills.AllowUserToAddRows = false;

            cbx_Status.Items.Add("Chờ xác nhận");
            cbx_Status.Items.Add("Đang xử lý");
            cbx_Status.Items.Add("Đang giao hàng");
            cbx_Status.Items.Add("Đã giao hàng");
            cbx_Status.Items.Add("Đã hủy");
            cbx_Status.SelectedIndex = 0;
            cbx_Status.SelectedValueChanged += Cbx_Status_SelectedValueChanged;
            btn_Load.Click += (s, e2) => LoadData();

            StartAutoRefreshAsync();
            FormClosing += (s, e1) => StopAutoRefresh();
            DesignTbx_BillId();
            btnPDF.Click += BtnPDF_Click;
        }

        private void BtnPDF_Click(object sender, EventArgs e)
        {
            if (_bill == null) return;

            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                folderDialog.Description = "Chọn thư mục để lưu file PDF";

                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedFolder = folderDialog.SelectedPath;

                    var path = Path.Combine(selectedFolder, $"Invoice_{DateTime.Now.ToString("MMddHHmmss")}.pdf");

                    var result = _apiClient.DownloadPdfAsync(path, _bill.id.ToString());
                    
                    MessageBox.Show("Tạo file PDF thành công.");
                }
                else MessageBox.Show("Bạn chưa chọn thư mục để lưu file.");
            }
        } 

        private void Cbx_Status_SelectedValueChanged(object sender, EventArgs e)
        {
            if (_id == 0) return;
            if (_status == cbx_Status.SelectedIndex + 1) return;

            var bill = new bill();
            bill.id = _id;
            bill.status = (byte)(cbx_Status.SelectedIndex + 1);
            if (cbx_Status.SelectedIndex + 1 == 3)
                bill.delivery_date = DateTime.Now;

            _BLL_Bills.UpdateStatus(_id, bill);
            LoadData();
        }

        private void Dgv_Bills_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            _id = long.Parse(dgv_Bills.Rows[e.RowIndex].Cells["id"].Value.ToString());
            if (e.ColumnIndex == 0 || e.ColumnIndex == 1)
            {
                _bill = _BLL_Bills.GetByID(_id);
                LoadDetails(_bill);
            }

            tbx_BillId.Text = dgv_Bills.Rows[e.RowIndex].Cells["id"].Value.ToString();
            var status = dgv_Bills.Rows[e.RowIndex].Cells["status"].Value.ToString();
            _status = getDataStatus(status);
            cbx_Status.SelectedItem = status;
        }

        private async void StartAutoRefreshAsync()
        {
            _cts = new CancellationTokenSource();
            try
            {
                while (!_cts.Token.IsCancellationRequested)
                {
                    LoadData();
                    await Task.Delay(TimeSpan.FromSeconds(10), _cts.Token);
                }
            }
            catch (TaskCanceledException)
            {

            }
        }

        private void StopAutoRefresh()
        {
            _cts?.Cancel();
            _cts?.Dispose();
        }

        private void LoadData()
        {
            dgv_Bills.Rows.Clear();

            dgv_Bills.Columns.Clear();
            dgv_Bills.Columns.Add("id", "ID");
            dgv_Bills.Columns.Add("user_name", "Người Nhận");
            dgv_Bills.Columns.Add("payment_method", "Hình Thức");
            dgv_Bills.Columns.Add("payment_status", "Tình Trạng");
            dgv_Bills.Columns.Add("status", "Trạng Thái");
            dgv_Bills.Columns.Add("total", "Tổng Tiền");
            dgv_Bills.Columns.Add("delivery_date", "Ngày Giao");

            var data = _BLL_Bills.GetList();
            foreach (var item in data)
            {
                dgv_Bills.Rows.Add(item.id, item.user.name, mapDataPaymentMethod(item.payment_method),
                    mapDataPaymentStatus(item.payment_status), mapDataStatus(item.status),
                    item.total, item.delivery_date?.ToString("yyyy-MM-dd") ?? "__");
            }
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
            switch (data)
            {
                case 1:
                    return "Đã thanh toán";
                case 2:
                    return "Chưa thanh toán";
                default:
                    return "__";
            }
        }

        public void LoadDetails(bill _bill)
        {
            tbx_Name.Text = _bill.user.name;
            tbx_Delivery.Text = _bill.delivery_date?.ToString() ?? "__";
            tbx_Total.Text = _bill.total.ToString();
            tbx_Status.Text = Frm_Bills.mapDataPaymentStatus(_bill.payment_status);
            tbx_Status2.Text = Frm_Bills.mapDataStatus(_bill.status);
            tbx_Method.Text = Frm_Bills.mapDataPaymentMethod(_bill.payment_method);
            tbx_Coupon.Text = _bill.coupon?.code?.ToString() ?? "__";
            tbx_Note.Text = _bill.note;
            tbx_Phone.Text = _bill.bill_address.phone;
            tbx_Address.Text = _bill.bill_address.street + " " + _bill.bill_address.ward + " " + _bill.bill_address.district + " " + _bill.bill_address.city;

            var products = _bill.bill_details.Select(x => new
            {
                x.product.name,
                x.quantity,
                x.price,
            }).ToList();

            dgv_Products.Rows.Clear();
            dgv_Products.Columns.Add("name", "Tên Sản Phẩm");
            dgv_Products.Columns.Add("quantity", "Số Lượng");
            dgv_Products.Columns.Add("price", "Giá");

            foreach (var item in products)
            {
                dgv_Products.Rows.Add(item.name, item.quantity, item.price);
            }
        }

        public void DesignTbx_BillId()
        {
            tbx_BillId.IconRight = null;
            tbx_BillId.GotFocus += (s, e) =>
            {
                tbx_BillId.IconLeft = Properties.Resources.search_tertiary;
                if (!string.IsNullOrEmpty(tbx_BillId.Text)) tbx_BillId.IconRight = Properties.Resources.xmark_tertiary;
            };
            tbx_BillId.LostFocus += (s, e) =>
            {
                tbx_BillId.IconLeft = Properties.Resources.search_primary;
                if (!string.IsNullOrEmpty(tbx_BillId.Text)) tbx_BillId.IconRight = Properties.Resources.xmark_primary;
            };
            tbx_BillId.MouseHover += (s, e) =>
            {
                if (!tbx_BillId.Focused)
                {
                    tbx_BillId.IconLeft = Properties.Resources.search_tertiary;
                    if (!string.IsNullOrEmpty(tbx_BillId.Text)) tbx_BillId.IconRight = Properties.Resources.xmark_tertiary;
                }
            };
            tbx_BillId.MouseLeave += (s, e) =>
            {
                if (!tbx_BillId.Focused)
                {
                    tbx_BillId.IconLeft = Properties.Resources.search_primary;
                    if (!string.IsNullOrEmpty(tbx_BillId.Text)) tbx_BillId.IconRight = Properties.Resources.xmark_primary;
                }
            };
            tbx_BillId.TextChanged += (s, e) =>
            {
                if (string.IsNullOrEmpty(tbx_BillId.Text)) tbx_BillId.IconRight = null;
                else tbx_BillId.IconRight = Properties.Resources.xmark_tertiary;
            };
            tbx_BillId.IconRightClick += (s, e) =>
            {
                tbx_BillId.Clear();
                tbx_BillId.IconRight = null;
            };
        }

    }
}
