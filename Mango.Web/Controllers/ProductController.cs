namespace Mango.Web.Controllers
{
    using System.Text.Json;
    using Mango.Web.Models;
    using Mango.Web.Services.IServices;
    using Microsoft.AspNetCore.Mvc;

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
                list = JsonSerializer.Deserialize<List<ProductDto>>(Convert.ToString(response.Result)) ?? throw new ArgumentException("Json string should not be \"null\"");
            }

            return this.View(list);
        }
    }
}
