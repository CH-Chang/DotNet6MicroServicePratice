namespace Mango.Web.Models
{
    /// <summary>
    /// 回應資料轉換物件
    /// </summary>
    public class ResponseDto
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSuccess { get; set; } = false;

        /// <summary>
        /// 展示訊息
        /// </summary>
        public string DisplayMessage { get; set; } = string.Empty;

        /// <summary>
        /// 錯誤訊息
        /// </summary>
        public IList<string> ErrorMessages { get; set; }

        /// <summary>
        /// 結果內容
        /// </summary>
        public object Result { get; set; }
    }
}
