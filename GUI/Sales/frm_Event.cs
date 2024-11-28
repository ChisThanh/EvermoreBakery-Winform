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

namespace GUI.Sales
{
    public partial class frm_Event : Form
    {
        BLL_Events _BLL_Events = new BLL_Events();
        List<product> _products = EvermoreBakeryContext.Instance.products.ToList();

        long _idE = 0;
        bool _isSubmit = true;

        string _name = "";
        string _description = "";
        DateTime _start_date = DateTime.Now;
        DateTime _end_date = DateTime.Now;

        public frm_Event()
        {
            InitializeComponent();
            this.Load += Frm_Event_Load;
        }

        private void Frm_Event_Load(object sender, EventArgs e)
        {
            LoadDataEvent();
            dgvMain.AllowUserToAddRows = false;
            dgvPro.AllowUserToAddRows = false;

            btnDelProEv.Click += BtnDelProEv_Click;
            btnAddProEv.Click += BtnAddProEv_Click;
            btnSaveEv.Click += BtnSaveEv_Click;
            btnPriceSale.Click += BtnPriceSale_Click;

            dgvPro.CellClick += DgvPro_CellClick;
            dgvMain.CellClick += DgvMain_CellClick;
        }

        private void BtnPriceSale_Click(object sender, EventArgs e)
        {
            if (_idE == 0) return;
            if (_BLL_Events.CalculatePriceSave(_idE)) MessageBox.Show("Tính giá thành công");
            else MessageBox.Show("Có lỗi xãy ra vui lòng hỏi thằng nào làm ra cái này");
        }

        private void BtnSaveEv_Click(object sender, EventArgs e)
        {
            getTxt();

            if (!_isSubmit) return;

            if (cbEvNew.Checked) _idE = 0;

            var _Event = new _event
            {
                id = _idE,
                name = _name,
                description = _description,
                start_date = _start_date,
                end_date = _end_date
            };

            if (_BLL_Events.CreateOrUpdateEv(_Event))
            {
                MessageBox.Show("Lưu sự kiện thành công");
                LoadDataEvent();
                cbEvNew.Checked = false;
            }
            else
            {
                MessageBox.Show("Có lỗi xãy ra vui lòng hỏi thằng nào làm ra cái này");
            }
        }

        private void BtnDelProEv_Click(object sender, EventArgs e)
        {
            var ids = getIdsChecked();
            if (ids.Count == 0) return;

            if (_BLL_Events.RemoveProductFromEvent(_idE, ids)) LoadDataEventPro(_idE);
            else MessageBox.Show("Có lỗi xãy ra vui lòng hỏi thằng nào làm ra cái này");
        }

        private void DgvPro_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var id = dgvPro.Rows[e.RowIndex].Cells["id"].Value;
            var idPro = long.Parse(id.ToString());

            var per = dgvPro.Rows[e.RowIndex].Cells["percentage"].Value;
            var percentage = int.Parse(per.ToString());

            cbxPro.SelectedValue = idPro;
            txtPer.Text = percentage.ToString();
        }

        private void BtnAddProEv_Click(object sender, EventArgs e)
        {
            if (_idE == 0) return;

            var proId = long.Parse(cbxPro.SelectedValue.ToString());
            var percentage = int.Parse(txtPer.Text);

            if(percentage < 0 || percentage > 100)
            {
                MessageBox.Show("Phần trăm giảm giá phải từ 0 đến 100");
                return;
            }

            if (_BLL_Events.CreateOrUpdateProEv(_idE, proId, percentage)) LoadDataEventPro(_idE);
            else MessageBox.Show("Có lỗi xãy ra vui lòng hỏi thằng nào làm ra cái này");
        }

        public void LoadDataEvent()
        {
            dgvMain.Rows.Clear();
            dgvMain.Columns.Clear();

            dgvMain.Columns.Add("id", "#");
            dgvMain.Columns.Add("name", "Tiêu đề");
            dgvMain.Columns.Add("description", "Mô tả");
            dgvMain.Columns.Add("start_date", "Ngày bắt đầu");
            dgvMain.Columns.Add("end_date", "Ngày kết thúc");

            var actionColumn = new DataGridViewImageColumn
            {
                Image = Properties.Resources.cake_primary,
                ImageLayout = DataGridViewImageCellLayout.Zoom,
                ReadOnly = true,
                Name = "Xóa",
            };

            dgvMain.Columns.Add(actionColumn);

            var data = _BLL_Events.GetList();

            foreach (var item in data)
                dgvMain.Rows.Add(item.id, item.name, item.description, item.start_date, item.end_date);

            if (_idE == 0 && dgvMain.Rows.Count > 0)
            {
                var id = dgvMain.Rows[0].Cells[0].Value;
                _idE = long.Parse(id.ToString());
            }

            LoadDataEventPro(_idE);
            LoadCBXProAsync();
        }


        private void DgvMain_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > 0)
            {
                var id = dgvMain.Rows[e.RowIndex].Cells[0].Value;
                _idE = long.Parse(id.ToString());

                LoadDataEventPro(_idE);

                _name = dgvMain.Rows[e.RowIndex].Cells[1].Value.ToString();
                _description = dgvMain.Rows[e.RowIndex].Cells[2].Value.ToString();
                _start_date = DateTime.Parse(dgvMain.Rows[e.RowIndex].Cells[3].Value.ToString());
                _end_date = DateTime.Parse(dgvMain.Rows[e.RowIndex].Cells[4].Value.ToString());
                setTxt();
            }

            if (e.ColumnIndex == 5)
            {
                var id = dgvMain.Rows[e.RowIndex].Cells[0].Value;
                var idEvent = long.Parse(id.ToString());

                if (MessageBox.Show("Bạn có chắc chắn muốn xóa sự kiện này không", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (_BLL_Events.DeleteEvent(idEvent)) LoadDataEvent();
                    else MessageBox.Show("Có lỗi xãy ra vui lòng hỏi thằng nào làm ra cái này");
                }
            }
        }

        public Task LoadDataEventPro(long id)
        {
            dgvPro.Invoke((Action)(() =>
            {
                dgvPro.Rows.Clear();
                dgvPro.Columns.Clear();

                var checkBoxColumn = new DataGridViewCheckBoxColumn
                {
                    Name = "checked",
                    HeaderText = "Chọn Xóa",
                    Width = 10,
                    TrueValue = true,
                    FalseValue = false
                };
                dgvPro.Columns.Add(checkBoxColumn);
                dgvPro.Columns.Add("id", "#");
                dgvPro.Columns.Add("name", "Tên sản phẩm");
                dgvPro.Columns.Add("percentage", "Phần trăm giảm giá");
            }));

            return Task.Run(() =>
            {
                var data = _BLL_Events.GetProductsByEvent(id);

                dgvPro.Invoke((Action)(() =>
                {
                    foreach (var item in data)
                        dgvPro.Rows.Add(false, item.id, item.name, item.percentage);
                }));
            });
        }

        public Task LoadCBXProAsync()
        {
            return Task.Run(() =>
            {
                var products = _products;
                cbxPro.Invoke((Action)(() =>
                {
                    cbxPro.DataSource = products;
                    cbxPro.DisplayMember = "name";
                    cbxPro.ValueMember = "id";
                }));
            });
        }

        public void getTxt()
        {
            _name = txtName.Text;
            _description = txtDes.Text;
            _start_date = txtStart.Value;
            _end_date = txtEnd.Value;

            if (
                txtName.Text == "" ||
                txtDes.Text == "" ||
                txtStart.Value == null ||
                txtEnd.Value == null)
            {
                MessageBox.Show("Vui lòng chọn sự kiện cần sửa");
                _isSubmit = false;
                return;
            }
        }

        public void setTxt()
        {
            txtName.Text = _name;
            txtDes.Text = _description;
            txtStart.Value = _start_date;
            txtEnd.Value = _end_date;
        }

        public List<long> getIdsChecked()
        {
            var ids = new List<long>();
            for (int i = 0; i < dgvPro.Rows.Count; i++)
            {
                var check = dgvPro.Rows[i].Cells[0].Value;
                if (check != null && (bool)check)
                {
                    var id = dgvPro.Rows[i].Cells[1].Value;
                    ids.Add(long.Parse(id.ToString()));
                }
            }
            return ids;
        }
    }
}
