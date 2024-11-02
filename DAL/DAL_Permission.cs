using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DAL_Permission : DAL_Base<permission>
    {
        public List<permission> GetPermissionOnUser()
        {
           return _dto.ToList();
        }

    }
}
