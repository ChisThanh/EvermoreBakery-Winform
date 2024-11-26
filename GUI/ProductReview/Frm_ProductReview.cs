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
        }

        private void Frm_ProductReview_Load(object sender, EventArgs e)
        {
            LoadData();   
        }

        public void LoadData()
        {
            var lstProductReviews = _bllProductReview.GetList();
            dgvMain.Rows.Clear();
            dgvMain.Columns.Add("id", "#");
            dgvMain.Columns.Add("product", "Tên sản phẩm");
            dgvMain.Columns.Add("user", "Người đánh giá");
            dgvMain.Columns.Add("comment", "Bình luận");
            dgvMain.Columns.Add("is_show", "Hiển thị");
            dgvMain.Columns.Add("created_at", "Ngày đánh giá");

            if (lstProductReviews == null)
            {
                MessageBox.Show("Không có dữ liệu");
                return;
            }
            foreach (var item in lstProductReviews)
                dgvMain.Rows.Add(item.id, item.product.name, item.user.name, item.comment,
                    item.is_show.HasValue && item.is_show.Value ? "Hiện" : "Ản", item.created_at);
        }
    }
}
