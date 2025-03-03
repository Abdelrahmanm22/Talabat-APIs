using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregate;
using Talabat.Core.Repositories;
using Talabat.Core.Services;
using Talabat.Core.Specifications.Order_Spec;

namespace Talabat.Service
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IBasketRepository basketRepository,
            IUnitOfWork unitOfWork) {
            this._basketRepository = basketRepository;
            this._unitOfWork = unitOfWork;
        }
        public async Task<Order?> CreateOrderAsync(string BuyerEmail, string BasketId, int DeliveryMethodId, Address ShippingAddress)
        {
            #region Steps for our business
            //1. Get Basket from basket repo
            var Basket = await _basketRepository.GetBasketAsync(BasketId);
            //2. Get Selected Items at basket from product repo
            var OrderItems = new List<OrderItem>();
            if (Basket?.Items.Count > 0) {
                foreach (var item in Basket.Items) {

                    var Product = await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
                    var OrderItem = new OrderItem(Product.Id,Product.Name,Product.PictureURL,item.Quantity,Product.Price);
                    OrderItems.Add(OrderItem);
                }
            }
            //3. Calculate SubTotal
            var SubTotal = OrderItems.Sum(Item=>Item.Price*Item.Quantity);
            //4. Get Delivery Method From DeliveryMethod Repo
            var DeliceryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(DeliveryMethodId);
            //5. Create Order
            var Order = new Order(BuyerEmail,ShippingAddress,DeliceryMethod,OrderItems,SubTotal);
            //6. Add Order Locally
            await _unitOfWork.Repository<Order>().AddAsync(Order);
            //7. Save Order To Database
            var result = await _unitOfWork.CompleteAsync();
            if (result <= 0) return null;
            return Order;
            #endregion
        }

        public async Task<Order?> GetOrderByIdForSpecificUserAsync(string BuyerEmail, int OrderId)
        {
            var Spec = new OrderSpecifications(BuyerEmail, OrderId);
            var Order = await _unitOfWork.Repository<Order>().GetByIdWithSpecAsync(Spec);
            return Order;
        }

        public async Task<IReadOnlyList<Order>> GetOrdersForSpecificUserAsync(string BuyerEmail)
        {
            var Spec = new OrderSpecifications(BuyerEmail);
            var Orders = await _unitOfWork.Repository<Order>().GetAllWithSpecAsync(Spec);
            return Orders;
        }
    }
}
