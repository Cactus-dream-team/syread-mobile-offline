using System.Collections.Generic;


using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Webkit;
using Java.Lang;

namespace SyRead
{
   
    [Activity(Label = "SyRead", MainLauncher = true, Icon = "@drawable/icon", Theme = "@android:style/Theme.NoTitleBar")]
    public class MainActivity : Activity, GestureDetector.IOnGestureListener
    {
        private GestureDetector _gestureDetector;
        
        public int counter = 2;
        public override bool OnTouchEvent(MotionEvent e)
        {
            _gestureDetector.OnTouchEvent(e);
            return false;
        }
        public bool OnFling(MotionEvent e1, MotionEvent e2, float velocityX, float velocityY)
        {
           /* if (fag == true)
            {
                layout.RemoveView(webForm);
                layout.AddView(HomeButton);
                layout.AddView(NextPageButton);
                layout.AddView(PreviousPageButton);
                layout.AddView(webForm);
                fag = false;
            }
            else
            {
                layout.RemoveView(HomeButton);
                layout.RemoveView(NextPageButton);
                layout.RemoveView(PreviousPageButton);
                
                fag = true;

            }
            */
            return true;
        }
        public bool OnDown(MotionEvent e)
        {
            return false;
        }

        public void OnLongPress(MotionEvent e) {
            /*
            if (fag == true)
            {
                layout.RemoveView(webForm);
                layout.AddView(HomeButton);
                layout.AddView(NextPageButton);
                layout.AddView(PreviousPageButton);
                layout.AddView(webForm);
                fag = false;
            }
            else
            {
                layout.RemoveView(HomeButton);
                layout.RemoveView(NextPageButton);
                layout.RemoveView(PreviousPageButton);
                
                fag = true;
                
            }
            //return true;
            */
        }
        public bool OnScroll(MotionEvent e1, MotionEvent e2, float distanceX, float distanceY)
        {
            return false;
        }
        public void OnShowPress(MotionEvent e) { }
        public bool OnSingleTapUp(MotionEvent e)
        {
            return false;
        }
        public bool fag = false;
        LinearLayout layout;
        WebView webForm;
        Button HomeButton;
        Button NextPageButton;
        Button PreviousPageButton;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            layout = new LinearLayout(this);
            layout.Orientation = Orientation.Vertical;
            _gestureDetector = new GestureDetector(this);
           // webForm = FindViewById<WebView>(Resource.Id.webView1);
             WebView mWebView = FindViewById<WebView>(Resource.Id.webView1);

            // Get our button from the layout resource,
            // and attach an event to it
            //string url = "http://46.105.85.199:3000/api/books/getChapter?id=12&name=bookcontent6_0";

            mWebView.LoadData(Content.ShowListOfBooks(), "text/html", "UTF-8");

            AddButtons();

        }


        public void AddButtons() {
            var Books = Shelf.ShowBooks(Shelf.FetchBook("http://46.105.85.199:3000/api/books"));
            
            HomeButton = new Button(this);
            HomeButton.Text = "Home";
           
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

                 webForm = new WebView(this);
               // webForm = FindViewById<WebView>(Resource.Id.webView1);
                string chaps = Shelf.FetchBook(string.Format("http://46.105.85.199:3000/api/books/getListChapter?id={0}", nButt.Tag));
                string name = Shelf.getChapter(chaps,1).Id;
                string url = string.Format("http://46.105.85.199:3000/api/books/getChapter?id={0}&name={1}", nButt.Tag, name);
                
                webForm.LoadData(Shelf.ParseChapter(Shelf.FetchBook(url)), "text/html; charset=UTF-8", "UTF-8");
               // webForm = FindViewById<WebView>(Resource.Id.webView1);
                foreach (Button mButt in Butts)
                    layout.RemoveView(mButt);


                //--
                    NextPage(nButt.Tag.ToString(), layout);
                    layout.AddView(HomeButton);
                    PreviousPage(nButt.Tag.ToString(), layout);
                //--
                layout.AddView(webForm);



            };
                HomeButton.Click += (sender, e) =>
                {
                    layout.RemoveAllViews();
                    counter *= 0;
                    counter++;
                    counter++;
                    foreach (Button zButt in Butts)
                        layout.AddView(zButt);
                };
                layout.AddView(nButt);
            }

            SetContentView(layout);
        }
        public void NextPage(string id, LinearLayout layout)
        {
            NextPageButton = new Button(this);
            NextPageButton.Text = "Next chapter";
            NextPageButton.Click += (sender, e) =>
            {
                string chaps = Shelf.FetchBook(string.Format("http://46.105.85.199:3000/api/books/getListChapter?id={0}", id));
                string name = Shelf.getChapter(chaps, counter).Id;
                counter++;
                if ((name == null) && (counter>0))
                {
                    counter = 2;
                    webForm.LoadData("The End", "text/html; charset=UTF-8", "UTF-8");
                    return;
                }
                string url = string.Format("http://46.105.85.199:3000/api/books/getChapter?id={0}&name={1}", id, name);
                webForm.LoadData(Shelf.ParseChapter(Shelf.FetchBook(url)), "text/html; charset=UTF-8", "windows-1251");
            };
            layout.AddView(NextPageButton);
        }
        public void PreviousPage(string id, LinearLayout layout)
        {
            PreviousPageButton = new Button(this);
            PreviousPageButton.Text = "Previous chapter";
            PreviousPageButton.Click += (sender, e) =>
            {
                string chaps = Shelf.FetchBook(string.Format("http://46.105.85.199:3000/api/books/getListChapter?id={0}", id));
                string name = Shelf.getChapter(chaps, counter-2).Id;
                counter--;
                if (counter < 0)
                {
                    counter = 0;
                    webForm.LoadData("The End", "text/html; charset=UTF-8", "UTF-8");
                    return;
                }
                string url = string.Format("http://46.105.85.199:3000/api/books/getChapter?id={0}&name={1}", id, name);
                webForm.LoadData(Shelf.ParseChapter(Shelf.FetchBook(url)), "text/html; charset=UTF-8", "windows-1251");
            };
            layout.AddView(PreviousPageButton);
        }
    }
}

