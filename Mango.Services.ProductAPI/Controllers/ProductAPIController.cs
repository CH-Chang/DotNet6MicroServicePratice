namespace Mango.Services.ProductAPI.Controllers
{
    using Mango.Services.ProductAPI.Models.Dto;
    using Mango.Services.ProductAPI.Repository;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// 商品API控制器
    /// </summary>
    [Route("api/products")]
    [ApiController]
    public class ProductAPIController : ControllerBase
    {
        /// <summary>
        /// 回應
        /// </summary>
        protected ResponseDto response;

        private readonly IProductRepository productRepository;

        /// <summary>
        /// 建構子
        /// </summary>
        /// <param name="productRepository"></param>
        public ProductAPIController( IProductRepository productRepository)
        {
            this.productRepository = productRepository;
            this.response = new ResponseDto();
        }

        [HttpGet]
        public async Task<object> Get()
        {
            try
            {
                IEnumerable<ProductDto> productDtos = await this.productRepository.GetProducts();
                this.response.IsSuccess = true;
                this.response.Result = productDtos;
            }
            catch (Exception ex)
            {
                this.response.IsSuccess = false;
                this.response.ErrorMessages = new List<string>() { ex.Message };
            }

            return this.response;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<object> Get(int id)
        {
            try
            {
                ProductDto? productDto = await this.productRepository.GetProductById(id);

                if (productDto == null)
                {
                    this.response.IsSuccess = false;
                    this.response.ErrorMessages = new List<string>() { "Not exists" };
                } else
                {
                    this.response.IsSuccess = true;
                    this.response.Result = productDto;
                }
            }
            catch (Exception ex)
            {
                this.response.IsSuccess = false;
                this.response.ErrorMessages = new List<string>() { ex.Message };
            }

            return this.response;
        }
    }
}
