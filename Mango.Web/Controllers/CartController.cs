namespace Mango.Web.Controllers
{
    using Mango.Web.Models;
    using Mango.Web.Services.IServices;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;

    public class CartController : Controller
    {
        private readonly IProductService productService;
        private readonly ICartService cartService;
        private readonly ICouponService couponService;

        /// <summary>
        /// 建構子
        /// </summary>
        /// <param name="productService">商品服務</param>
        public CartController(IProductService productService, ICartService cartService, ICouponService couponService)
        {
            this.productService = productService;
            this.cartService = cartService;
            this.couponService = couponService;
        }

        public async Task<IActionResult> CartIndex()
        {
            return this.View(await LoadCartDtoBasedOnLoggedInUser());
        }

        [HttpPost]
        [ActionName("ApplyCoupon")]
        public async Task<IActionResult> ApplyCoupon(CartDto cartDto)
        {
            var userId = this.User.Claims.Where(u => u.Type == "sub").FirstOrDefault().Value;
            var accessToken = await this.HttpContext.GetTokenAsync("access_token");
            var response = await this.cartService.ApplyCoupon<ResponseDto>(cartDto, accessToken);

            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(CartIndex));
            }

            return View();
        }

        [HttpPost]
        [ActionName("RemoveCoupon")]
        public async Task<IActionResult> RemoveCoupon(CartDto cartDto)
        {
            var userId = this.User.Claims.Where(u => u.Type == "sub").FirstOrDefault().Value;
            var accessToken = await this.HttpContext.GetTokenAsync("access_token");
            var response = await this.cartService.RemoveCoupon<ResponseDto>(cartDto.CartHeader.UserId, accessToken);

            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(CartIndex));
            }

            return View();
        }

        public async Task<IActionResult> Remove(int cartDetailsId)
        {
            var userId = this.User.Claims.Where(u => u.Type == "sub").FirstOrDefault().Value;
            var accessToken = await this.HttpContext.GetTokenAsync("access_token");
            var response = await this.cartService.RemoveFromCartAsync<ResponseDto>(cartDetailsId, accessToken);

            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(CartIndex));
            }

            return View();
        }

        private async Task<CartDto> LoadCartDtoBasedOnLoggedInUser()
        {
            var userId = this.User.Claims.Where(u => u.Type == "sub").FirstOrDefault().Value;
            var accessToken = await this.HttpContext.GetTokenAsync("access_token");
            var response = await this.cartService.GetCartByUserIdAsync<ResponseDto>(userId, accessToken);

            CartDto cartDto = new CartDto();
            if (response != null && response.IsSuccess)
            {
                string result = Convert.ToString(response.Result);
                cartDto = JsonConvert.DeserializeObject<CartDto>(result);
            }

            if (cartDto.CartHeader != null)
            {
                if (!string.IsNullOrEmpty(cartDto.CartHeader.CouponCode))
                {
                    var coupon = await couponService.GetCoupon<ResponseDto>(cartDto.CartHeader.CouponCode, accessToken);
                    if (coupon != null && coupon.IsSuccess)
                    {
                        var couponObj = JsonConvert.DeserializeObject<CouponDto>(Convert.ToString(coupon.Result));
                        cartDto.CartHeader.DiscountTotal = couponObj.DiscountAmount;
                    }
                }

                foreach (var detail in cartDto.CartDetails)
                {
                    cartDto.CartHeader.OrderTotal += (detail.Product.Price * detail.Count);
                }

                cartDto.CartHeader.OrderTotal -= cartDto.CartHeader.DiscountTotal;
            }

            return cartDto;
        }
    }
}
