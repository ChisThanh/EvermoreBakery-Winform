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

        public bool AddToProduct(long productId, long nutritionId, byte quantity)
        {
            var check = _context.nutrition_product.Where(np => np.product_id == productId && np.nutrition_id == nutritionId).FirstOrDefault();
            if (check != null)
                return false;

            var npp = new nutrition_product
            {
                product_id = productId,
                nutrition_id = nutritionId,
                quantity = quantity
            };
            _context.nutrition_product.Add(npp);
            _context.SaveChanges();
            return true;
        }

        public bool DelToProduct(long productId, string nutritionName)
        {
            var nutrition = _context.nutritions.Where(nt => nt.name.Trim() == nutritionName.Trim()).FirstOrDefault();
            if (nutrition == null)
                return false;

            var nutritionProduct = _context.nutrition_product.Where(np => np.product_id == productId && np.nutrition_id == nutrition.id).FirstOrDefault();
            if (nutritionProduct == null)
                return false;

            _context.nutrition_product.Remove(nutritionProduct);
            _context.SaveChanges();
            return true;
        }
    }
}
