using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;

namespace HangManGame
{
    public class WordsListDatabase
    {
        public string Words { get; set; }

        public WordsListDatabase(string word)
        {
            Words = word;
        }

        public WordsListDatabase() // needs a blank contructor for the app to work and pull data from
        {

        }

        public override string ToString()
        {
            return string.Format("{1}", Words);
        }
    }
}