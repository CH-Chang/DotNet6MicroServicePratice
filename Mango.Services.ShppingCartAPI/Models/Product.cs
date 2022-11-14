namespace Mango.Services.ShppingCartAPI.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// 商品模型
    /// </summary>
    public class Product
    {
        /// <summary>
        /// 商品ID
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProductId { get; set; }

        /// <summary>
        /// 商品名稱
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// 商品說明
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 商品價格
        /// </summary>
        [Range(1, 1000)]
        public double Price { get; set; }

        /// <summary>
        /// 商品類別名稱
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// 商品圖片網址
        /// </summary>
        public string ImageUrl { get; set; }
    }
}
