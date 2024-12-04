using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAL;
using static System.Net.Mime.MediaTypeNames;

namespace BLL
{
    public class BLL_Products : BLL_Base<product>
    {
        protected DAL_Products _dalP;
        ApiClient apiClient = new ApiClient();

        public BLL_Products() :base()
        {
            _dalP = new DAL_Products();
            _dal = _dalP;
        }

        public async Task<product> AddAsync(product entity)
        {
            var data = await _dalP.AddAsync(entity);
            var text = entity.name + " " + entity.description;
            string response = await apiClient.GenerateKeywordsAsync(data.id, text);
            return data;
        }


        public override List<product> GetList()
        {
            return _dal.GetAll();
        }

        public override async Task<List<product>> GetListAsync()
        {
            return await _dal.GetAllAsync();
        }

        public image GetImageByProduct(long productId)
        {
            return _dalP.GetImageByProduct(productId);
        }
    }
}
