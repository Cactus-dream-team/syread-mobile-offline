using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Threading.Tasks;
using Android.Webkit;
using Newtonsoft.Json;

namespace ADVANCEDCACTUSDREAMAPPFORVEDROPRO
{
    [Activity(Label = "ADVANCED CACTUS DREAMAPP FOR VEDRO PRO", MainLauncher = true, Icon = "@drawable/icon", Theme = "@android:style/Theme.NoTitleBar")]
    public class Chapter
    {
        public string chapter { get; set; }
    }
    public class MainActivity : Activity
    {
       // private List<Book> books = Book.DownloadBooksAsync();
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            
            //initialize layout components
            WebView mWebView = FindViewById<WebView>(Resource.Id.webView1);
            string url = "http://46.105.85.199:3000/api/books/getChapter?id=12&name=bookcontent6_0";
            var book = Book.FetchBook(url);
            var chapter = JsonConvert.DeserializeObject<Chapter>(book).chapter;
            mWebView.LoadData(Book.ParseChapter(book), "text/html", "UTF-8");
        }
    }


}

