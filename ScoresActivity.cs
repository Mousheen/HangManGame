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
using System.IO;
using Android.Content.PM;

namespace HangManGame
{
    [Activity(Label = "ScoresActivity", ScreenOrientation = ScreenOrientation.Portrait)]
    public class ScoresActivity : Activity
    {
        string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "dbPlayer.db3");

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Scores);

            Button btnBack = FindViewById<Button>(Resource.Id.btnBack);
            TextView tvScores = FindViewById<TextView>(Resource.Id.tvScores);

            var db = new SQLiteConnection(dbPath);

            //connect to the table that has the data we want
            var table = db.Table<PlayerDatabase>();

            if (table.Count() != 0) //do nothing if table already has words init
            {
                foreach (var item in table)
                {
                    PlayerDatabase Scores = new PlayerDatabase(item.ID, item.PlayerName, item.Scores);
                    tvScores.Text += "\n" + Scores;
                }
            }
            else if (table.Count() == 0) //add the records if the table is empty
            {
                tvScores.Text = "No Player Scores Currently";
            }

            btnBack.Click += delegate
            {
                Finish();
            };
        }
    }
}