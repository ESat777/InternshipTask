using System.Net;
using InternshipTask.Models;
using Newtonsoft.Json;
using System.Text.Json;
using System.Text;
using InternshipTask.Settings;

namespace InternshipTask.API
{
    public class TestAPI
    {
        private static readonly HttpClient httpClient = new HttpClient
        {
            BaseAddress = new Uri(Constants.BASE_URL)
        };
        private static readonly JsonSerializerOptions jsonSerializerOptions
    = new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };



        public static async Task<List<Post>> GetPostsAsync(string url)
        {
            string result;
            var httpRequest = (HttpWebRequest)WebRequest.Create(url);
            httpRequest.Accept = "application/json";

            var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                result = await streamReader.ReadToEndAsync();
            }

            List<Post> posts = JsonConvert.DeserializeObject<List<Post>>(result);

            return posts;
        }

        public static async Task<Post> GetPostAsync(string url)
        {
            string result;
            var httpRequest = (HttpWebRequest)WebRequest.Create(url);
            httpRequest.Accept = "application/json";

            var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                result = await streamReader.ReadToEndAsync();
            }

            Post post = JsonConvert.DeserializeObject<Post>(result);

            return post;
        }

        public static async Task<object> AddPostAsync(Post post)
        {
            try
            {
                using WebClient webClient = new WebClient();
                webClient.BaseAddress = Constants.BASE_URL;
                var url = "/posts";
                webClient.Headers[HttpRequestHeader.ContentType] = "aplication/json; charset=utf-8";
                string data = JsonConvert.SerializeObject(post);
                Console.WriteLine(data);
                string response = webClient.UploadString(url, data);


                var des = JsonConvert.DeserializeObject<Post>(response);
                Console.WriteLine(des);
                return des;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public static async Task<Post> SendPostAsync(Post post)
        {
            string result = null;
            var jsonContent = System.Text.Json.JsonSerializer.Serialize(post,jsonSerializerOptions);
            using var httpContent = new StringContent
                (jsonContent, Encoding.UTF8, "application/json");

            using var response = await httpClient.PostAsync("posts", httpContent);
            string responseBody = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<Post>(responseBody);            
        }
    }
}
