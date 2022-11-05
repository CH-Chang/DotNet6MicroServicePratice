namespace Mango.Web.Services.IServices
{
    using Mango.Web.Models;

    /// <summary>
    /// 商品服務介面
    /// </summary>
    public interface IProductService
    {
        /// <summary>
        /// 取得所有商品
        /// </summary>
        /// <typeparam name="T">商品物件</typeparam>
        /// <returns>所有商品</returns>
        public Task<T> GetAllProductsAsync<T>();

        /// <summary>
        /// 取得特定商品
        /// </summary>
        /// <typeparam name="T">商品物件</typeparam>
        /// <param name="id">商品編號</param>
        /// <returns>特定商品</returns>
        public Task<T> GetProductByIdAsync<T>(int id);

        /// <summary>
        /// 創建商品
        /// </summary>
        /// <typeparam name="T">商品物件</typeparam>
        /// <param name="productDto">商品</param>
        /// <returns>成功創建商品</returns>
        public Task<T> CreateProductAsync<T>(ProductDto productDto);

        /// <summary>
        /// 更新商品
        /// </summary>
        /// <typeparam name="T">商品物件</typeparam>
        /// <param name="productDto">商品</param>
        /// <returns>成功更新商品</returns>
        public Task<T> UpdateProductAsync<T>(ProductDto productDto);

        /// <summary>
        /// 刪除商品
        /// </summary>
        /// <typeparam name="T">商品物件</typeparam>
        /// <param name="id">商品編號</param>
        /// <returns>成功刪除商品</returns>
        public Task<T> DeleteProductAsync<T>(int id);
    }
}
