using System.Collections.Generic;


using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Webkit;


namespace SyRead
{
    [Activity(Label = "SyRead", MainLauncher = true, Icon = "@drawable/icon", Theme = "@android:style/Theme.NoTitleBar")]
    public class MainActivity : Activity
    {
        public int counter = 2;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            WebView mWebView = FindViewById<WebView>(Resource.Id.webView1);

            // Get our button from the layout resource,
            // and attach an event to it
            //string url = "http://46.105.85.199:3000/api/books/getChapter?id=12&name=bookcontent6_0";

            //mWebView.LoadData(Content.ShowListOfBooks(), "text/html", "UTF-8");
            AddButtons();

        }
        public void AddButtons() {
            var Books = Shelf.ShowBooks(Shelf.FetchBook("http://46.105.85.199:3000/api/books"));
            var layout = new LinearLayout(this);
            layout.Orientation = Orientation.Vertical;

            var aLabel = new TextView(this);
            aLabel.Text = "Hello, World!!!";
            var Butts = new List<Button>();
            foreach (Book item in Books) {
                var butt = new Button(this);
                butt.Text = item.name;
                butt.Tag = item.Id;
                Butts.Add(butt);
                    }
            foreach (Button nButt in Butts) {
                nButt.Click += (sender, e) =>
            {
                var webForm = new WebView(this);
                string chaps = Shelf.FetchBook(string.Format("http://46.105.85.199:3000/api/books/getListChapter?id={0}", nButt.Tag));
                string name = Shelf.getChapter(chaps,1).Id;
                string url = string.Format("http://46.105.85.199:3000/api/books/getChapter?id={0}&name={1}", nButt.Tag, name);
               // webForm.SetMinimumHeight(578);
                webForm.LoadData(Shelf.ParseChapter(Shelf.FetchBook(url)), "text/html", "UTF-8");
                foreach (Button mButt in Butts)
                    layout.RemoveView(mButt);
                NextPage(nButt.Tag.ToString(), webForm, layout);
                layout.AddView(webForm);
                

            };
                layout.AddView(nButt);
            }

            SetContentView(layout);
        }
        public void NextPage(string id, WebView webForm, LinearLayout layout)
        {
            var NextPageButton = new Button(this);
            NextPageButton.Text = "Next chapter";
            NextPageButton.Click += (sender, e) =>
            {
                string chaps = Shelf.FetchBook(string.Format("http://46.105.85.199:3000/api/books/getListChapter?id={0}", id));
                string name = Shelf.getChapter(chaps, counter).Id;
                counter++;
                if (name == null)
                {
                    counter = 2;
                    webForm.LoadData("The End", "text/html", "UTF-8");
                    return;
                }
                string url = string.Format("http://46.105.85.199:3000/api/books/getChapter?id={0}&name={1}", id, name);
                webForm.LoadData(Shelf.ParseChapter(Shelf.FetchBook(url)), "text/html", "UTF-8");
            };
            layout.AddView(NextPageButton);
        }
    }
}

