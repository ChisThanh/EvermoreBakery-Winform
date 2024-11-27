using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLL_Categories : BLL_Base<category>
    {
        protected DAL_Categories _dalC;
        public BLL_Categories() : base()
        {
            _dalC = new DAL_Categories();
            _dal = _dalC;
        }

        public override List<category> GetList()
        {
            return _dal.GetAll();
        }

        public override async Task<List<category>> GetListAsync()
        {
            return await _dal.GetAllAsync();
        }
    }
}
