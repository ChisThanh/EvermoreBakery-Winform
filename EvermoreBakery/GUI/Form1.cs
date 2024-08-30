using EvermoreBakery.Service;
using System.Data;

namespace EvermoreBakery
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            UserRepository userRepo = new UserRepository();

            //MessageBox.Show(userRepo.GetById(1).Name);
            //MessageBox.Show(userRepo.Add(new Users { Name = "Test1", Email = "ahah1", Password = "123" }).Name);
            //MessageBox.Show(userRepo.Update(17,
            //new Users { Name = "Test update1", Email = "ahah1231", Password = "123" }).Name);
            //MessageBox.Show(userRepo.Delete(14).ToString());
            //MessageBox.Show(userRepo.SoftDelete(12).ToString());

            var users = userRepo.GetAll();
            dataGridView1.DataSource = users;
        }
    }
}
