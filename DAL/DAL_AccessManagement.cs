using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DAL_AccessManagement
    {
        protected readonly EvermoreBakeryContext _context;

        public DAL_AccessManagement()
        {
            _context = EvermoreBakeryContext.Instance;
        }

        public List<permission> GetPerList()
        {
            return _context.permissions.ToList();
        }
        

        public List<role> GetRoleList() {
            return _context.roles.ToList();
        }

        public List<permission> GetPermissionsByRoleId(long roleId)
        {
            return _context.permissions.Where(p => p.roles.Any(r => r.id == roleId)).ToList();
        }

        public bool AddPermissionRole(long roleId, long permissionId)
        {
            try
            {
                var role = _context.roles.Find(roleId);
                var permission = _context.permissions.Find(permissionId);
                role.permissions.Add(permission);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeletePermissionRole(long roleId, long permissionId)
        {
            try
            {
                var role = _context.roles.Find(roleId);
                var permission = _context.permissions.Find(permissionId);
                role.permissions.Remove(permission);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool AddPermissionUser(long userId, long permissionId)
        {
            try
            {
                var user = _context.users.Find(userId);
                var permission = _context.permissions.Find(permissionId);
                user.permission_user.Add(new permission_user { permission = permission, user_type = "App\\Models\\User" });
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeletePermissionUser(long userId, long permissionId)
        {
            try
            {
                var user = _context.users.Find(userId);
                var permission = _context.permissions.Find(permissionId);
                var permissionUser = user.permission_user.FirstOrDefault(pu => pu.permission_id == permissionId);
                user.permission_user.Remove(permissionUser);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool AddRoleUser(long userId, long roleId)
        {
            try
            {
                var user = _context.users.Find(userId);
                var role = _context.roles.Find(roleId);
                user.role_user.Add(new role_user { role = role, user_type = "App\\Models\\User" });
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool ChangeRoleUser(long userId, string roleName)
        {
            try
            {
                var user = _context.users.Find(userId);
                var role = _context.roles.Where(r => r.name == roleName).FirstOrDefault();
                var roleUser = user.role_user.FirstOrDefault(ru => ru.role_id == role.id);
                if (roleUser != null)
                    user.role_user.Remove(roleUser);

                user.role_user.Add(new role_user { role = role, user_type = "App\\Models\\User" });

                _context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
