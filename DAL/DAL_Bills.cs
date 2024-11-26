using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DAL_Bills : DAL_Base<bill>
    {
        public DAL_Bills() { }

        public override List<bill> GetAll()
        {
            var data = _context.bills.ToList();
            return data;
        }

        public bool UpdateStatus(long id, bill _bill)
        {
            var bill = _context.bills.Find(id);
            if (bill == null)
                return false;
            bill.status = _bill.status;

            if(_bill.status == 3)
                bill.delivery_date = _bill.delivery_date;

            if (_bill.status == 4)
                bill.payment_status = 1;

            _context.SaveChanges();
            return true;
        }
    }
}
