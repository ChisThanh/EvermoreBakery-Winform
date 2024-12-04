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
            return _context.purchase_orders.OrderByDescending(po => po.order_date).ToList();
        }

        //public override List<purchase_orders> GetById(long id)
        //{
        //    var purchaseOrder = base.GetById(id);
        //    if (purchaseOrder == null)
        //    {
        //        throw new KeyNotFoundException($"Purchase order with ID {id} not found.");
        //    }
        //    return purchaseOrder;
        //}
        public List<purchase_order_details> GetPurchaseOrderDetailsByOrderId(long orderId)
        {
            // Truy vấn các chi tiết nhập kho dựa vào ID của đơn nhập hàng
            var detailsDtos = _context.purchase_order_details
                                      .Where(pod => pod.purchase_order_id == orderId)
                                      .Select(pod => new
                                      {
                                          OrderId = pod.purchase_order_id,
                                          IngredientId = pod.ingredient_id,
                                          Quantity = pod.quantity,
                                          Price = pod.price,
                                          IngredientName = pod.ingredient.name,
                                          IngredientUnit = pod.ingredient.unit,
                                          IngredientDescription = pod.ingredient.description
                                      })
                                      .ToList();

            // Chuyển đổi kết quả truy vấn thành danh sách `purchase_order_details`
            var details = detailsDtos.Select(dto => new purchase_order_details
            {
                purchase_order_id = dto.OrderId,
                ingredient_id = dto.IngredientId,
                quantity = dto.Quantity,
                price = dto.Price,
                ingredient = new ingredient
                {
                    id = dto.IngredientId,
                    name = dto.IngredientName,
                    unit = dto.IngredientUnit,
                    description = dto.IngredientDescription
                }
            }).ToList();

            return details;
        }
        //Thêm chi tiết nhập kho
        public bool AddIngredientToPurchaseOrder(long purchaseOrderId, long ingredientId, int quantity, double price)
        {
            var orderDetail = new purchase_order_details
            {
                purchase_order_id = purchaseOrderId,
                ingredient_id = ingredientId,
                quantity = quantity,
                price = price
            };

            _context.purchase_order_details.Add(orderDetail);
            return _context.SaveChanges() > 0;
        }
        //Tạo hoặc cập nhật chi tiết nhập kho 
        public bool CreateOrUpdatePurchaseOrderDetail(long purchaseOrderId, long ingredientId, int quantity, double price)
        {
            var orderDetail = _context.purchase_order_details
                                      .FirstOrDefault(pod => pod.purchase_order_id == purchaseOrderId && pod.ingredient_id == ingredientId);

            if (orderDetail == null)
            {
                orderDetail = new purchase_order_details
                {
                    purchase_order_id = purchaseOrderId,
                    ingredient_id = ingredientId,
                    quantity = quantity,
                    price = price
                };
                _context.purchase_order_details.Add(orderDetail);
            }
            else
            {
                orderDetail.quantity = quantity;
                orderDetail.price = price;
            }

            return _context.SaveChanges() > 0;
        }
        // Xóa chi tiết nhập kho
        public bool RemoveIngredientsFromPurchaseOrder(long purchaseOrderId, List<long> ingredientIds)
        {
            var orderDetails = _context.purchase_order_details
                                       .Where(pod => pod.purchase_order_id == purchaseOrderId && ingredientIds.Contains(pod.ingredient_id))
                                       .ToList();

            _context.purchase_order_details.RemoveRange(orderDetails);
            return _context.SaveChanges() > 0;
        }
        // Tạo hoặc cập nhật đơn nhập kho 
        public bool CreateOrUpdatePurchaseOrder(purchase_orders purchaseOrder)
        {
            var existingOrder = _context.purchase_orders.Find(purchaseOrder.id);

            if (existingOrder == null)
            {
                _context.purchase_orders.Add(purchaseOrder);
            }
            else
            {
                existingOrder.supplier_id = purchaseOrder.supplier_id;
                //existingOrder.order_date = purchaseOrder.order_date;
                existingOrder.delivery_date = purchaseOrder.delivery_date;
                existingOrder.status = purchaseOrder.status;
                //existingOrder.total = purchaseOrder.total;
                existingOrder.is_pay = purchaseOrder.is_pay;
                existingOrder.updated_at = DateTime.Now;
            }

            return _context.SaveChanges() > 0;
        }
        //Xóa đơn nhập kho và các chi tiết liên quan
        public bool DeletePurchaseOrder(long purchaseOrderId)
        {
            var purchaseOrder = _context.purchase_orders.Find(purchaseOrderId);
            if (purchaseOrder == null)
                return false;

            var orderDetails = _context.purchase_order_details
                                       .Where(pod => pod.purchase_order_id == purchaseOrderId)
                                       .ToList();

            _context.purchase_order_details.RemoveRange(orderDetails);
            _context.SaveChanges();

            _context.purchase_orders.Remove(purchaseOrder);
            return _context.SaveChanges() > 0;
        }

        //public double CalculateTotalForPurchaseOrder(long purchaseOrderId)
        //{
        //    var total = _context.purchase_order_details
        //                        .Where(pod => pod.purchase_order_id == purchaseOrderId)
        //                        .Sum(pod => pod.quantity * pod.price);

        //    var purchaseOrder = _context.purchase_orders.Find(purchaseOrderId);
        //    if (purchaseOrder != null)
        //    {
        //        purchaseOrder.total = total;
        //        _context.SaveChanges();
        //    }

        //    return total;
        //}
        public bool CalculateTotalForPurchaseOrder(long purchaseOrderId)
        {
            // Lấy chi tiết đơn hàng
            var purchaseOrderDetails = _context.purchase_order_details
                                               .Where(pod => pod.purchase_order_id == purchaseOrderId)
                                               .ToList();

            // Kiểm tra nếu không có chi tiết nào
            if (!purchaseOrderDetails.Any())
                return false;

            // Lấy thông tin đơn hàng
            var purchaseOrder = _context.purchase_orders.Find(purchaseOrderId);
            if (purchaseOrder == null)
                return false;

            // Tính tổng giá trị từ chi tiết đơn hàng
            double total = 0;
            foreach (var detail in purchaseOrderDetails)
            {
                total += detail.quantity * detail.price;
            }

            // Cập nhật tổng giá trị vào đơn hàng
            purchaseOrder.total = total;

            // Lưu thay đổi
            _context.SaveChanges();
            return true;
        }

        public bool UpdateStockQuantity(long ingredientId, int additionalQuantity)
        {
            var ingredient = _context.ingredients.Find(ingredientId);
            if (ingredient == null)
                return false;

            ingredient.stock_quantity += additionalQuantity;
            ingredient.updated_at = DateTime.Now;

            return _context.SaveChanges() > 0;
        }
        //Kiểm tra tồn kho tối thiểu
        public List<ingredient> GetLowStockIngredients(int threshold)
        {
            return _context.ingredients
                           .Where(i => i.stock_quantity < threshold && i.deleted_at == null)
                           .ToList();
        }
        //Tìm kiếm nguyên liệu
        public List<ingredient> SearchIngredients(string keyword)
        {
            return _context.ingredients
                           .Where(i => i.name.Contains(keyword) && i.deleted_at == null)
                           .ToList();
        }
        //Lấy danh sách nguyên liệu
        public List<ingredient> GetIngredients()
        {
            return _context.ingredients
                           .Where(i => i.deleted_at == null)
                           .ToList();
        }

        // Thêm mới nguyên liệu
        public bool AddIngredient(string name, string unit, string description, int stockQuantity)
        {
            var ingredient = new ingredient
            {
                name = name,
                unit = unit,
                description = description,
                stock_quantity = stockQuantity,
                created_at = DateTime.Now
            };

            _context.ingredients.Add(ingredient);
            return _context.SaveChanges() > 0;
        }


    }
}
