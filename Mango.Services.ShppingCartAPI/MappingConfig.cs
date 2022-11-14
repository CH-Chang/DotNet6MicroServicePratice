namespace Mango.Services.ProductAPI
{
    using AutoMapper;

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
            });

            return mapperConfiguration;
        }
    }
}
