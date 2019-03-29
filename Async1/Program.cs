using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime;
using System.Diagnostics;

namespace Async1
{
    class Program
    {
        static void Main(string[] args)
        {
            var x = Task.Run(() =>
            {
                WebClient webClient = new WebClient();
                var result = webClient.DownloadString("http://www.msdn.com");
                Console.WriteLine(result);
                return result;
            });
            x.Wait();
            Console.WriteLine("2121");
            Console.ReadKey();
        }


        //async标记这个方法支持异步,但是返回值必须是void或者是Task<T>

        static async Task<string> GetHtml()
        {
             var x= Task.Run(() => {
                 WebClient client = new WebClient();
                 var result = client.DownloadString("http://www.msdn.com");
                 return result;
            }); //这里启动一个异步，这下面的方法还会继续执行


            return await x;
        }

        static async void Test()
        {
            string str = await GetHtml();
            Console.WriteLine(str);
        }
    }
}
