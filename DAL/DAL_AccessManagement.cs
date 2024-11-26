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
    }
}
