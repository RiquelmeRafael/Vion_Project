using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Vion_Desktop.Services
{
    public static class ApiClient
    {
        private static readonly HttpClient _client = new HttpClient();
        private static string _baseUrl;
        private static string _token = "";
        public static string CurrentRole { get; private set; } = "";

        static ApiClient()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            IConfiguration config = builder.Build();
            _baseUrl = config["ApiSettings:BaseUrl"] ?? "http://10.136.46.31:5301/";

            _client.BaseAddress = new Uri(_baseUrl);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public static void SetToken(string token, string role = "")
        {
            _token = token;
            if (!string.IsNullOrEmpty(role)) CurrentRole = role;
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        public static async Task<T> GetAsync<T>(string endpoint)
        {
            var response = await _client.GetAsync(endpoint);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(content)!;
        }

        public static async Task<TResponse> PostAsync<TRequest, TResponse>(string endpoint, TRequest data)
        {
            var json = JsonConvert.SerializeObject(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(endpoint, content);
            
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Erro na requisição: {response.StatusCode} - {errorContent}");
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TResponse>(responseContent)!;
        }

        public static async Task PutAsync<TRequest>(string endpoint, TRequest data)
        {
            var json = JsonConvert.SerializeObject(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync(endpoint, content);
            response.EnsureSuccessStatusCode();
        }

        public static async Task DeleteAsync(string endpoint)
        {
            var response = await _client.DeleteAsync(endpoint);
            response.EnsureSuccessStatusCode();
        }

        public static async Task<string> UploadFileAsync(string endpoint, string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("Arquivo não encontrado", filePath);

            using (var form = new MultipartFormDataContent())
            {
                using (var fileStream = new FileStream(filePath, FileMode.Open))
                {
                    using (var content = new StreamContent(fileStream))
                    {
                        content.Headers.ContentType = new MediaTypeHeaderValue("multipart/form-data");
                        form.Add(content, "arquivo", Path.GetFileName(filePath));

                        var response = await _client.PostAsync(endpoint, form);
                        
                        if (!response.IsSuccessStatusCode)
                        {
                            var errorContent = await response.Content.ReadAsStringAsync();
                            throw new Exception($"Erro no upload: {response.StatusCode} - {errorContent}");
                        }

                        var responseContent = await response.Content.ReadAsStringAsync();
                        dynamic? result = JsonConvert.DeserializeObject(responseContent);
                        return result?.url?.ToString() ?? "";
                    }
                }
            }
        }
    }
}
