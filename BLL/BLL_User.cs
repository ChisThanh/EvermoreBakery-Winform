using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;

namespace BLL
{
    public class BLL_User : BLL_Base<user>
    {
        private readonly DAL_User _dalUser;

        public BLL_User() : base()
        {
            _dalUser = new DAL_User();
            _dal = _dalUser;
        }

        public bool Add(user user, long roleId)
        {
            user.password = BCrypt.Net.BCrypt.HashPassword(user.password);
            return _dalUser.Add(user, roleId);
        }
    }
}
