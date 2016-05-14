using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.Http;
using System.Threading.Tasks;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace ADVANCEDCACTUSDREAMAPPFORVEDROPRO
{
    class Book
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }

        public static async Task<List<Book>> DownloadBooksAsync()
        {
            string url = "http://javatechig.com/api/get_category_posts/?dev=1&slug=android";


            var httpClient = new HttpClient();
            Task<string> downloadTask = httpClient.GetStringAsync(url);
            string content = await downloadTask;
            Console.Out.WriteLine("Response: \r\n {0}", content);
            var books = new List<Book>();
            JObject jsonResponse = JObject.Parse(content);
            IList<JToken> results = jsonResponse["posts"].ToList();
            foreach (JToken token in results)
            {
                //PointOfInterest poi = JsonConvert.DeserializeObject<Book>(token.ToString());
                books.Add(JsonConvert.DeserializeObject<Book>(token.ToString()));
            }
            return books;
        }

    }
}