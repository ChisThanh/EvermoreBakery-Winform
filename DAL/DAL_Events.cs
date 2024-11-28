using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAL
{
    public class DAL_Events : DAL_Base<_event>
    {
        public DAL_Events() { }

        public List<product> GetProductsByEvent(long id)
        {
            var productDtos = _context.event_products
                                      .Where(ep => ep.event_id == id)
                                      .Select(ep => new
                                      {
                                          Id = ep.product.id,
                                          Name = ep.product.name,
                                          Description = ep.product.description,
                                          Price = ep.product.price,
                                          CategoryId = ep.product.category_id,
                                          ProductReviews = ep.product.product_reviews,
                                          Percentage = ep.percentage ?? 0
                                      })
                                      .ToList();

            var products = productDtos.Select(dto => new product
            {
                id = dto.Id,
                name = dto.Name,
                description = dto.Description,
                price = dto.Price,
                category_id = dto.CategoryId,
                product_reviews = dto.ProductReviews,
                percentage = dto.Percentage
            }).ToList();

            return products;
        }

        public bool AddProductToEvent(long eventId, long productId, int percentage)
        {
            var eventProduct = new event_products
            {
                event_id = eventId,
                product_id = productId,
                percentage = percentage
            };

            _context.event_products.Add(eventProduct);
            return _context.SaveChanges() > 0;
        }

        public bool CreateOrUpdateProEv(long eventId, long productId, int percentage)
        {
            var eventProduct = _context.event_products.FirstOrDefault(ep => ep.event_id == eventId && ep.product_id == productId);
            if (eventProduct == null)
            {
                eventProduct = new event_products
                {
                    event_id = eventId,
                    product_id = productId,
                    percentage = percentage
                };
                _context.event_products.Add(eventProduct);
            }
            else
            {
                eventProduct.percentage = percentage;
            }
            return _context.SaveChanges() > 0;
        }

        public bool RemoveProductFromEvent(long eventId, List<long> productId)
        {
            var eventProducts = _context.event_products.Where(ep => ep.event_id == eventId && productId.Contains(ep.product_id));
            _context.event_products.RemoveRange(eventProducts);
            return _context.SaveChanges() > 0;
        }

        public bool CreateOrUpdateEv(_event _Event)
        {
            var ev = _context.events.Find(_Event.id);

            if (ev == null)
                _context.events.Add(_Event);
            else
            {
                ev.name = _Event.name;
                ev.description = _Event.description;
                ev.start_date = _Event.start_date;
                ev.end_date = _Event.end_date;
            }
            return _context.SaveChanges() > 0;
        }

        public bool DeleteEvent(long id)
        {
            var ev = _context.events.Find(id);
            if (ev == null)
                return false;

            var proEv = _context.event_products.Where(ep => ep.event_id == id).ToList();
            _context.event_products.RemoveRange(proEv);

            _context.SaveChanges();

            _context.events.Remove(ev);

            _context.SaveChanges();

            return true;
        }

        public bool CalculatePriceSave(long eventId)
        {
            var eventProducts = _context.event_products.Where(ep => ep.event_id == eventId).ToList();

            if (!eventProducts.Any())
                return false;

            var ev = _context.events.Find(eventId);

            var products = eventProducts.Select(ep => ep.product).ToList();

            foreach (var item in eventProducts)
            {
                var product = products.FirstOrDefault(p => p.id == item.product_id);
                var price_sale = product.price * item.percentage / 100;
                product.price_sale = (long)price_sale;
            }

            _context.SaveChanges();
            return true;
        }

    }
}
