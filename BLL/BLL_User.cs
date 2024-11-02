using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLL_User : BLL_Base<user>
    {
        private readonly DAL_User _dalUser;

        public BLL_User() : base()
        {
            _dal = new DAL_User();
        }
    }
}
