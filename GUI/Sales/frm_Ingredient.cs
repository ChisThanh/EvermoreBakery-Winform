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

namespace GUI.Sales
{
    public partial class frm_Ingredient : Form
    {
        BLL_Purchase_orders _BLL_Purchase_Orders = new BLL_Purchase_orders();
        //List<purchase_orders> purchase_Orders = new List<purchase_orders>(); 
        long _idP = 0;
        bool _isSubmit = true;
        long _iDI = 0;

        DateTime _create_at = DateTime.Now;
        DateTime _update_at = DateTime.Now;

        public frm_Ingredient()
        {
            InitializeComponent();
            this.Load += Frm_Ingredient_Load;
            btn_IngredientAdd.Click += Btn_IngredientAdd_Click;
            btn_Update.Click += Btn_Update_Click;
            btn_Reset.Click += Btn_Reset_Click;
            btn_IngredientRemove.Click += Btn_IngredientRemove_Click;
            btn_Insert.Click += Btn_Insert_Click;
            btn_Search.Click += Btn_Search_Click;
        }

        private void Btn_Search_Click(object sender, EventArgs e)
        {
            SearchAndLoadIngredients();
        }

        private void SearchAndLoadIngredients()
        {
            SetupIngredientsGrid(); // Thiết lập lại các cột

            string keyword = tbx_Search.Text.Trim();
            var ingredients = _BLL_Purchase_Orders.SearchIngredients(keyword);

            // Thêm dữ liệu vào DataGridView
            foreach (var item in ingredients)
            {
                dgv_Ingredients.Rows.Add(item.id, item.name, item.unit, item.stock_quantity);
            }

            if (ingredients.Count == 0)
            {
                MessageBox.Show("Không tìm thấy nguyên liệu nào.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Btn_Insert_Click(object sender, EventArgs e)
        {
            // Lấy giá trị từ DateTimePicker
            DateTime deliveryDate = txtEnd.Value; // txtEnd là DateTimePicker cho ngày giao hàng
            DateTime currentDate = DateTime.Now;

            // Kiểm tra delivery_date
            if (deliveryDate < currentDate.Date)
            {
                MessageBox.Show("Ngày giao hàng phải lớn hơn hoặc bằng ngày hiện tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Tạo đối tượng purchase_order mới
            var newPurchaseOrder = new purchase_orders
            {
                supplier_id = 1, // Mặc định
                order_date = currentDate,
                delivery_date = deliveryDate,
                status = "Chờ xử lý", // Mặc định
                total = 0, // Mặc định
                is_pay = 0, // Mặc định
                created_at = currentDate, // Ghi lại thời gian tạo
                updated_at = currentDate // Ghi lại thời gian cập nhật
            };

            // Gọi BLL để thêm mới
            bool isInserted = _BLL_Purchase_Orders.CreateOrUpdatePurchaseOrder(newPurchaseOrder);

            // Kiểm tra kết quả
            if (isInserted)
            {
                MessageBox.Show("Thêm đơn hàng mới thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadPurchaseOrder(); // Cập nhật lại danh sách
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra khi thêm đơn hàng mới.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Btn_IngredientRemove_Click(object sender, EventArgs e)
        {
            try
            {
                var ingredientIds = new List<long> { _iDI };

                bool isRemoved = _BLL_Purchase_Orders.RemoveIngredientsFromPurchaseOrder(_idP, ingredientIds);

                if (isRemoved)
                {
                    bool isTotalCalculated = _BLL_Purchase_Orders.CalculateTotalForPurchaseOrder(_idP);

                    if (isTotalCalculated)
                    {
                        MessageBox.Show("Xóa chi tiết nhập kho thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Xóa chi tiết nhập kho thành công, nhưng không thể cập nhật tổng tiền.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    LoadPurchaseOrderDetails(_idP);
                    LoadPurchaseOrder();
                }
                else
                {
                    MessageBox.Show("Xóa chi tiết nhập kho thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Có lỗi xảy ra: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Btn_Reset_Click(object sender, EventArgs e)
        {
            
            LoadPurchaseOrder();
            tbx_Name.Text = string.Empty;
            tbx_Price.Text = string.Empty;
            tbx_Search.Text = string.Empty;
            nbr_SL.Value = 0;
            _idP = 0;
            LoadIngredients();
        }

        private void Btn_Update_Click(object sender, EventArgs e)
        {
            DateTime deliveryDate = txtEnd.Value;
            DateTime currentDate = DateTime.Now;

            // Kiểm tra delivery_date
            if (deliveryDate < currentDate.Date)
            {
                MessageBox.Show("Ngày giao hàng phải lớn hơn hoặc bằng ngày hiện tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Kiểm tra xem có hàng nào được chọn trong DataGridView không
            if (dgv_PurchaseOrders.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một đơn hàng để cập nhật.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedRow = dgv_PurchaseOrders.SelectedRows[0];
            long orderId = Convert.ToInt64(selectedRow.Cells["id"].Value);
            double total = Convert.ToDouble(selectedRow.Cells["total"].Value);

            // Lấy giá trị trạng thái từ ComboBox
            string selectedStatus = cbx_Status.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(selectedStatus))
            {
                MessageBox.Show("Vui lòng chọn trạng thái hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (selectedStatus == "Hoàn thành" && total == 0)
            {
                MessageBox.Show("Không thể cập nhật trạng thái 'Hoàn thành' khi tổng giá trị đơn hàng bằng 0.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Chuẩn bị dữ liệu để cập nhật đơn hàng
            var orderToUpdate = new purchase_orders
            {
                id = orderId,
                status = selectedStatus,
                supplier_id = 1,
                delivery_date = deliveryDate,
                is_pay = (selectedStatus == "Hoàn thành" && total > 1000) ? (byte)1 : (byte)0
            };

            // Gọi BLL để cập nhật đơn hàng
            bool isUpdated = _BLL_Purchase_Orders.CreateOrUpdatePurchaseOrder(orderToUpdate);

            // Nếu cập nhật thành công và trạng thái là Hoàn thành + is_pay == 1, thì cập nhật số lượng tồn kho
            if (isUpdated && selectedStatus == "Hoàn thành" && orderToUpdate.is_pay == 1)
            {
                // Lấy danh sách chi tiết đơn hàng
                var orderDetails = _BLL_Purchase_Orders.GetPurchaseOrderDetailsByOrderId(orderId);

                foreach (var detail in orderDetails)
                {
                    bool isStockUpdated = _BLL_Purchase_Orders.UpdateStockQuantity(detail.ingredient_id, detail.quantity);
                    if (!isStockUpdated)
                    {
                        MessageBox.Show($"Cập nhật số lượng tồn kho thất bại cho: {detail.ingredient_id}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

            // Hiển thị thông báo kết quả
            if (isUpdated)
            {
                MessageBox.Show("Cập nhật trạng thái đơn hàng thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadPurchaseOrder(); // Cập nhật lại danh sách
                LoadIngredients();
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra khi cập nhật trạng thái đơn hàng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Btn_IngredientAdd_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy giá trị từ NumericUpDown và TextBox
                int quantity = (int)nbr_SL.Value;
                decimal priceDecimal;

                if (!decimal.TryParse(tbx_Price.Text, out priceDecimal))
                {
                    MessageBox.Show("Giá trị trong ô giá không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                double price = (double)priceDecimal;
                bool success = _BLL_Purchase_Orders.CreateOrUpdatePurchaseOrderDetail(_idP, _iDI, quantity, price);

                if (success)
                {
                    bool isTotalCalculated = _BLL_Purchase_Orders.CalculateTotalForPurchaseOrder(_idP);
                    MessageBox.Show("Thêm nguyên liệu vào chi tiết đơn hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadPurchaseOrderDetails(_idP);
                    LoadPurchaseOrder();
                }
                else
                {
                    MessageBox.Show("Thêm nguyên liệu thất bại. Vui lòng kiểm tra lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Frm_Ingredient_Load(object sender, EventArgs e)
        {
            LoadPurchaseOrder();
            LoadIngredients();
            dgv_Ingredients.AllowUserToAddRows = false;
            dgv_PurchaseOrders.AllowUserToAddRows = false;
            dgv_Details.AllowUserToAddRows = false;

            dgv_Details.CellClick += Dgv_Details_CellClick;
            dgv_PurchaseOrders.CellClick += Dgv_PurchaseOrders_CellClick;
            dgv_Ingredients.CellClick += Dgv_Ingredients_CellClick;
            txtEnd.Value = DateTime.Now;
        }


        public void LoadPurchaseOrder()
        {
            dgv_PurchaseOrders.Rows.Clear();
            dgv_PurchaseOrders.Columns.Clear();

            dgv_PurchaseOrders.Columns.Add("id", "Mã ĐH");
            dgv_PurchaseOrders.Columns.Add("order_date", "Ngày đặt");
            dgv_PurchaseOrders.Columns.Add("delivery_date", "Ngày giao");
            dgv_PurchaseOrders.Columns.Add("status", "Trạng thái");
            dgv_PurchaseOrders.Columns.Add("total", "Tổng tiền");
            DataGridViewCheckBoxColumn isPayColumn = new DataGridViewCheckBoxColumn
            {
                Name = "is_pay",
                HeaderText = "Thanh toán",
                TrueValue = true,    // Giá trị true trong cơ sở dữ liệu
                FalseValue = false,  // Giá trị false trong cơ sở dữ liệu
                DataPropertyName = "is_pay" // Gán cột này với thuộc tính trong dữ liệu
            };
            dgv_PurchaseOrders.Columns.Add(isPayColumn);
            //dgv_PurchaseOrders.Columns.Add("created_at", "Ngày tạo");
            dgv_PurchaseOrders.Columns.Add("updated_at", "Ngày cập nhật");
            dgv_PurchaseOrders.Columns["updated_at"].Visible = false;
            dgv_PurchaseOrders.Columns["is_pay"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            var data = _BLL_Purchase_Orders.GetList();
            foreach (var item in data)
            {
                dgv_PurchaseOrders.Rows.Add(item.id,item.order_date,item.delivery_date,item.status,item.total,item.is_pay, item.updated_at);

            }
            // Kiểm tra nếu có dữ liệu trước khi xử lý _idP
            if (_idP == 0 && dgv_PurchaseOrders.Rows.Count > 0)
            {
                var id = dgv_PurchaseOrders.Rows[0].Cells[0].Value;
                if (id != null)
                {
                    _idP = long.Parse(id.ToString());
                }
            }
            
            // Chỉ tải chi tiết nếu _idP hợp lệ
            if (_idP > 0)
            {
                LoadPurchaseOrderDetails(_idP);
            }
        }
        public void LoadPurchaseOrderDetails(long purchaseOrderId)
        {
            dgv_Details.Rows.Clear();
            dgv_Details.Columns.Clear();

            dgv_Details.Columns.Add("purchase_order_id", "Mã ĐH");
            dgv_Details.Columns.Add("ingredient_id","Mã NL");
            dgv_Details.Columns["ingredient_id"].Visible = false;
            dgv_Details.Columns.Add("name", "Tên sản phẩm");
            dgv_Details.Columns.Add("quantity", "Số lượng");
            dgv_Details.Columns.Add("price", "Đơn giá");

            var data = _BLL_Purchase_Orders.GetPurchaseOrderDetailsByOrderId(purchaseOrderId);

            foreach (var item in data)
            {
                dgv_Details.Rows.Add(item.purchase_order_id, item.ingredient_id, item.ingredient.name, item.quantity, item.price);
            }
        }
        private void Dgv_PurchaseOrders_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var id = dgv_PurchaseOrders.Rows[e.RowIndex].Cells["id"].Value;
                _idP = long.Parse(id.ToString());
                var status = dgv_PurchaseOrders.Rows[e.RowIndex].Cells["status"].Value?.ToString();
                var isPay = dgv_PurchaseOrders.Rows[e.RowIndex].Cells["is_pay"].Value;
                var deliveryDate = dgv_PurchaseOrders.Rows[e.RowIndex].Cells["delivery_date"].Value;
                //txtEnd.Value= DateTime.Parse(deliveryDate.ToString());
                if (deliveryDate != null && DateTime.TryParse(deliveryDate.ToString(), out DateTime parsedDate))
                {
                    if (parsedDate >= txtEnd.MinDate && parsedDate <= txtEnd.MaxDate)
                    {
                        txtEnd.Value = parsedDate; 
                    }
                    else
                    {
                        txtEnd.Value = DateTime.Now; 
                    }
                }
                else
                {
                    txtEnd.Value = DateTime.Now;
                }

                int pay= int.Parse(isPay.ToString());
                if (status == "Hoàn thành" || pay == 1)
                {
                    // Đóng nút nếu đơn hàng hoàn thành hoặc đã thanh toán
                    btn_IngredientAdd.Enabled = false;
                    btn_IngredientRemove.Enabled = false;
                    btn_Update.Enabled = false;
                }
                else
                {
                    // Mở nút nếu không phải trạng thái hoàn thành hoặc chưa thanh toán
                    btn_IngredientAdd.Enabled = true;
                    btn_IngredientRemove.Enabled = true;
                    btn_Update.Enabled = true;
                }

                LoadPurchaseOrderDetails(_idP);
            }
        }
        private void Dgv_Ingredients_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {

                var selectedValue = dgv_Ingredients.Rows[e.RowIndex].Cells["name"].Value;
                var idI = dgv_Ingredients.Rows[e.RowIndex].Cells["id"].Value;
                _iDI = long.Parse(idI.ToString());

                if (selectedValue != null)
                {
                    tbx_Name.Text = selectedValue.ToString();
                    tbx_Price.Text = string.Empty;
                    nbr_SL.Value = 0;
                }
                else
                {
                    tbx_Name.Text = string.Empty; // Xóa TextBox nếu giá trị null
                }
            }
        }

        private void Dgv_Details_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {

                var id= dgv_Details.Rows[e.RowIndex].Cells["purchase_order_id"].Value;
                _idP = long.Parse(id.ToString());
                var name = dgv_Details.Rows[e.RowIndex].Cells["name"].Value;
                var quantity = dgv_Details.Rows[e.RowIndex].Cells["quantity"].Value;
                var price = dgv_Details.Rows[e.RowIndex].Cells["price"].Value;
                var ingredient_id = dgv_Details.Rows[e.RowIndex].Cells["ingredient_id"].Value;
                _iDI=long.Parse(ingredient_id.ToString());
                tbx_Name.Text =name.ToString() ;
                nbr_SL.Value = int.Parse(quantity.ToString());
                tbx_Price.Text = price.ToString();

            }
        }
        public void LoadIngredients()
        {
            SetupIngredientsGrid(); // Thiết lập lại các cột

            // Lấy dữ liệu từ BLL
            var data = _BLL_Purchase_Orders.GetIngredients();

            // Thêm dữ liệu vào DataGridView
            foreach (var item in data)
            {
                dgv_Ingredients.Rows.Add(item.id, item.name, item.unit, item.stock_quantity);
            }
        }

        private void SetupIngredientsGrid()
        {
            dgv_Ingredients.Rows.Clear(); // Xóa tất cả các hàng hiện tại
            dgv_Ingredients.Columns.Clear(); // Xóa tất cả các cột hiện tại

            // Thêm các cột vào DataGridView 
            dgv_Ingredients.Columns.Add(new DataGridViewTextBoxColumn { Name = "id", HeaderText = "ID", Visible = false });
            dgv_Ingredients.Columns.Add("name", "Tên nguyên liệu");
            dgv_Ingredients.Columns.Add("unit", "Đơn vị tính");
            dgv_Ingredients.Columns.Add("stock_quantity", "Số lượng tồn");
        }

    }
}
