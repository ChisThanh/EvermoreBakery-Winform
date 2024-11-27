using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DAL_Purchase_orders : DAL_Base<purchase_orders>
    {
        public DAL_Purchase_orders() { }
        public override List<purchase_orders> GetAll()
        {
            return _context.purchase_orders.ToList();
        }
    }
}
