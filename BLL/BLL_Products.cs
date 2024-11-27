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

        public override List<product> GetList()
        {
            return _dal.GetAll();
        }

        public override async Task<List<product>> GetListAsync()
        {
            return await _dal.GetAllAsync();
        }

        public image GetImageByProduct(long productId)
        {
            return _dalP.GetImageByProduct(productId);
        }
    }
}
