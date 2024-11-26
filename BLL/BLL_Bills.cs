using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLL_Bills : BLL_Base<bill>
    {
        protected DAL_Bills _dalP;
        public BLL_Bills() : base()
        {
            _dalP = new DAL_Bills();
            _dal = _dalP;
        }

        public bool UpdateStatus(long id, bill _bill)
        {
            return _dalP.UpdateStatus(id, _bill);
        }
    }
}
