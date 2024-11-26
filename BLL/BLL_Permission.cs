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
        protected DAL_Permission _dalP;
        public BLL_Permission() : base()
        {
            _dalP = new DAL_Permission();
            _dal = _dalP;
        }
    }

}
