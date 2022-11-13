namespace Mango.Web.Services
{
    using System;
    using System.Net.Http.Headers;
    using System.Text;
    using Mango.Web.Models;
    using Mango.Web.Services.IServices;
    using Newtonsoft.Json;
    using static Mango.Web.SD;

    /// <summary>
    /// 基礎服務
    /// </summary>
    public class BaseService : IBaseService
    {
        private readonly IHttpClientFactory httpClientFactory;

        /// <summary>
        /// 建構子
        /// </summary>
        /// <param name="httpClientFactory">HTTP實例工廠</param>
        public BaseService(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
            this.ResponseModel = new ResponseDto();
        }

        /// <inheritdoc />
        public ResponseDto ResponseModel { get; set; }

        /// <inheritdoc />
        public async Task<T> SendAsync<T>(ApiRequest apiRequest)
        {
            try
            {
                HttpClient httpClient = this.httpClientFactory.CreateClient("ManogoAPI");
                httpClient.DefaultRequestHeaders.Clear();

                HttpRequestMessage message = new HttpRequestMessage();
                message.Headers.Add("Accept", "application/json");
                message.RequestUri = new Uri(apiRequest.Url);

                if (apiRequest.Data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(apiRequest.Data), Encoding.UTF8, "application/json");
                }

                if (!string.IsNullOrEmpty(apiRequest.AccessToken))
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiRequest.AccessToken);
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
                T apiResponseDto = JsonConvert.DeserializeObject<T>(apiContent) ?? throw new ArgumentException("Json string should not be \"null\"");

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

                var resString = JsonConvert.SerializeObject(dto);
                var resDto = JsonConvert.DeserializeObject<T>(resString) ?? throw new ArgumentException("Json string should not be \"null\"");

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
