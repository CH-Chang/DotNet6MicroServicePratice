namespace Mango.Services.ProductAPI
{
    using AutoMapper;
    using Mango.Services.ShppingCartAPI.Models;
    using Mango.Services.ShppingCartAPI.Models.Dto;

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
                config.CreateMap<ProductDto, Product>();
                config.CreateMap<CartHeaderDto, CartHeader>();
                config.CreateMap<CartDetailsDto, CartDetails>();
                config.CreateMap<CartDto, Cart>();
            });

            return mapperConfiguration;
        }
    }
}
