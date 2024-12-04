using DTO;
using DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DAL_Ingredients : DAL_Base<ingredient>
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

        public bool AddToProduct(long productId, long ingredientId)
        {
            var check = _context.ingredient_product.Where(ip => ip.product_id == productId && ip.ingredient_id == ingredientId).FirstOrDefault();
            if (check != null)
                return false;

            var ipp = new ingredient_product
            {
                product_id = productId,
                ingredient_id = ingredientId
            };
            _context.ingredient_product.Add(ipp);
            _context.SaveChanges();
            return true;
        }

        public bool DelToProduct(long productId, string ingredientName)
        {
            var ingredient = _context.ingredients.Where(ig => ig.name == ingredientName).FirstOrDefault();
            if (ingredient == null)
                return false;

            var ipp = _context.ingredient_product.Where(ip => ip.product_id == productId && ip.ingredient_id == ingredient.id).FirstOrDefault();
            if (ipp == null)
                return false;

            _context.ingredient_product.Remove(ipp);
            _context.SaveChanges();
            return true;
        }
    }
}
