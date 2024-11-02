using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using BCrypt.Net;

namespace BLL
{
    public class BLL_Auth
    {
        private static DAL_User dalUsers = new DAL_User();
        public BLL_Auth() { }

        public static user Login(string account, string password)
        {
            var user = dalUsers._dto.FirstOrDefault(x => x.email == account);
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

        public static async Task<user> LoginAsync(string account, string password)
        {
            var user = await Task.Run(() => dalUsers._dto.FirstOrDefault(x => x.email == account));

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
    }
}
