namespace Mango.Services.ProductAPI
{
    using AutoMapper;
    using Mango.Services.ShoppingCartAPI.Models;
    using Mango.Services.ShoppingCartAPI.Models.Dto;

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
                config.CreateMap<ProductDto, Product>().ReverseMap();
                config.CreateMap<CartHeader, CartHeaderDto>().ReverseMap();
                config.CreateMap<CartDetails, CartDetailsDto>().ReverseMap();
                config.CreateMap<Cart, CartDto>().ReverseMap();
            });

            return mapperConfiguration;
        }
    }
}
