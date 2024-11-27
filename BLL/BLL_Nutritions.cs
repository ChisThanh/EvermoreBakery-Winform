using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLL_Nutritions : BLL_Base<nutrition>
    {
        protected DAL_Nutritions _dalN;
        public BLL_Nutritions() : base()
        {
            _dalN = new DAL_Nutritions();
            _dal = _dalN;
        }

        public override List<nutrition> GetList()
        {
            return _dal.GetAll();
        }

        //public override async Task<List<nutrition>> GetListAsync()
        //{
        //    return await _dal.GetAllAsync();
        //}

        public List<object> GetByProduct(long productId)
        {
            return _dalN.GetByProduct(productId);
        }
    }
}
