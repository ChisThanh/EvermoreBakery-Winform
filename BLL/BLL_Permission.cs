using DTO;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLL_Permission : BLL_Base<permission>
    {
        public BLL_Permission() : base()
        {
            _dal = new DAL_Permission();
        }

        public List<permission> GetPermissionOnUser()
        {
            return ((DAL_Permission)_dal).GetPermissionOnUser();
        }
    }

}
