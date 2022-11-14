namespace Mango.Web.Controllers
{
    using System.Diagnostics;
    using Mango.Web.Models;
    using Mango.Web.Services.IServices;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly IProductService productService;

        public HomeController(ILogger<HomeController> logger, IProductService productService)
        {
            this.logger = logger;
            this.productService = productService;

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
