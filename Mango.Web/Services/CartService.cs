namespace Mango.Web.Services
{
    using Mango.Web.Models;
    using Mango.Web.Services.IServices;

    public class CartService : BaseService, ICartService
    {
        /// <summary>
        /// 建構子
        /// </summary>
        /// <param name="httpClientFactory">HTTP倉庫實例</param>
        public CartService(IHttpClientFactory httpClientFactory)
            : base(httpClientFactory)
        {
        }

        public async Task<T> AddToCartAsync<T>(CartDto cartDto, string token = "")
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.POST,
                Url = SD.ShoppingCartAPIBase + "/api/cart/addCart",
                Data = cartDto,
                AccessToken = token,
            });
        }

        public async Task<T> ApplyCoupon<T>(CartDto cartDto, string token = "")
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.POST,
                Url = SD.ShoppingCartAPIBase + "/api/cart/applyCoupon",
                Data = cartDto,
                AccessToken = token,
            });
        }

        public async Task<T> Checkout<T>(CartHeaderDto cartHeaderDto, string token = "")
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.POST,
                Url = SD.ShoppingCartAPIBase + "/api/cart/checkout",
                Data = cartHeaderDto,
                AccessToken = token,
            });
        }

        public async Task<T> GetCartByUserIdAsync<T>(string userId, string token = "")
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ShoppingCartAPIBase + "/api/cart/getCart/" + userId,
                AccessToken = token,
            });
        }

        public async Task<T> RemoveCoupon<T>(string userId, string token = "")
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.POST,
                Url = SD.ShoppingCartAPIBase + "/api/cart/removeCoupon",
                Data = userId,
                AccessToken = token,
            });
        }

        public async Task<T> RemoveFromCartAsync<T>(int cartId, string token = "")
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.POST,
                Url = SD.ShoppingCartAPIBase + "/api/cart/removeCart",
                Data = cartId,
                AccessToken = token,
            });
        }

        public async Task<T> UpdateCartAsync<T>(CartDto cartDto, string token = "")
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.POST,
                Url = SD.ShoppingCartAPIBase + "/api/cart/updateCart",
                Data = cartDto,
                AccessToken = token,
            });
        }
    }
}
