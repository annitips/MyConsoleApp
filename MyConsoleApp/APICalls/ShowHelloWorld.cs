using MyConsoleApp.Model;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Configuration;
using System.Collections.Generic;

namespace MyConsoleApp.APICalls
{
    /// <summary>
    /// Method to just display Hello World on Console
    /// </summary>
    public class ShowHelloWorld
    {
        public static HttpClient client = new HttpClient();

        /// <summary>
        /// method to show HelloWorld on Console
        /// </summary>
        /// <param name="hello"></param>
        public static void ShowProduct(HelloWorld hello)
        {

                Console.WriteLine($"{hello.Name}");
        }

        public static async Task<HelloWorld> GetHelloAsync(string path)
        {
            var result = await client.GetAsync(path);

            HelloWorld hello = null;

            if (result.IsSuccessStatusCode)
            {
                hello = await result.Content.ReadAsAsync<HelloWorld>();
            }
            return hello;
        }

        public static async Task RunAsync()
        {

            var url = ConfigurationManager.AppSettings["APiBaseURL"];
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                HelloWorld hello = await GetHelloAsync("api/HelloWorld/Show");
                ShowProduct(hello);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }
    }
}
