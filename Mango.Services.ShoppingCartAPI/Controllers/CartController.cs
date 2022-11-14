using Mango.Services.ShoppingCartAPI.Models.Dto;
using Mango.Services.ShoppingCartAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.ShoppingCartAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : Controller
    {
        private readonly ICartRepository repository;
        protected ResponseDto response;

        public CartController(ICartRepository repository)
        {
            this.repository = repository;
            this.response = new ResponseDto();
        }

        [HttpGet("GetCart/{userId}")]
        public async Task<object> GetCart(string userId)
        {
            try
            {
                CartDto cartDto = await repository.GetCartByUserId(userId);
                response.Result = cartDto;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessages = new List<string>() { ex.ToString() } ;
            }

            return response;
        }

        [HttpPost("AddCart")]
        public async Task<object> AddCart(CartDto cartDto)
        {
            try
            {
                CartDto cartDt = await repository.CreateUpdateCart(cartDto);
                response.Result = cartDt;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessages = new List<string>() { ex.ToString() };
            }

            return response;
        }

        [HttpPost("UpdateCart")]
        public async Task<object> UpdateCart(CartDto cartDto)
        {
            try
            {
                CartDto cartDt = await repository.CreateUpdateCart(cartDto);
                response.Result = cartDt;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessages = new List<string>() { ex.ToString() };
            }

            return response;
        }

        [HttpPost("RemoveCart")]
        public async Task<object> UpdateCart([FromBody]int cartId)
        {
            try
            {
                bool isSuccess = await repository.RemoveFromCart(cartId);
                response.Result = isSuccess;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessages = new List<string>() { ex.ToString() };
            }

            return response;
        }
    }
}
