using EvermoreBakery.Service;

namespace EvermoreBakery
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            using (var context = new ApplicationDbContext())
            {
                var users = context.Users.ToList();
                dataGridView1.DataSource = users;

                //string str = "";
                //foreach (var item in users)
                //{
                //    str += item.Name + "\n";
                //}
                //MessageBox.Show(str);
            }
        }
    }
}
