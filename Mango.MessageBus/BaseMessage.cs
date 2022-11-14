namespace Mango.MessageBus
{
    /// <summary>
    /// 訊息基礎類別
    /// </summary>
    public class BaseMessage
    {
        /// <summary>
        /// 訊息唯一識別碼
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 訊息創建時間
        /// </summary>
        public DateTime MessageCreated { get; set; }
    }
}
