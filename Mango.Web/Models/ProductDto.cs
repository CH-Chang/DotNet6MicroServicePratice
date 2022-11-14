using System.ComponentModel.DataAnnotations;

namespace Mango.Web.Models
{
    /// <summary>
    /// 商品模型資料轉換物件
    /// </summary>
    public class ProductDto
    {
        public ProductDto()
        {
            Count = 1;
        }

        /// <summary>
        /// 商品ID
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// 商品名稱
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 商品說明
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 商品價格
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// 商品類別名稱
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// 商品圖片網址
        /// </summary>
        public string ImageUrl { get; set; }

        [Range(1, 100)]
        public int Count { get; set; }
    }
}
