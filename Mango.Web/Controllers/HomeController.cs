namespace Mango.Web.Controllers
{
    using System.Diagnostics;
    using Mango.Web.Models;
    using Mango.Web.Services;
    using Mango.Web.Services.IServices;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly IProductService productService;
        private readonly ICartService cartService;

        public HomeController(ILogger<HomeController> logger, IProductService productService, ICartService cartService)
        {
            this.logger = logger;
            this.productService = productService;
            this.cartService = cartService;

        }

        public async Task<IActionResult> Index()
        {
            List<ProductDto> list = new ();
            var response = await this.productService.GetAllProductsAsync<ResponseDto>(string.Empty);
            if (response != null && response.IsSuccess)
            {
                string result = Convert.ToString(response.Result) ?? throw new ArgumentException("Result should not be null");
                list = JsonConvert.DeserializeObject<List<ProductDto>>(result) ?? throw new ArgumentException("Json string should not be \"null\"");
            }

            return this.View(list);
        }

        [Authorize]
        public async Task<IActionResult> Details(int productId)
        {
            ProductDto productDto = new ();
            var response = await this.productService.GetProductByIdAsync<ResponseDto>(productId, string.Empty);
            if (response != null && response.IsSuccess)
            {
                string result = Convert.ToString(response.Result) ?? throw new ArgumentException("Result should not be null");
                productDto = JsonConvert.DeserializeObject<ProductDto>(result) ?? throw new ArgumentException("Json string should not be \"null\"");
            }

            return this.View(productDto);
        }

        [HttpPost]
        [ActionName("Details")]
        [Authorize]
        public async Task<IActionResult> DetailsPost(ProductDto productDto)
        {
            CartDto cartDto = new()
            {
                CartHeader = new CartHeaderDto
                {
                    UserId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value,
                },
            };

            CartDetailsDto cartDetails = new CartDetailsDto()
            {
                Count = productDto.Count,
                ProductId = productDto.ProductId,
            };

            var resp = await productService.GetProductByIdAsync<ResponseDto>(productDto.ProductId, "");
            if (resp != null && resp.IsSuccess)
            {
                cartDetails.Product = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(resp.Result));
            }
            List<CartDetailsDto> cartDetailsDtos = new();
            cartDetailsDtos.Add(cartDetails);
            cartDto.CartDetails = cartDetailsDtos;

            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var addToCartResp = await cartService.AddToCartAsync<ResponseDto>(cartDto, accessToken);
            if (addToCartResp != null && addToCartResp.IsSuccess)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(productDto);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }

        [Authorize]
        public async Task<IActionResult> Login()
        {
            return this.RedirectToAction(nameof(this.Index));
        }

        public IActionResult Logout()
        {
            return this.SignOut("Cookies", "oidc");
        }
    }
}
