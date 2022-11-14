using AutoMapper;
using Mango.Services.ShoppingCartAPI.DbContexts;
using Mango.Services.ShoppingCartAPI.Models;
using Mango.Services.ShoppingCartAPI.Models.Dto;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.ShoppingCartAPI.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly IMapper mapper;

        public CartRepository(ApplicationDbContext applicationDbContext, IMapper mapper)
        {
            this.applicationDbContext = applicationDbContext;
            this.mapper = mapper;
        }

        public async Task<bool> ClearCart(string userId)
        {
            var cartHeaderFromDb = await applicationDbContext.CartHeader.FirstOrDefaultAsync(u => u.UserId == userId);
            if (cartHeaderFromDb != null)
            {
                applicationDbContext.CartDetails.RemoveRange(applicationDbContext.CartDetails.Where(u => u.CartHeaderId == cartHeaderFromDb.CartHeaderId));
                applicationDbContext.CartHeader.Remove(cartHeaderFromDb);
                await applicationDbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<CartDto> CreateUpdateCart(CartDto cartDto)
        {
            Cart cart = this.mapper.Map<Cart>(cartDto);

            var prodInDb = applicationDbContext.Products.First(u => u.ProductId == cartDto.CartDetails.FirstOrDefault().ProductId);
            if (prodInDb == null) {
                applicationDbContext.Products.Add(cart.CartDetails.FirstOrDefault().Product);
                await applicationDbContext.SaveChangesAsync();
            }

            var cartHeaderFromDb = await applicationDbContext.CartHeader.AsNoTracking().FirstOrDefaultAsync(u => u.UserId == cart.CartHeader.UserId);
            if (cartHeaderFromDb == null)
            {
                applicationDbContext.CartHeader.Add(cart.CartHeader);
                await applicationDbContext.SaveChangesAsync();
                cart.CartDetails.FirstOrDefault().CartHeaderId = cart.CartHeader.CartHeaderId;
                cart.CartDetails.FirstOrDefault().Product = null;
                applicationDbContext.CartDetails.Add(cart.CartDetails.FirstOrDefault());
                await applicationDbContext.SaveChangesAsync();
            }
            else
            {
                var carDetailsFromDb = await applicationDbContext.CartDetails.AsNoTracking().FirstOrDefaultAsync(u => u.ProductId == cart.CartDetails.FirstOrDefault().ProductId && u.CartHeaderId == cartHeaderFromDb.CartHeaderId);

                if (cartHeaderFromDb == null)
                {
                    cart.CartDetails.FirstOrDefault().CartHeaderId = cartHeaderFromDb.CartHeaderId;
                    cart.CartDetails.FirstOrDefault().Product = null;
                    applicationDbContext.CartDetails.Add(cart.CartDetails.FirstOrDefault());
                    await applicationDbContext.SaveChangesAsync();
                } else
                {
                    cart.CartDetails.FirstOrDefault().Product = null;
                    cart.CartDetails.FirstOrDefault().Count += carDetailsFromDb.Count;
                    applicationDbContext.CartDetails.Update(cart.CartDetails.FirstOrDefault());
                    await applicationDbContext.SaveChangesAsync();
                }
            }

            return this.mapper.Map<CartDto>(cart);
        }

        public async Task<CartDto> GetCartByUserId(string userId)
        {
            Cart cart = new Cart()
            {
                CartHeader = await applicationDbContext.CartHeader.FirstOrDefaultAsync(u => u.UserId == userId)
            };

            cart.CartDetails = applicationDbContext.CartDetails.Where(u => u.CartHeaderId == cart.CartHeader.CartHeaderId).Include(u => u.Product);

            return mapper.Map<CartDto>(cart);
        }

        public async Task<bool> RemoveFromCart(int cartDetailsId)
        {
            try
            {
                CartDetails cartDetails = await applicationDbContext.CartDetails.FirstOrDefaultAsync(u => u.CartDetailsId == cartDetailsId);

                int totalCountOfCartItems = applicationDbContext.CartDetails.Where(u => u.CartHeaderId == cartDetails.CartDetailsId).Count();

                applicationDbContext.CartDetails.Remove(cartDetails);

                if (totalCountOfCartItems == 1)
                {
                    var cartHeaderToRemove = await applicationDbContext.CartHeader.FirstOrDefaultAsync(u => u.CartHeaderId == cartDetails.CartHeaderId);
                    applicationDbContext.CartHeader.Remove(cartHeaderToRemove);
                }

                await applicationDbContext.SaveChangesAsync();

                return true;
            } catch (Exception)
            {
                return false;
            }
        }
    }
}
