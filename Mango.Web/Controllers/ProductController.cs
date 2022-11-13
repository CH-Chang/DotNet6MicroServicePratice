namespace Mango.Web.Controllers
{
    using System.Collections.Generic;
    using Mango.Web.Models;
    using Mango.Web.Services.IServices;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;

    /// <summary>
    /// 商品控制器
    /// </summary>
    public class ProductController : Controller
    {
        private readonly IProductService productService;

        /// <summary>
        /// 建構子
        /// </summary>
        /// <param name="productService">商品服務</param>
        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        /// <summary>
        /// 取得商品首頁
        /// </summary>
        /// <returns>商品首頁</returns>
        public async Task<IActionResult> ProductIndex()
        {
            List<ProductDto> list = new ();

            var response = await this.productService.GetAllProductsAsync<ResponseDto>();
            if (response != null && response.IsSuccess)
            {
                string result = Convert.ToString(response.Result) ?? throw new ArgumentException("Result should not be null");
                list = JsonConvert.DeserializeObject<List<ProductDto>>(result) ?? throw new ArgumentException("Json string should not be \"null\"");
            }

            return this.View(list);
        }

        /// <summary>
        /// 創建商品
        /// </summary>
        /// <returns>商品創建成功</returns>
        public IActionResult ProductCreate()
        {
            return this.View();
        }

        /// <summary>
        /// 創建商品API
        /// </summary>
        /// <param name="model">商品</param>
        /// <returns>商品創建成功</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductCreate(ProductDto model)
        {
            if (this.ModelState.IsValid)
            {
                var response = await this.productService.CreateProductAsync<ResponseDto>(model);
                if (response != null && response.IsSuccess)
                {
                    return this.RedirectToAction(nameof(this.ProductIndex));
                }
            }

            return this.View(model);
        }

        /// <summary>
        /// 編輯商品
        /// </summary>
        /// <param name="productId">商品編號</param>
        /// <returns>商品編輯成功</returns>
        public async Task<IActionResult> ProductEdit(int productId)
        {
            var response = await this.productService.GetProductByIdAsync<ResponseDto>(productId);
            if (response != null && response.IsSuccess)
            {
                string result = Convert.ToString(response.Result) ?? throw new ArgumentException("Result should not be null");
                ProductDto model = JsonConvert.DeserializeObject<ProductDto>(result) ?? throw new ArgumentException("Json string should not be \"null\"");
                return this.View(model);
            }

            return this.NotFound();
        }

        /// <summary>
        /// 編輯商品API
        /// </summary>
        /// <param name="model">商品</param>
        /// <returns>商品編輯成功</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductEdit(ProductDto model)
        {
            if (this.ModelState.IsValid)
            {
                var response = await this.productService.UpdateProductAsync<ResponseDto>(model);
                if (response != null && response.IsSuccess)
                {
                    return this.RedirectToAction(nameof(this.ProductIndex));
                }
            }

            return this.View(model);
        }

        /// <summary>
        /// 編輯商品
        /// </summary>
        /// <param name="productId">商品編號</param>
        /// <returns>商品編輯成功</returns>
        public async Task<IActionResult> ProductDelete(int productId)
        {
            var response = await this.productService.GetProductByIdAsync<ResponseDto>(productId);
            if (response != null && response.IsSuccess)
            {
                string result = Convert.ToString(response.Result) ?? throw new ArgumentException("Result should not be null");
                ProductDto model = JsonConvert.DeserializeObject<ProductDto>(result) ?? throw new ArgumentException("Json string should not be \"null\"");
                return this.View(model);
            }

            return this.NotFound();
        }

        /// <summary>
        /// 編輯商品API
        /// </summary>
        /// <param name="model">商品</param>
        /// <returns>商品編輯成功</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductDelete(ProductDto model)
        {
            var response = await this.productService.DeleteProductAsync<ResponseDto>(model.ProductId);
            if (response.IsSuccess)
            {
                return this.RedirectToAction(nameof(this.ProductIndex));
            }

            return this.View(model);
        }
    }
}
