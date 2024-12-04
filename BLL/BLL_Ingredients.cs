using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLL_Ingredients : BLL_Base<ingredient>
    {
        protected DAL_Ingredients _dalI;
        public BLL_Ingredients() : base()
        {
            _dalI = new DAL_Ingredients();
            _dal = _dalI;
        }

        public override List<ingredient> GetList()
        {
            return _dal.GetAll();
        }

        public override async Task<List<ingredient>> GetListAsync()
        {
            return await _dal.GetAllAsync();
        }

        public List<object> GetByProduct(long productId)
        {
            return _dalI.GetByProduct(productId);
        }

        public bool AddToProduct(long productId, long ingredientId)
        {
            return _dalI.AddToProduct(productId, ingredientId);
        }

        public bool DelToProduct(long productId, string ingredientName)
        {
            return _dalI.DelToProduct(productId, ingredientName);
        }
    }
}
