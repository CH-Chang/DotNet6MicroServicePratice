using Mango.Web.Models;

namespace Mango.Web.Services.IServices
{
    public interface ICouponService
    {
        public Task<T> GetCoupon<T>(string couponCode, string token = "");
    }
}
