using BLL;
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

namespace GUI.ProductReview
{
    public partial class Frm_ProductReview : Form
    {
        BLL_ProductReview _bllProductReview = new BLL_ProductReview();

        public Frm_ProductReview()
        {
            InitializeComponent();
            this.Load += Frm_ProductReview_Load;
            dgvMain.AllowUserToAddRows = false;
        }

        private void Frm_ProductReview_Load(object sender, EventArgs e)
        {
            LoadData();
            dgvMain.CellClick += ClickCheckBox;
        }

        public void LoadData()
        {
            var lstProductReviews = _bllProductReview.GetList();
            dgvMain.Rows.Clear();
            dgvMain.Columns.Add("id", "#");
            dgvMain.Columns.Add("product", "Tên sản phẩm");
            dgvMain.Columns.Add("user", "Người đánh giá");
            dgvMain.Columns.Add("comment", "Bình luận");
            dgvMain.Columns.Add("created_at", "Ngày đánh giá");

            var checkBoxColumn = new DataGridViewCheckBoxColumn
            {
                Name = "is_show",
                HeaderText = "Ẩn/Hiện",
                Width = 10,
                TrueValue = true,
                FalseValue = false
            };
            dgvMain.Columns.Add(checkBoxColumn);

            if (lstProductReviews == null)
            {
                MessageBox.Show("Không có dữ liệu");
                return;
            }

            foreach (var item in lstProductReviews)
            {
                var isShowValue = item.is_show ?? false;
                dgvMain.Rows.Add(item.id, item.product.name, item.user.name, item.comment, item.created_at, isShowValue);
            }
        }

        public void ClickCheckBox(object sender, DataGridViewCellEventArgs e)
        {
            var dgv = (DataGridView)sender;
            if (dgv.Columns[e.ColumnIndex] is DataGridViewCheckBoxColumn)
            {
                var id = (long)dgv.Rows[e.RowIndex].Cells["id"].Value;
                var isShow = (bool)dgv.Rows[e.RowIndex].Cells["is_show"].Value;

                if (_bllProductReview.updateIsShow(id, !isShow)) return;
                else MessageBox.Show("Có lỗi xãy ra vui lòng hỏi thằng nào làm ra cái này");
            }
        }
    }
}
