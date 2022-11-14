using Mango.Services.OrderAPI.Models;

namespace Mango.Services.OrderAPI.Repository
{
    public interface IOrderRepository
    {
        public Task<bool> AddOrder(OrderHeader orderHeader);

        public Task UpdateOrderPaymentStatus(int orderHeaderId, bool paid);
    }
}
