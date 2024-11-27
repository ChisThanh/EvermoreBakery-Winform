using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DAL_Nutritions : DAL_Base<nutrition>
    {
        public DAL_Nutritions() { }

        public override List<nutrition> GetAll()
        {
            return _context.nutritions.ToList();
        }

        //public override async Task<List<nutrition>> GetAllAsync()
        //{
        //    return await _context.nutritions.ToListAsync();
        //}

        public List<object> GetByProduct(long productId)
        {
            return (from nt in _context.nutritions
                    join np in _context.nutrition_product on nt.id equals np.nutrition_id
                    join pd in _context.products on np.product_id equals pd.id
                    where pd.id == productId
                    select new
                    {
                        name = nt.name,
                        quantity = np.quantity
                    }).ToList<object>();
        }
    }
}
