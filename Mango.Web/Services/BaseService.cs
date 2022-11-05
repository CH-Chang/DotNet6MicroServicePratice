namespace Mango.Web.Services
{
    using System;
    using System.Text;
    using System.Text.Json;
    using Mango.Web.Models;
    using Mango.Web.Services.IServices;
    using static Mango.Web.SD;

    /// <summary>
    /// 基礎服務
    /// </summary>
    public class BaseService : IBaseService
    {
        /// <summary>
        /// 建構子
        /// </summary>
        /// <param name="httpClientFactory">HTTP實例工廠</param>
        public BaseService(IHttpClientFactory httpClientFactory)
        {
            this.HttpClientFactory = httpClientFactory;
            this.ResponseModel = new ResponseDto();
        }

        /// <inheritdoc />
        public ResponseDto ResponseModel { get; set; }

        /// <summary>
        /// HTTP實例工廠
        /// </summary>
        public IHttpClientFactory HttpClientFactory { get; set; }

        /// <inheritdoc />
        public async Task<T> SendAsync<T>(ApiRequest apiRequest)
        {
            try
            {
                HttpClient httpClient = this.HttpClientFactory.CreateClient("ManogoAPI");
                httpClient.DefaultRequestHeaders.Clear();

                HttpRequestMessage message = new HttpRequestMessage();
                message.Headers.Add("Accept", "application/json");
                message.RequestUri = new Uri(apiRequest.Url);

                if (apiRequest.Data != null)
                {
                    byte[] jsonBytes = JsonSerializer.SerializeToUtf8Bytes(apiRequest.Data);
                    string jsonString = Encoding.UTF8.GetString(jsonBytes);
                    message.Content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                }

                message.Method = apiRequest.ApiType switch
                {
                    ApiType.GET => HttpMethod.Get,
                    ApiType.POST => HttpMethod.Post,
                    ApiType.PUT => HttpMethod.Put,
                    ApiType.DELETE => HttpMethod.Delete,
                    _ => throw new Exception("Unsupported api type")
                };

                HttpResponseMessage apiResponse = await httpClient.SendAsync(message);

                string apiContent = await apiResponse.Content.ReadAsStringAsync();
                T apiResponseDto = JsonSerializer.Deserialize<T>(apiContent) ?? throw new ArgumentException("Json string should not be \"null\"");

                return apiResponseDto;
            }
            catch (Exception e)
            {
                ResponseDto dto = new ResponseDto()
                {
                    DisplayMessage = "Error",
                    ErrorMessages = new List<string>() { e.Message },
                    IsSuccess = false,
                };

                var resBytes = JsonSerializer.SerializeToUtf8Bytes(dto);
                var resString = Encoding.UTF8.GetString(resBytes);
                var resDto = JsonSerializer.Deserialize<T>(resString) ?? throw new ArgumentException("Json string should not be \"null\"");

                return resDto;
            }
        }

        /// <inheritdoc />
        public void Dispose()
        {
            GC.SuppressFinalize(true);
        }
    }
}
