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

namespace HangManGame
{
    [Activity(Label = "PlayerActivity")]
    public class PlayerActivity : Activity
    {
        string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "dbPlayer.db3");

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Player);

            Button btnStart = FindViewById<Button>(Resource.Id.btnStart);
            Button btnBack = FindViewById<Button>(Resource.Id.btnBack);
            EditText txtName = FindViewById<EditText>(Resource.Id.txtName);

            btnStart.Click += delegate
            {
                if (txtName.Text != "")
                {
                    Android.Widget.Toast.MakeText(this, "Welcome: " + txtName.Text, ToastLength.Long).Show();

                    var db = new SQLiteConnection(dbPath);

                    PlayerDatabase Player = new PlayerDatabase(1, txtName.Text, 0);
                    db.Insert(Player);

                    StartActivity(typeof(MainGame));
                }
                else
                {
                    Toast.MakeText(this, "Please Enter Player Name", ToastLength.Long).Show();
                }
            };

            btnBack.Click += delegate
            {
                Finish();
            };
        }

        public void onBackPressed() //if you press back on your phone
        {
            AlertDialog.Builder dialog = new AlertDialog.Builder(this, AlertDialog.ThemeHoloDark);
            AlertDialog alert = dialog.Create();
            alert.SetCancelable(false);

            alert.SetTitle("GO BACK");
            alert.SetMessage("You Will Loose Current Progress");

            alert.SetButton("YES", (c, ev) =>
            {
                Finish();
            });
            alert.SetButton2("NO", (c, ev) =>
            {
                alert.Cancel();
            });
            alert.Show();
        }
    }
}