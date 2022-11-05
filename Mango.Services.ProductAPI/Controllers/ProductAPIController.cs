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
        private readonly ResponseDto response;
        private readonly IProductRepository productRepository;

        /// <summary>
        /// 建構子
        /// </summary>
        /// <param name="productRepository">商品倉庫</param>
        public ProductAPIController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
            this.response = new ResponseDto();
        }

        /// <summary>
        /// 取得所有商品
        /// </summary>
        /// <returns>所有商品</returns>
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

        /// <summary>
        /// 取得特定商品
        /// </summary>
        /// <param name="id">商品代號</param>
        /// <returns>特定商品</returns>
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
                }
                else
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

        /// <summary>
        /// 新增商品
        /// </summary>
        /// <param name="productDto">商品</param>
        /// <returns>新增成功商品</returns>
        [HttpPost]
        public async Task<object> Post([FromBody] ProductDto productDto)
        {
            try
            {
                ProductDto model = await this.productRepository.CreateOrUpdateProduct(productDto);

                this.response.IsSuccess = true;
                this.response.Result = model;
            }
            catch (Exception ex)
            {
                this.response.IsSuccess = false;
                this.response.ErrorMessages = new List<string>() { ex.Message };
            }

            return this.response;
        }

        /// <summary>
        /// 編輯商品
        /// </summary>
        /// <param name="productDto">商品</param>
        /// <returns>編輯成功商品</returns>
        [HttpPut]
        public async Task<object> Put([FromBody] ProductDto productDto)
        {
            try
            {
                ProductDto model = await this.productRepository.CreateOrUpdateProduct(productDto);

                this.response.IsSuccess = true;
                this.response.Result = model;
            }
            catch (Exception ex)
            {
                this.response.IsSuccess = false;
                this.response.ErrorMessages = new List<string>() { ex.Message };
            }

            return this.response;
        }

        /// <summary>
        /// 刪除商品
        /// </summary>
        /// <param name="id">商品代號</param>
        /// <returns>刪除成功與否</returns>
        [HttpDelete]
        [Route("{id}")]
        public async Task<object> Delete(int id)
        {
            try
            {
                bool success = await this.productRepository.DeleteProduct(id);

                this.response.IsSuccess = true;
                this.response.Result = success;
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
