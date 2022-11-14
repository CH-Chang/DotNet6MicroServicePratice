namespace Mango.Services.CouponAPI
{
    using AutoMapper;
    using Mango.Services.CouponAPI.Models;
    using Mango.Services.CouponAPI.Models.Dto;

    /// <summary>
    /// 對應配置
    /// </summary>
    public class MappingConfig
    {
        /// <summary>
        /// 註冊對應
        /// </summary>
        /// <returns>對應配置</returns>
        public static MapperConfiguration RegisterMaps()
        {
            MapperConfiguration mapperConfiguration = new MapperConfiguration(config =>
            {
                config.CreateMap<CouponDto, Coupon>().ReverseMap();
            });

            return mapperConfiguration;
        }
    }
}
