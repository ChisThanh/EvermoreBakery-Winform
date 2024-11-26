using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAL;

namespace BLL
{
    public class BLL_Products : BLL_Base<product>
    {
        protected DAL_Products _dalP; 
        public BLL_Products() :base()
        {
            _dalP = new DAL_Products();
            _dal = _dalP;
        }
    }
}
