using Mango.Services.CouponAPI.Models.Dto;

namespace Mango.Services.CouponAPI.Repository
{
    public interface ICouponRepository
    {
        public Task<CouponDto> GetCouponByCode(string couponCode);
    }
}
