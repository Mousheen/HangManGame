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

namespace HangManGame
{
    public class ScoresDatabase
    {

        public string PlayerName { get; set; }

        public string Scores { get; set; }

        public ScoresDatabase(string playername, string scores)
        {
            PlayerName = playername;
            Scores = scores;
        }

        public ScoresDatabase() // needs a blank contructor for the app to work and pull data from
        {

        }
    }
}