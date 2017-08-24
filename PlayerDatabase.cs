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
    public class PlayerDatabase
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string PlayerName { get; set; }

        public int Scores { get; set; }

        public PlayerDatabase(int id, string player, int scores)
        {
            ID = id;
            PlayerName = player;
            Scores = scores;
        }

        public PlayerDatabase() // needs a blank contructor for the app to work and pull data from
        {

        }

        public override string ToString()
        {
            return string.Format("[ {0} ] - {1}", PlayerName, Scores);
        }
    }
}