using System.Collections.Generic;

//using Android.UIKit;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Webkit;

namespace SyRead
{

    public class Content
    {
        public static string ShowListOfBooks() {
            string shelf_url = "http://46.105.85.199:3000/api/books";
            string books = "<h1> Your books:</h1>";
            foreach (Book item in Shelf.ShowBooks(Shelf.FetchBook(shelf_url)))
                books += "<p>" + item.name + "</p>";
            return books;
        }
    }
}