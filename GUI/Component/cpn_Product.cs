using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DTO;
using Guna.UI2.WinForms;

namespace GUI.Component
{
    public partial class cpn_Product : UserControl
    {
        public product product {  get; set; }
        public image image;
        public Image trueImage;
        public event EventHandler clicked;
        public event EventHandler hoverIn;
        public event EventHandler hoverOut;
        public bool selected;

        public cpn_Product(product product, image image)
        {
            InitializeComponent();
            this.product = product;
            this.image = image;
            selected = false;
            Load += Cpn_Product_Load;
        }

        private async void Cpn_Product_Load(object sender, EventArgs e)
        {
            lbl_Product.Text = product.name;
            try
            {
                //string imageUrl = "http://web.chithanh.id.vn/storage/" + image.url;
                //await GetImageFromUrlAsync(imageUrl);

                if (trueImage != null) pbx_Image.Image = trueImage;
            }
            catch
            {
                trueImage = null;
            }

            pnl_Click.Click += (s, ec) => clicked?.Invoke(this, EventArgs.Empty);
            pnl_Click.MouseHover += (s, ec) =>
            {
                hoverIn?.Invoke(this, EventArgs.Empty);
                if (!selected) ChangeHoverColor();
            };
            pnl_Click.MouseLeave += (s, ec) =>
            {
                hoverOut?.Invoke(this, EventArgs.Empty);
                if (!selected)  ChangeDefaultColor();
            };
        }

        public async Task GetImageFromUrlAsync(string imageUrl)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    byte[] imageBytes = await client.GetByteArrayAsync(imageUrl);

                    using (var ms = new MemoryStream(imageBytes))
                    {
                        trueImage = Image.FromStream(ms);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error loading image: " + ex.Message);
                    trueImage = null;
                }
            }
        }

        public void ChangeSelectColor()
        {
            pnl_Background.FillColor = Color.FromArgb(139, 94, 34);
            pnl_Background.FillColor2 = Color.FromArgb(221, 165, 74);
            lbl_Product.ForeColor = Color.White;
        }

        public void ChangeHoverColor()
        {
            pnl_Background.FillColor = Color.Gray;
            pnl_Background.FillColor2 = Color.LightGray;
            lbl_Product.ForeColor = Color.FromArgb(69, 28, 13);
        }

        public void ChangeDefaultColor()
        {
            pnl_Background.FillColor = Color.FromArgb(216, 184, 148);
            pnl_Background.FillColor2 = Color.FromArgb(255, 249, 237);
            lbl_Product.ForeColor = Color.FromArgb(69, 28, 13);
        }
    }
}
