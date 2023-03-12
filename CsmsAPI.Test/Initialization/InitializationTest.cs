using Newtonsoft.Json;
using System.Text;

namespace CsmsAPI.Test.Initialization
{
    public class InitializationTest
    {
        protected HttpClient _httpClient;

        [TestInitialize]
        public void TestInitialize()
        {
            var application = new MyWebApplication();
            _httpClient = application.CreateClient();
        }

        protected async Task<TResult> PostAsync<TResult>(string url, object obj)
        {
            var json = JsonConvert.SerializeObject(obj);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(url, data);

            var result = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<TResult>(result);
        }

        protected async Task<TResult> GetAsync<TResult>(string url) where TResult : class
        {
            var response = await _httpClient.GetAsync(url);

            var result = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<TResult>(result);
        }

        protected async Task<TResult> PutAsync<TResult>(string url, object obj)
        {
            var json = JsonConvert.SerializeObject(obj);
            var data = new StringContent(json, Encoding.UTF8, "application/json");


            var response = await _httpClient.PutAsync(url, data);

            var result = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<TResult>(result);
        }

        protected async Task<TResult> DeleteAsync<TResult>(string url) where TResult : class
        {
            var response = await _httpClient.DeleteAsync(url);

            var result = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<TResult>(result);
        }
    }
}