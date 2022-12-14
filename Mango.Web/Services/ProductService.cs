namespace Mango.Web.Services
{
    using System.Threading.Tasks;
    using Mango.Web.Models;
    using Mango.Web.Services.IServices;

    /// <summary>
    /// 商品服務
    /// </summary>
    public class ProductService : BaseService, IProductService
    {
        /// <summary>
        /// 建構子
        /// </summary>
        /// <param name="httpClientFactory">HTTP倉庫實例</param>
        public ProductService(IHttpClientFactory httpClientFactory)
            : base(httpClientFactory)
        {
        }

        /// <inheritdoc />
        public async Task<T> CreateProductAsync<T>(ProductDto productDto, string token)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.POST,
                Url = SD.ProductAPIBase + "/api/products",
                Data = productDto,
                AccessToken = token,
            });
        }

        /// <inheritdoc />
        public async Task<T> DeleteProductAsync<T>(int id, string token)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = SD.ProductAPIBase + "/api/products/" + id,
                AccessToken = token,
            });
        }

        /// <inheritdoc />
        public async Task<T> GetAllProductsAsync<T>(string token)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ProductAPIBase + "/api/products",
                AccessToken = token,
            });
        }

        /// <inheritdoc />
        public async Task<T> GetProductByIdAsync<T>(int id, string token)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ProductAPIBase + "/api/products/" + id,
                AccessToken = token,
            });
        }

        /// <inheritdoc />
        public async Task<T> UpdateProductAsync<T>(ProductDto productDto, string token)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.PUT,
                Url = SD.ProductAPIBase + "/api/products/",
                Data = productDto,
                AccessToken = token,
            });
        }
    }
}
