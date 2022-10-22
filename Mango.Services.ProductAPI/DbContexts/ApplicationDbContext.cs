namespace Mango.Services.ProductAPI.DbContexts
{
    using Mango.Services.ProductAPI.Models;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// 應用程式資料庫上下文類別
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        /// <summary>
        /// 建構子
        /// </summary>
        /// <param name="options">資料庫上下文選項</param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// 商品表
        /// </summary>
        public DbSet<Product> Products { get; set; }
    }
}
