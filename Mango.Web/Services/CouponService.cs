namespace Mango.Web.Services
{
    using Mango.Web.Models;
    using Mango.Web.Services.IServices;

    public class CouponService : BaseService, ICouponService
    {
        /// <summary>
        /// 建構子
        /// </summary>
        /// <param name="httpClientFactory">HTTP倉庫實例</param>
        public CouponService(IHttpClientFactory httpClientFactory)
            : base(httpClientFactory)
        {
        }

        public async Task<T> GetCoupon<T>(string couponCode, string token = "")
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.CouponAPIBase + "/api/coupon/" + couponCode,
                AccessToken = token,
            });
        }
    }
}
