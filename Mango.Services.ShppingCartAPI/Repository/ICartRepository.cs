using Mango.Services.ShppingCartAPI.Models.Dto;

namespace Mango.Services.ShppingCartAPI.Repository
{
    public interface ICartRepository
    {
        public Task<CartDto> GetCartByUserId(string userId);

        public Task<CartDto> CreateUpdateCart(CartDto cartDto);

        public Task<bool> RemoveFromCart(int cartDetailsId);

        public Task<bool> ClearCart(string userId);
    }
}
