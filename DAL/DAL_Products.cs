using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAL
{
    public class DAL_Products : DAL_Base<product>
    {
        public DAL_Products() {}

        public override List<product> GetAll()
        {
            return _context.products.ToList();
        }

        public override async Task<List<product>> GetAllAsync()
        {
            return await _context.products.ToListAsync();
        }

        public image GetImageByProduct(long productId)
        {
            return _context.images.Where(im => im.imageable_id == productId && im.imageable_type == "App\\Models\\Product").FirstOrDefault();
        }
    }
}
