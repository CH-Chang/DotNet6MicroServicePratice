namespace Mango.Services.ProductAPI.Repository
{
    using System.Collections.Generic;
    using Mango.Services.ProductAPI.Models.Dto;

    /// <summary>
    /// 商品倉庫介面
    /// </summary>
    public interface IProductRepository
    {
        /// <summary>
        /// 取得所有商品
        /// </summary>
        /// <returns>所有商品</returns>
        public Task<IEnumerable<ProductDto>> GetProducts();

        /// <summary>
        /// 取得指定ID的商品
        /// </summary>
        /// <param name="productId">商品ID</param>
        /// <returns>指定ID的商品</returns>
        public Task<ProductDto> GetProductById(int productId);

        /// <summary>
        /// 新增或更新商品
        /// </summary>
        /// <param name="productDto">商品</param>
        /// <returns>新增或更新後的商品</returns>
        public Task<ProductDto> CreateOrUpdateProduct(ProductDto productDto);

        /// <summary>
        /// 刪除商品
        /// </summary>
        /// <param name="productId">商品ID</param>
        /// <returns>成功與否</returns>
        public Task<bool> DeleteProduct(int productId);
    }
}
