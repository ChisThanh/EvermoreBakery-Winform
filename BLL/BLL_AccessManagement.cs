using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLL_AccessManagement
    {
        DAL_AccessManagement dalAccessManagement = new DAL_AccessManagement();
        public BLL_AccessManagement() { }

        public List<permission> GetPerList()
        {
            return dalAccessManagement.GetPerList();
        }

        public List<role> GetRoleList()
        {
            return dalAccessManagement.GetRoleList();
        }

        public List<permission> GetPermissionsByRoleId(long roleId)
        {
            return dalAccessManagement.GetPermissionsByRoleId(roleId);
        }
    }
}
