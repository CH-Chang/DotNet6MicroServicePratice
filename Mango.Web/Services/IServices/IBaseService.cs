namespace Mango.Web.Services.IServices
{
    using Mango.Web.Models;

    /// <summary>
    /// 基礎服務介面
    /// </summary>
    public interface IBaseService : IDisposable
    {
        /// <summary>
        /// 回應物件
        /// </summary>
        public ResponseDto ResponseModel { get; set; }

        /// <summary>
        /// 傳送
        /// </summary>
        /// <typeparam name="T">物件類型</typeparam>
        /// <param name="apiRequest">物件</param>
        /// <returns>結果</returns>
        public Task<T> SendAsync<T>(ApiRequest apiRequest);
    }
}
