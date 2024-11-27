using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLL_Events : BLL_Base<_event>
    {
        protected DAL_Events _dalE;
        public BLL_Events() : base()
        {
            _dalE = new DAL_Events();
            _dal = _dalE;
        }

        public List<product> GetProductsByEvent(long id)
        {
            return _dalE.GetProductsByEvent(id);
        }

        public bool AddProductToEvent(long eventId, long productId, int percentage)
        {
            return _dalE.AddProductToEvent(eventId, productId, percentage);
        }

        public bool CreateOrUpdateProEv(long eventId, long productId, int percentage)
        {
            return _dalE.CreateOrUpdateProEv(eventId, productId, percentage);
        }

        public bool RemoveProductFromEvent(long eventId, List<long> productId)
        {
            return _dalE.RemoveProductFromEvent(eventId, productId);
        }

        public bool CreateOrUpdateEv(_event _Event)
        {
            return _dalE.CreateOrUpdateEv(_Event);
        }

        public bool DeleteEvent(long id)
        {
            return _dalE.DeleteEvent(id);
        }

        public bool CalculatePriceSave(long id)
        {
            return _dalE.CalculatePriceSave(id);
        }
    }
}
