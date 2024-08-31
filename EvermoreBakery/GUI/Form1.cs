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
            //MessageBox.Show(userRepo.GetByKeyValue("Name", "admin").Name);
            //MessageBox.Show(userRepo.ExistById(1).ToString());
            //MessageBox.Show(userRepo.ExistByKeyValue("Id", 1).ToString());
            //var users = userRepo.GetAll("Email", "@ex");
            //var users = userRepo.repo.ToList();
            //var users = userRepo.GetListFromSqlRaw("select * from users where id = @p0", 1);
            //foreach (var row in users)
            //{
            //    var values = (object[])row;
            //    var id = values[0];
            //    var name = values[1];
            //}
            //MessageBox.Show(userRepo.DeleteIds(5,6,7).ToString());
            //MessageBox.Show(userRepo.DeleteByKeyValue("Id", 9).ToString());

            var users = userRepo.GetAll();
            dataGridView1.DataSource = users;
        }
    }
}
