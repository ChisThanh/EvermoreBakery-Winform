using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DAL_ProductReview : DAL_Base<product_reviews>
    {
        public DAL_ProductReview() { }

        public bool updateIsShow(long id, bool isShow)
        {
            var productReview = _context.product_reviews.Find(id);
            if (productReview == null)
                return false;
            productReview.is_show = isShow;
            _context.SaveChanges();
            return true;
        }
    }


}
