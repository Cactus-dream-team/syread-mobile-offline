using System;
using System.IO;
using Newtonsoft.Json;
using System.Net;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
namespace SyRead
{

    public class Book
    {
        public int Id { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public JObject metaData { get; set; }
        public string chapter { get; set; }
    }
    public class Chapter
    {
        public string Id { get; set; }
        public string href { get; set; }
        public string media_type { get; set; }
        public string title { get; set; }
        public int order { get; set; }
        public int level { get; set; }
    }
    public class Chapters
    {
        public List<Chapter> chapters { get; set; }
    }
    public class ChContent
    {
        public string chapter { get; set; }
    }
    public class Shelf
    {
        public static string FetchBook(string url)
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
            var cont = JsonConvert.DeserializeObject<ChContent>(json).chapter;
            return cont;
        }
        public static Chapter getChapter(string json, int position)
        {
 //           var jz = json.Replace(" \"chapters\": ", "");
            var chaps = JsonConvert.DeserializeObject<Chapters>(json);
             Chapter first = chaps.chapters[3];
             foreach(Chapter ch in chaps.chapters)
             {
                 if (ch.order == position)
                    first = ch;
             }
            return first;
        }

        public static List<Book> ShowBooks(string json)
        {
            // Extract the array of name/value results for the field name "weatherObservation". 
              List<Book> books = JsonConvert.DeserializeObject<List<Book>>(json);
            //Book books = JsonConvert.DeserializeObject<Book>(json);
            return books;
        }
    }
}