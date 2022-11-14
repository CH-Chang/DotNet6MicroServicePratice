using Mango.Services.ShppingCartAPI.Models.Dto;

namespace Mango.Services.ShppingCartAPI.Repository
{
    public class CartRepository : ICartRepository
    {
        public async Task<bool> ClearCart(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<CartDto> CreateUpdateCart(CartDto cartDto)
        {
            throw new NotImplementedException();
        }

        public async Task<CartDto> GetCartByUserId(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RemoveFromCart(int cartDetailsId)
        {
            throw new NotImplementedException();
        }
    }
}
