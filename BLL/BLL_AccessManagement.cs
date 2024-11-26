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

        public bool AddPermissionRole(long roleId, long permissionId)
        {
            return dalAccessManagement.AddPermissionRole(roleId, permissionId);
        }

        public bool DeletePermissionRole(long roleId, long permissionId)
        {
            return dalAccessManagement.DeletePermissionRole(roleId, permissionId);
        }

        public bool ChangeRoleUser(long userId, string roleName)
        {
            return dalAccessManagement.ChangeRoleUser(userId, roleName);
        }
    }
}
