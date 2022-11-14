namespace Mango.Web
{
    /// <summary>
    /// SD類別
    /// </summary>
    public static class SD
    {
        /// <summary>
        /// API類型枚舉
        /// </summary>
        public enum ApiType
        {
            /// <summary>
            /// 取得
            /// </summary>
            GET,

            /// <summary>
            /// 新增
            /// </summary>
            POST,

            /// <summary>
            /// 更新
            /// </summary>
            PUT,

            /// <summary>
            /// 刪除
            /// </summary>
            DELETE
        }

        /// <summary>
        /// 商品API
        /// </summary>
        public static string ProductAPIBase { get; set; }

        public static string ShoppingCartAPIBase { get; set; }

        public static string CouponAPIBase { get; set; }
    }
}
