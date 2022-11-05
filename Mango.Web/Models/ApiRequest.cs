namespace Mango.Web.Models
{
    using static Mango.Web.SD;

    /// <summary>
    /// API請求
    /// </summary>
    public class ApiRequest
    {
        /// <summary>
        /// API類型
        /// </summary>
        public ApiType ApiType { get; set; } = ApiType.GET;

        /// <summary>
        /// 網址
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 資料
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// 權杖
        /// </summary>
        public string AccessToken { get; set; }
    }
}
