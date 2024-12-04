using GUI.Component;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using DTO;
using Guna.UI2.WinForms;

namespace GUI.Sales
{
    public partial class pnl_PaddingMiddle : Form
    {
        private cpn_Product selectedProduct;
        private BLL_Products bllProducts;
        private BLL_Categories bllCategories;
        private BLL_Ingredients bllIngredients;
        private BLL_Nutritions bllNutritions;

        string ingredientName = "";
        string nutritionName = "";

        public pnl_PaddingMiddle()
        {
            InitializeComponent();
            Load += Pnl_PaddingMiddle_Load;
        }

        private void Pnl_PaddingMiddle_Load(object sender, EventArgs e)
        {
            InitializeGraphics();
            InitializeBLLs();
            InitializeData();

            btn_Reset.Click += Btn_Reset_Click;
            btn_Insert.Click += Btn_Insert_Click;
            btn_Update.Click += Btn_Update_Click;
            btn_Delete.Click += Btn_Delete_Click;
            btn_IngredientAdd.Click += Btn_IngredientAdd_Click;
            btn_NutritionAdd.Click += Btn_NutritionAdd_Click;
            btn_IngredientRemove.Click += Btn_IngredientRemove_Click;
            btn_NutritionRemove.Click += Btn_NutritionRemove_Click;
        }

        private void Btn_NutritionRemove_Click(object sender, EventArgs e)
        {
            if (selectedProduct == null)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần cập nhật", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(nutritionName))
            {
                MessageBox.Show("Vui lòng chọn dinh dưỡng cần xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var productId = selectedProduct.product.id;
            if (bllNutritions.DelToProduct(productId, nutritionName))
            {
                MessageBox.Show("Xóa thành công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadNutritionssByProduct(bllNutritions.GetByProduct(productId));
            }
            else
            {
                MessageBox.Show("Xóa thất bại hỏi thằng nào làm ra cái này", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Btn_IngredientRemove_Click(object sender, EventArgs e)
        {
            if (selectedProduct == null)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần cập nhật", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(ingredientName))
            {
                MessageBox.Show("Vui lòng chọn nguyên liệu cần xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var productId = selectedProduct.product.id;
            if (bllIngredients.DelToProduct(productId, ingredientName))
            {
                MessageBox.Show("Xóa thành công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadIngredientsByProduct(bllIngredients.GetByProduct(productId));
            }
            else
            {
                MessageBox.Show("Xóa thất bại hỏi thằng nào làm ra cái này", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Btn_NutritionAdd_Click(object sender, EventArgs e)
        {
            if (selectedProduct == null)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần cập nhật", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cbx_Nutrition.SelectedIndex == 0)
            {
                MessageBox.Show("Vui lòng chọn dinh dưỡng cần thêm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var productId = selectedProduct.product.id;
            var nutritionId = long.Parse(cbx_Nutrition.SelectedValue.ToString());
            var quantity = byte.Parse(tbx_Quantity.Text);

            if (bllNutritions.AddToProduct(productId, nutritionId, quantity))
            {
                MessageBox.Show("Thêm thành công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadNutritionssByProduct(bllNutritions.GetByProduct(productId));
            }
            else
            {
                MessageBox.Show("Thêm Thất bại hỏi thằng nào làm ra cái này", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Btn_IngredientAdd_Click(object sender, EventArgs e)
        {
            if (selectedProduct == null)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần cập nhật", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cbx_Ingredient.SelectedIndex == 0)
            {
                MessageBox.Show("Vui lòng chọn nguyên liệu cần thêm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            var productId = selectedProduct.product.id;
            var ingredientId = long.Parse(cbx_Ingredient.SelectedValue.ToString());

            if (bllIngredients.AddToProduct(productId, ingredientId))
            {
                MessageBox.Show("Thêm thành công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadIngredientsByProduct(bllIngredients.GetByProduct(productId));
            }
            else
            {
                MessageBox.Show("Thêm Thất bại hỏi thằng nào làm ra cái này", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Btn_Delete_Click(object sender, EventArgs e)
        {
            if(Program.userAuth.HasPermissions("all-delete") == false)
            {
                MessageBox.Show("Bạn không có quyền thực hiện hành động này", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (selectedProduct == null)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần cập nhật", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (bllProducts.SoftDelete(selectedProduct.product.id))
            {
                MessageBox.Show("Xóa thành công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadProducts(bllProducts.GetList());
            }
            else
            {
                MessageBox.Show("Xóa Thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Btn_Update_Click(object sender, EventArgs e)
        {
            if (Program.userAuth.HasPermissions("all-update") == false)
            {
                MessageBox.Show("Bạn không có quyền thực hiện hành động này", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (selectedProduct == null)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần cập nhật", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            product product = new product()
            {
                id = selectedProduct.product.id,
                name = tbx_Name.Text,
                slug = SlugHelper.ToSlug(tbx_Name.Text),
                category_id = Convert.ToInt64(cbx_Category.SelectedValue),
                price = long.Parse(tbx_Price.Text),
                price_sale = long.Parse(tbx_Price.Text),
                description = tbx_Description.Text,
                is_display = chk_Display.Checked,
            };
            if (bllProducts.Update(product.id, product) != null)
            {
                MessageBox.Show("Cập nhật thành công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadProducts(bllProducts.GetList());
            }
            else
            {
                MessageBox.Show("Cập nhật Thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Btn_Insert_Click(object sender, EventArgs e)
        {
            product product = new product()
            {
                name = tbx_Name.Text,
                slug = SlugHelper.ToSlug(tbx_Name.Text),
                category_id = Convert.ToInt64(cbx_Category.SelectedValue),
                price = long.Parse(tbx_Price.Text),
                price_sale = long.Parse(tbx_Price.Text),
                description = tbx_Description.Text,
                is_display = chk_Display.Checked,
            };
            if (bllProducts.AddAsync(product) != null)
            {
                MessageBox.Show("Thêm thành công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadProducts(bllProducts.GetList());
            }
            else
            {
                MessageBox.Show("Thêm Thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Btn_Reset_Click(object sender, EventArgs e)
        {
            tbx_Name.Clear();
            tbx_Description.Clear();
            tbx_Price.Clear();
            tbx_Search.Clear();
            tbx_Quantity.Clear();
            tbx_Love.Clear();
            cbx_Category.SelectedIndex = 0;
            cbx_Ingredient.SelectedIndex = 0;
            cbx_Nutrition.SelectedIndex = 0;
            dgv_Ingredients.DataSource = null;
            if(selectedProduct != null) dgv_Ingredients.Columns.AddRange(new DataGridViewColumn[] { dgv_Ingredients_Name });
            dgv_Nutritions.DataSource = null;
            if (selectedProduct != null) dgv_Nutritions.Columns.AddRange(new DataGridViewColumn[] { dgv_Nutritions_Quantity, dgv_Nutritions_Name });
            chk_Display.Checked = false;
            pbx_Image.Image = null;
            selectedProduct = null;
            LoadProducts(bllProducts.GetList());
        }

        #region Data

        public void InitializeData()
        {
            //List<product> products = await bllProducts.GetListAsync();
            //List<category> categories = await bllCategories.GetListAsync();

            //Invoke((Action)(() =>
            //{
            //    LoadData(products);
            //    LoadCategories(categories);
            //}));

            LoadProducts(bllProducts.GetList());
            LoadCategories(bllCategories.GetList());
            LoadIngredients(bllIngredients.GetList());
            LoadNutritions(bllNutritions.GetList());
        }

        private void LoadIngredientsByProduct(List<object> ingredients)
        {
            dgv_Ingredients.DataSource = ingredients;
            dgv_Ingredients.CellClick += Dgv_Ingredients_CellClick;
            DesignDgv_Ingredients();
        }

        

        private void Dgv_Ingredients_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgv_Ingredients.Rows[e.RowIndex];
                ingredientName = row.Cells["dgv_Ingredients_Name"].Value.ToString();
            }
        }

        private void LoadNutritionssByProduct(List<object> nutritions)
        {
            dgv_Nutritions.DataSource = nutritions;
            dgv_Nutritions.CellClick += Dgv_Nutritions_CellClick;
            DesignDgv_Nutritions();
        }

        private void Dgv_Nutritions_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgv_Nutritions.Rows[e.RowIndex];
                nutritionName = row.Cells["dgv_Nutritions_Name"].Value.ToString();
            }
        }

        private void LoadCategories(List<category> categories)
        {
            cbx_Category.DataSource = categories;
            cbx_Category.DisplayMember = "name";
            cbx_Category.ValueMember = "id";
        }

        private void LoadIngredients(List<ingredient> ingredients)
        {
            cbx_Ingredient.DataSource = ingredients;
            cbx_Ingredient.DisplayMember = "name";
            cbx_Ingredient.ValueMember = "id";
        }

        private void LoadNutritions(List<nutrition> nutritions)
        {
            cbx_Nutrition.DataSource = nutritions;
            cbx_Nutrition.DisplayMember = "name";
            cbx_Nutrition.ValueMember = "id";
        }

        private void LoadProducts(List<product> products)
        {
            pnl_Products.Controls.Clear();

            int topMargin = 15;
            int leftMargin = 15;
            int componentWidth = 150;
            int componentHeight = 175;
            int componentPerRow = 4;

            for (int i = 0; i < products.Count; i++)
            {
                cpn_Product productComponent = new cpn_Product(products[i], bllProducts.GetImageByProduct(products[i].id));

                int row = i / componentPerRow;
                int column = i % componentPerRow;

                productComponent.Top = topMargin + (row * (componentHeight + topMargin));
                productComponent.Left = leftMargin + (column * (componentWidth + leftMargin));

                productComponent.clicked += (s, e) =>
                {
                    var component = s as cpn_Product;

                    if (selectedProduct != null && component != selectedProduct)
                    {
                        selectedProduct.ChangeDefaultColor();
                        selectedProduct.selected = false;
                    }

                    selectedProduct = component;
                    component.ChangeSelectColor();
                    component.selected = true;

                    PushSelectedData(component.product, component.trueImage);
                };
                productComponent.hoverIn += (s, e) => { };
                productComponent.hoverOut += (s, e) => { };


                pnl_Products.Controls.Add(productComponent);
            }
        }

        public void PushSelectedData(product product, Image image)
        {
            tbx_Name.Text = product.name;
            tbx_Price.Text = product.price.ToString();
            tbx_Description.Text = product.description;
            tbx_Love.Text = product.like_count.ToString();
            chk_Display.Checked = product.is_display;
            cbx_Category.SelectedValue = product.category_id;
            pbx_Image.Image = image;
            LoadIngredientsByProduct(bllIngredients.GetByProduct(product.id));
            LoadNutritionssByProduct(bllNutritions.GetByProduct(product.id));
        }

        #endregion

        private void InitializeBLLs()
        {
            bllProducts = new BLL_Products();
            bllCategories = new BLL_Categories();
            bllIngredients = new BLL_Ingredients();
            bllNutritions = new BLL_Nutritions();
        }

        #region Graphics

        public void InitializeGraphics()
        {
            DesignTbx_Search();
            DesignTbx_Love();

            tbx_Price.KeyPress += (s, e) =>
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) e.Handled = true;
            };
            tbx_Quantity.KeyPress += (s, e) =>
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) e.Handled = true;
            };
        }

        public void DesignTbx_Search()
        {
            tbx_Search.IconRight = null;
            tbx_Search.GotFocus += (s, e) =>
            {
                tbx_Search.IconLeft = Properties.Resources.search_tertiary;
                if(!string.IsNullOrEmpty(tbx_Search.Text)) tbx_Search.IconRight = Properties.Resources.xmark_tertiary;
            };
            tbx_Search.LostFocus += (s, e) =>
            {
                tbx_Search.IconLeft = Properties.Resources.search_primary;
                if (!string.IsNullOrEmpty(tbx_Search.Text)) tbx_Search.IconRight = Properties.Resources.xmark_primary;
            };
            tbx_Search.MouseHover += (s, e) =>
            {
                if (!tbx_Search.Focused)
                {
                    tbx_Search.IconLeft = Properties.Resources.search_tertiary;
                    if (!string.IsNullOrEmpty(tbx_Search.Text))  tbx_Search.IconRight = Properties.Resources.xmark_tertiary;
                }
            };
            tbx_Search.MouseLeave += (s, e) =>
            {
                if (!tbx_Search.Focused)
                {
                    tbx_Search.IconLeft = Properties.Resources.search_primary;
                    if (!string.IsNullOrEmpty(tbx_Search.Text)) tbx_Search.IconRight = Properties.Resources.xmark_primary;
                }
            };
            tbx_Search.TextChanged += (s, e) =>
            {
                if (string.IsNullOrEmpty(tbx_Search.Text)) tbx_Search.IconRight = null;
                else tbx_Search.IconRight = Properties.Resources.xmark_tertiary;
            };
            tbx_Search.IconRightClick += (s, e) =>
            {
                tbx_Search.Clear();
                tbx_Search.IconRight = null;
            };
        }

        public void DesignTbx_Love()
        {
            tbx_Love.GotFocus += (s, e) => tbx_Love.IconLeft = Properties.Resources.heart_petal;
            tbx_Love.LostFocus += (s, e) => tbx_Love.IconLeft = Properties.Resources.heart_primary;
            tbx_Love.MouseHover += (s, e) =>
            {
                if (!tbx_Love.Focused) tbx_Love.IconLeft = Properties.Resources.heart_petal;
            };
            tbx_Love.MouseLeave += (s, e) =>
            {
                if (!tbx_Love.Focused) tbx_Love.IconLeft = Properties.Resources.heart_primary;
            };
            //if (tbx_Love.ReadOnly)
            //{
            //    tbx_Love.BorderColor = Color.FromArgb(254, 205,194);
            //    tbx_Love.IconLeft = Properties.Resources.heart_petal;
            //}
        }

        public void DesignDgv_Ingredients()
        {
            foreach (DataGridViewColumn column in dgv_Ingredients.Columns)
            {
                if (column.Name != "dgv_Ingredients_Name") column.Visible = false;
            }
        }

        public void DesignDgv_Nutritions()
        {
            foreach (DataGridViewColumn column in dgv_Nutritions.Columns)
            {
                if (column.Name != "dgv_Nutritions_Name" && column.Name != "dgv_Nutritions_Quantity") column.Visible = false;
            }
        }

        #endregion
    }
}
