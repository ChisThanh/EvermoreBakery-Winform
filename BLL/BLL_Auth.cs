using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using System.Diagnostics.SymbolStore;
using BCrypt.Net;


namespace BLL
{
    public class BLL_Auth 
    {
        private static BLL_Auth _instance;
        private static readonly object _lock = new object();

        DAL_User users = new DAL_User();

        public static BLL_Auth Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                        _instance = new BLL_Auth();
                    return _instance;
                }
            }
        }

        public static user Login(string account, string password)
        {
            var user = DAL_User.Instance._dto.FirstOrDefault(x => x.email == account);
            if (user == null)
                throw new Exception("Tài khoản không tồn tại");

            string hashedPassword = user.password;

            bool isPasswordCorrect = BCrypt.Net.BCrypt.Verify(password, hashedPassword);

            if (!isPasswordCorrect)
                throw new Exception("Mật khẩu không đúng");

            if(!user.HasRoles("admin") || !user.HasRoles("sadmin"))
                throw new Exception("Tài khoản không có quyền truy cập");

            return user;
        }

        public static async Task<user> LoginAsync(string account, string password)
        {
            var user = await Task.Run(() => DAL_User.Instance._dto.FirstOrDefault(x => x.email == account));

            if (user == null)
                throw new Exception("Tài khoản không tồn tại");

            string hashedPassword = user.password;

            bool isPasswordCorrect = BCrypt.Net.BCrypt.Verify(password, hashedPassword);

            if (!isPasswordCorrect)
                throw new Exception("Mật khẩu không đúng");

            if (!user.HasRoles("admin") && !user.HasRoles("sadmin")) 
                throw new Exception("Tài khoản không có quyền truy cập");

            return user;
        }


        public BLL_Auth() { }
    }
}
