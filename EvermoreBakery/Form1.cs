using EvermoreBakery.Service;
using System.Data;

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
                var user = users[0].HasPermissions("all-create");
                var getPermissions = users[1].GetPermissions();
                dataGridView1.DataSource = users;
            }
        }
    }
}
