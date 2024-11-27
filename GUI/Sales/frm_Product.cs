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
            DesignDgv_Ingredients();
        }

        private void LoadNutritionssByProduct(List<object> nutritions)
        {
            dgv_Nutritions.DataSource = nutritions;
            DesignDgv_Nutritions();
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
