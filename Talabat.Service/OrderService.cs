using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregate;
using Talabat.Core.Repositories;
using Talabat.Core.Services;

namespace Talabat.Service
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IGenericRepository<DeliveryMethod> _deliveryRepository;
        private readonly IGenericRepository<Order> _orderRepository;
        private readonly IGenericRepository<Product> _productRepository;

        public OrderService(IBasketRepository basketRepository,
            IGenericRepository<Product> productRepository,
            IGenericRepository<DeliveryMethod> deliveryRepository,
            IGenericRepository<Order> orderRepository) {
            this._basketRepository = basketRepository;
            this._deliveryRepository = deliveryRepository;
            this._orderRepository = orderRepository;
            this._productRepository = productRepository;
        }
        public async Task<Order> CreateOrderAsync(string BuyerEmail, string BasketId, int DeliveryMethodId, Address ShippingAddress)
        {
            #region Steps for our business
            //1. Get Basket from basket repo
            var Basket = await _basketRepository.GetBasketAsync(BasketId);
            //2. Get Selected Items at basket from product repo
            var OrderItems = new List<OrderItem>();
            if (Basket?.Items.Count > 0) {
                foreach (var item in Basket.Items) {

                    var Product = await _productRepository.GetByIdAsync(item.Id);
                    var OrderItem = new OrderItem(Product.Id,Product.Name,Product.PictureURL,item.Quantity,Product.Price);
                    OrderItems.Add(OrderItem);
                }
            }
            //3. Calculate SubTotal
            var SubTotal = OrderItems.Sum(Item=>Item.Price*Item.Quantity);
            //4. Get Delivery Method From DeliveryMethod Repo
            var DeliceryMethod = await _deliveryRepository.GetByIdAsync(DeliveryMethodId);
            //5. Create Order
            var Order = new Order(BuyerEmail,ShippingAddress,DeliceryMethod,OrderItems,SubTotal);
            //6. Add Order Locally
            await _orderRepository.AddAsync(Order);
            //7. Save Order To Database

            #endregion
        }

        public Task<Order> GetOrderByIdForSpecificUserAsync(string BuyerEmail, int OrderId)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Order>> GetOrdersForSpecificUserAsync(string BuyerEmail)
        {
            throw new NotImplementedException();
        }
    }
}
