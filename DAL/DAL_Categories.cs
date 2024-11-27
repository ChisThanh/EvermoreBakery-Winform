using DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DAL_Categories: DAL_Base<category>
    {
        public DAL_Categories() { }

        public override List<category> GetAll()
        {
            return _context.categories.ToList();
        }

        public override async Task<List<category>> GetAllAsync()
        {
            return await _context.categories.ToListAsync();
        }
    }
}
