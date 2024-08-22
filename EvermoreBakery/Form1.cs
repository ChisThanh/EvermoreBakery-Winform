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
                string str = "";
                foreach (var item in users)
                {
                    str += item.Name + "\n";
                }
                MessageBox.Show(str);
            }
        }
    }
}
