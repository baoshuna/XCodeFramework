using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace UseHttpClient
{
    class Program
    {
        static void Main(string[] args)
        {
            HttpClient client = HttpClientFactory.Create(new MyMessageHandler());
            client.BaseAddress = new Uri("127.0.0.1：8080");
            // 请求标头加上json
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("appication/json"));

            HttpResponseMessage message = client.GetAsync("api/book").Result;
            // client.PostAsJsonAsync<string>("",)
            if (message.IsSuccessStatusCode)
            {
                var book = message.Content.ReadAsAsync<string>().Result;
            }
            else
            {
                Console.WriteLine("请求api失败");
            }
            Console.ReadKey();
        }
    }
}
