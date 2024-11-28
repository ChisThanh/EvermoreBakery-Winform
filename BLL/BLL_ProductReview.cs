using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLL_ProductReview : BLL_Base<product_reviews>
    {
        private readonly DAL_ProductReview _dalProductReview;

        public BLL_ProductReview() : base()
        {
            _dalProductReview = new DAL_ProductReview();
            _dal = _dalProductReview; 
        }

        public bool updateIsShow(long id, bool isShow)
        {
            return _dalProductReview.updateIsShow(id, isShow);
        }
    }
}
