using Mango.Web.Models;

namespace Mango.Web.Services.IServices
{
    public interface ICartService
    {
        public Task<T> GetCartByUserIdAsync<T>(string userId, string token = "");

        public Task<T> AddToCartAsync<T>(CartDto cartDto, string token = "");

        public Task<T> UpdateCartAsync<T>(CartDto cartDto, string token = "");

        public Task<T> RemoveFromCartAsync<T>(int cartId, string token = "");

        public Task<T> ApplyCoupon<T>(CartDto cartDto, string token = "");

        public Task<T> RemoveCoupon<T>(string userId, string token = "");

        public Task<T> Checkout<T>(CartHeaderDto cartHeaderDto, string token = "");
    }
}
