namespace Mango.MessageBus
{
    /// <summary>
    /// Message Bus 介面
    /// </summary>
    public interface IMessageBus
    {
        /// <summary>
        /// 發布訊息
        /// </summary>
        /// <param name="message">訊息</param>
        /// <param name="topic">主題</param>
        /// <returns>發布結束</returns>
        public Task PublishMessage(BaseMessage message, string topic);
    }
}
