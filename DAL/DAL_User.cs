using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DAL_User : DAL_Base<user>
    {
        public void AddRoleToUser(long userId, long roleId)
        {
            var userRole = new role_user
            {
                user_id = userId,
                role_id = roleId,
                user_type = "App\\Models\\User"
            };

            _context.role_user.Add(userRole);
            _context.SaveChanges();
        }

        public void RemoveRoleFromUser(long userId, long roleId)
        {

            var userRole = _context.role_user.FirstOrDefault(ur => ur.user_id == userId && ur.role_id == roleId);
            if (userRole != null)
            {
                _context.role_user.Remove(userRole);
                _context.SaveChanges();
            }
        }

        public void UpdateUserRole(long userId, long oldRoleId, long newRoleId)
        {
            var userRole = _context.role_user.FirstOrDefault(ur => ur.user_id == userId && ur.role_id == oldRoleId);
            if (userRole != null)
            {
                userRole.role_id = newRoleId;
                _context.SaveChanges();
            }
        }

        public void AddPermissionToUser(long userId, long permissionId, string userType)
        {
            var userPermission = new permission_user
            {
                user_id = userId,
                permission_id = permissionId,
                user_type = "App\\Models\\User"
            };
            _context.permission_user.Add(userPermission);
            _context.SaveChanges();
        }

        public void RemovePermissionFromUser(long userId, long permissionId)
        {
            var userPermission = _context.permission_user.FirstOrDefault(up => up.user_id == userId && up.permission_id == permissionId);
            if (userPermission != null)
            {
                _context.permission_user.Remove(userPermission);
                _context.SaveChanges();
            }
        }
    }
}
