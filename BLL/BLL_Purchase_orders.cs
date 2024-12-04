using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLL_Purchase_orders : BLL_Base<purchase_orders>
    {
        protected DAL_Purchase_orders _dalP;
        public BLL_Purchase_orders() : base()
        {
            _dalP = new DAL_Purchase_orders();
            _dal = _dalP;
        }

        public override List<purchase_orders> GetList()
        {
            return _dalP.GetAll();
        }
        //public override List<purchase_orders> GetById(long id)
        //{
        //    return _dalP.GetById(id);
        //}
        public List<purchase_order_details> GetPurchaseOrderDetailsByOrderId(long id)
        {
            return _dalP.GetPurchaseOrderDetailsByOrderId(id);
        }
        public bool AddIngredientToPurchaseOrder(long purchaseOrderId, long ingredientId, int quantity, double price)
        {
            return _dalP.AddIngredientToPurchaseOrder(purchaseOrderId, ingredientId, quantity, price);
        }
        public bool CreateOrUpdatePurchaseOrderDetail(long purchaseOrderId, long ingredientId, int quantity, double price)
        {
            return _dalP.CreateOrUpdatePurchaseOrderDetail(purchaseOrderId,ingredientId, quantity, price);
        }
        public bool RemoveIngredientsFromPurchaseOrder(long purchaseOrderId, List<long> ingredientIds)
        {
            return _dalP.RemoveIngredientsFromPurchaseOrder(purchaseOrderId, ingredientIds);
        }
        public bool CreateOrUpdatePurchaseOrder(purchase_orders purchaseOrder)
        {
            return _dalP.CreateOrUpdatePurchaseOrder(purchaseOrder);
        }
        public bool DeletePurchaseOrder(long purchaseOrderId)
        {
            return _dalP.DeletePurchaseOrder(purchaseOrderId);
        }
        public bool CalculateTotalForPurchaseOrder(long purchaseOrderId)
        {
            return _dalP.CalculateTotalForPurchaseOrder(purchaseOrderId);
        }
        public List<ingredient> GetIngredients()
        {
            return _dalP.GetIngredients();
        }
        public List<ingredient> SearchIngredients(string keyword)
        {
            return _dalP.SearchIngredients(keyword);
        }
        public bool UpdateStockQuantity(long ingredientId, int additionalQuantity)
        {
            return _dalP.UpdateStockQuantity(ingredientId, additionalQuantity);
        }

    }
}
