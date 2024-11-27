using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLL_Coupons : BLL_Base<coupon>
    {
        protected DAL_Coupons _dalP;
        public BLL_Coupons() : base()
        {
            _dalP = new DAL_Coupons();
            _dal = _dalP;

        }
    }
}
