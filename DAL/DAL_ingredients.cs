using DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DAL_Ingredients: DAL_Base<ingredient>
    {
        public DAL_Ingredients() { }
        public override List<ingredient> GetAll()
        {
            return _context.ingredients.ToList();
        }

        public override async Task<List<ingredient>> GetAllAsync()
        {
            return await _context.ingredients.ToListAsync();
        }

        public List<object> GetByProduct(long productId)
        {
            return (from ig in _context.ingredients
                    join ip in _context.ingredient_product on ig.id equals ip.ingredient_id
                    join pd in _context.products on ip.product_id equals pd.id
                    where pd.id == productId
                    select new
                    {
                        name = ig.name
                    }).ToList<object>();
        }
    }
}
