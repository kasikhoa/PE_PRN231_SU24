using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;

namespace PEPRN231_SU24_009909_NguyenDoanAnhKhoa_FE
{
    public static class Common
    {
        public  static string BaseURL = "https://localhost:44318";
        public static async Task<HttpResponseMessage> SendGetRequest(string url, string? jwt = null)
        {
            var httpClient = new HttpClient();
            if (jwt != null)
            {
                httpClient.DefaultRequestHeaders.Clear();
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
            }
            return await httpClient.GetAsync(url);
        }
        public static async Task<HttpResponseMessage?> SendRequestWithBody<T>(T? body, string url, string? jwt = null, string? method = "Post")
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(BaseURL);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            if (jwt != null)
            {
                httpClient.DefaultRequestHeaders.Clear();
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
            }
            using StringContent jsonContent = new(
                                JsonSerializer.Serialize(body),
                                Encoding.UTF8,
                                "application/json");
            if (method == "Post") return await httpClient.PostAsync(url, jsonContent);
            if (method == "Put") return await httpClient.PutAsync(url, jsonContent);
            if (method == "Delete") return await httpClient.DeleteAsync(url);
            return null;
        }
    }
}
