using Mango.MessageBus;
using Mango.Services.ShoppingCartAPI.Messages;
using Mango.Services.ShoppingCartAPI.Models.Dto;
using Mango.Services.ShoppingCartAPI.RabbitMQSender;
using Mango.Services.ShoppingCartAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.ShoppingCartAPI.Controllers
{
    [Route("api/cart")]
    [ApiController]
    public class CartAPIController : Controller
    {
        private readonly ICartRepository repository;
        private readonly ICouponRepository couponRepository;
        private readonly IMessageBus messageBus;
        private readonly IRabbitMQCartMessageSender rabbitMQCartMessageSender;
        protected ResponseDto response;

        public CartAPIController(ICartRepository repository, ICouponRepository couponRepository, IMessageBus messageBus, IRabbitMQCartMessageSender rabbitMQCartMessageSender)
        {
            this.repository = repository;
            this.couponRepository = couponRepository;
            this.messageBus = messageBus;
            this.rabbitMQCartMessageSender = rabbitMQCartMessageSender;
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
        public async Task<object> RemoveCart([FromBody]int cartId)
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

        [HttpPost("ApplyCoupon")]
        public async Task<object> ApplyCoupon([FromBody] CartDto cartDto)
        {
            try
            {
                bool isSuccess = await repository.ApplyCoupon(cartDto.CartHeader.UserId, cartDto.CartHeader.CouponCode);
                response.Result = isSuccess;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessages = new List<string>() { ex.ToString() };
            }

            return response;
        }

        [HttpPost("RemoveCoupon")]
        public async Task<object> RemoveCoupon([FromBody] string userId)
        {
            try
            {
                bool isSuccess = await repository.RemoveCoupon(userId);
                response.Result = isSuccess;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessages = new List<string>() { ex.ToString() };
            }

            return response;
        }

        [HttpPost("Checkout")]
        public async Task<object> Checkout(CheckoutHeaderDto checkoutHeaderDto)
        {
            try
            {
                CartDto cartDto = await repository.GetCartByUserId(checkoutHeaderDto.UserId);
                if (cartDto == null)
                {
                    return BadRequest();
                }

                if (!string.IsNullOrEmpty(checkoutHeaderDto.CouponCode))
                {
                    CouponDto coupon = await couponRepository.GetCoupon(checkoutHeaderDto.CouponCode);
                    if (checkoutHeaderDto.DiscountTotal != coupon.DiscountAmount)
                    {
                        response.IsSuccess = false;
                        response.ErrorMessages = new List<string>() { "Coupon price has changed, please confirm" };
                        response.DisplayMessage = "Coupon price has changed, please confirm";
                        return response;
                    }
                }

                checkoutHeaderDto.CartDetails = cartDto.CartDetails;
                // await messageBus.PublishMessage(checkoutHeaderDto, "checkoutqueue");
                rabbitMQCartMessageSender.SendMessage(checkoutHeaderDto, "checkoutqueue");
                await repository.ClearCart(checkoutHeaderDto.UserId);
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
