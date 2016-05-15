using System;
using System.Net;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
namespace ADVANCEDCACTUSDREAMAPPFORVEDROPRO
{
    public class Book
    {
        public int Id { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public string metadata { get; set; }

        

        public static string  FetchBook(string url)
        {
            // Create an HTTP web request using the URL:
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.Method = WebRequestMethods.Http.Get;
            request.Accept = "application/json";
            request.ContentType = "application/json";

            var response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string output = reader.ReadToEnd();
            //output.Append(reader.ReadToEnd());

            response.Close();
            return output;
        }

        public static Book ParseBook(string json)
        {
            // Extract the array of name/value results for the field name "weatherObservation". 
            Book rawBook = JsonConvert.DeserializeObject<Book>(json); 
            return rawBook;
        }
        public static string ParseChapter(string json)
        {
            return JsonConvert.DeserializeObject<string>(json);
        }

    }

}
