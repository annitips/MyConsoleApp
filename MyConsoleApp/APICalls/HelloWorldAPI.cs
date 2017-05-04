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
    /// Enhanced version of API Calls for GetAll, GetById etc
    /// </summary>
    public class HelloWorldAPI
    {
        public static HttpClient client = new HttpClient();

        /// <summary>
        /// method to show HelloWorld on Console
        /// </summary>
        /// <param name="hello"></param>
        public static void ShowProduct(List<HelloWorld> hello)
        {
            foreach (var item in hello)
            {
                Console.WriteLine($"{item.Name}");
            }
        }

        public static async Task<List<HelloWorld>> GetHelloAsync(string path)
        {
            List<HelloWorld> hello = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                hello = await response.Content.ReadAsAsync<List<HelloWorld>>();
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
                List<HelloWorld> hello = await GetHelloAsync("api/HelloWorld/Show");
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
