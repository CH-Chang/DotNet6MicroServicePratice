namespace Mango.Services.ProductAPI.Repository
{
    using AutoMapper;
    using Mango.Services.ProductAPI.DbContexts;
    using Mango.Services.ProductAPI.Models;
    using Mango.Services.ProductAPI.Models.Dto;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// 商品倉庫實作
    /// </summary>
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        /// <summary>
        /// 建構子
        /// </summary>
        /// <param name="dbContext">資料庫上下文</param>
        /// <param name="mapper">對應器</param>
        public ProductRepository(ApplicationDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        /// <inheritdoc/>
        public Task<ProductDto> CreateOrUpdateProduct(ProductDto productDto)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteProduct(int productId)
        {
            try
            {
                Product? product = await this.dbContext.Products.FirstOrDefaultAsync(p => p.ProductId == productId);

                if (product == null)
                {
                    return false;
                }

                this.dbContext.Remove(product);
                await this.dbContext.SaveChangesAsync();

                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }

        /// <inheritdoc/>
        public async Task<ProductDto?> GetProductById(int productId)
        {
            Product? product = await this.dbContext.Products.Where(p => p.ProductId == productId).FirstOrDefaultAsync();
            ProductDto? productDto = this.mapper.Map<ProductDto?>(product);
            return productDto;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<ProductDto>> GetProducts()
        {
            IEnumerable<Product> products = await this.dbContext.Products.ToListAsync();
            IEnumerable<ProductDto> productDtos = this.mapper.Map<List<ProductDto>>(products);
            return productDtos;
        }
    }
}
