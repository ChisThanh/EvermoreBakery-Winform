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

namespace GUI.Bills
{
    public partial class Frm_Bill_Details : Form
    {
        bill _bill;
        public Frm_Bill_Details(bill _bill)
        {
            InitializeComponent();
            this._bill = _bill;
            this.Load += Frm_Bill_Details_Load;
        }

        private void Frm_Bill_Details_Load(object sender, EventArgs e)
        {
            txtUserName.Text = _bill.user.name;
            txtDeliveryDate.Text = _bill.delivery_date?.ToString() ?? "__";
            txtTotal.Text = _bill.total.ToString();
            txtPaymentStatus.Text = Frm_Bills.mapDataPaymentStatus( _bill.payment_status);
            txtStatus.Text = Frm_Bills.mapDataStatus(_bill.status);
            txtPaymentMethod.Text = Frm_Bills.mapDataPaymentMethod(_bill.payment_method);
            txtCoupon.Text = _bill.created_at.ToString();
            txtNote.Text = _bill.note;
            txtPhone.Text = _bill.bill_address.phone;
            txtAddress.Text = _bill.bill_address.street + " " + _bill.bill_address.ward + " " + _bill.bill_address.district + " " + _bill.bill_address.city;

            var products = _bill.bill_details.Select(x => new
            {
                x.product.name,
                x.quantity,
                x.price,
            }).ToList();

            dgvMain.Rows.Clear();
            dgvMain.Columns.Add("name", "Tên sản phẩm");
            dgvMain.Columns.Add("quantity", "Số lượng");
            dgvMain.Columns.Add("price", "Giá");

            foreach (var item in products)
            {
                dgvMain.Rows.Add(item.name, item.quantity, item.price);
            }
        }
    }
}
