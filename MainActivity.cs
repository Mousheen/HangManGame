using Android.App;
using Android.Widget;
using Android.OS;
using System.IO;
using SQLite;
using System.Threading.Tasks;
using Android.Content;
using Android.Support.V7.App;
using Android.Content.PM;

namespace HangManGame
{
    [Activity(Label ="HangManGame", Theme = "@style/Theme.AppCompat.Light.NoActionBar", Icon ="@drawable/Icon", ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : AppCompatActivity
    {
        string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "dbPlayer.db3"); //path for the database to be created in
        string dbPath2 = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "dbWordList.db3");

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main); //sets the content view to the layout

            Button btnPlay = FindViewById<Button>(Resource.Id.btnPlay); //connects the elements from the layout
            Button btnScores = FindViewById<Button>(Resource.Id.BtnScores);
            Button btnExit = FindViewById<Button>(Resource.Id.btnExit);

            var db = new SQLiteConnection(dbPath2); //opens a connection to SQLLite 

            db.CreateTable<WordsListDatabase>(); // creates my table with empty fields

            var table = db.Table<WordsListDatabase>(); //connects to the table in the database

            if (table.Count() != 0) //do nothing if table already has words init
            {
                Toast.MakeText(this, "WORDS DATABASE EXISTS", ToastLength.Short).Show(); //toast message to check if database exists.
            }
            else if (table.Count() == 0) //add the records if the table is empty
            {
                Toast.MakeText(this, "WORDS DATABASE CREATED", ToastLength.Long).Show(); //toast message when table gets created

                WordsListDatabase word0 = new WordsListDatabase("FERMENT"); //creates an emtry in the table
                WordsListDatabase word1 = new WordsListDatabase("DISTINCTIVE");
                WordsListDatabase word2 = new WordsListDatabase("POTATOES");
                WordsListDatabase word3 = new WordsListDatabase("SMOKY");
                WordsListDatabase word4 = new WordsListDatabase("FRUITY");
                WordsListDatabase word5 = new WordsListDatabase("GRAVY");
                WordsListDatabase word6 = new WordsListDatabase("MATURED");
                WordsListDatabase word7 = new WordsListDatabase("SWEET");
                WordsListDatabase word8 = new WordsListDatabase("SAUSAGE");
                WordsListDatabase word9 = new WordsListDatabase("NUTRITIOUS");
                WordsListDatabase word10 = new WordsListDatabase("CABBAGE");
                WordsListDatabase word11 = new WordsListDatabase("AEDJVLDS");
                WordsListDatabase word12 = new WordsListDatabase("JOKES");
                WordsListDatabase word13 = new WordsListDatabase("REPUTATION");
                WordsListDatabase word14 = new WordsListDatabase("SERVED");
                WordsListDatabase word15 = new WordsListDatabase("POISON");
                WordsListDatabase word16 = new WordsListDatabase("WHISKEY");
                WordsListDatabase word17 = new WordsListDatabase("RAW");
                WordsListDatabase word18 = new WordsListDatabase("POISON");
                WordsListDatabase word19 = new WordsListDatabase("CRUST");
                WordsListDatabase word20 = new WordsListDatabase("TRUST");
                WordsListDatabase word21 = new WordsListDatabase("CULINARY");
                WordsListDatabase word22 = new WordsListDatabase("FRIED");
                WordsListDatabase word23 = new WordsListDatabase("CHEESE");
                WordsListDatabase word24 = new WordsListDatabase("MIXED");

                db.Insert(word0); db.Insert(word1); db.Insert(word2); db.Insert(word3); db.Insert(word4); //inserts the emtry into the table
                db.Insert(word5); db.Insert(word6); db.Insert(word7); db.Insert(word8); db.Insert(word9);
                db.Insert(word10); db.Insert(word11); db.Insert(word12); db.Insert(word13); db.Insert(word14);
                db.Insert(word15); db.Insert(word16); db.Insert(word17); db.Insert(word18); db.Insert(word19);
                db.Insert(word20); db.Insert(word21); db.Insert(word22); db.Insert(word23); db.Insert(word24);
            }

            btnPlay.Click += delegate
            {
                StartActivity(typeof(PlayerActivity));
                
                var db2 = new SQLiteConnection(dbPath); //connection for the sqllite database                
                db2.CreateTable<PlayerDatabase>(); //creates table from the class file
            };

            btnScores.Click += delegate
            {
                StartActivity(typeof(ScoresActivity)); //starts defined activity

                var db2 = new SQLiteConnection(dbPath); //connection for the sqllite database                
                db2.CreateTable<PlayerDatabase>(); //creates table from the class file
            };

            btnExit.Click += delegate
            {
                Android.App.AlertDialog.Builder dialog = new Android.App.AlertDialog.Builder(this, Android.App.AlertDialog.ThemeHoloDark);
                Android.App.AlertDialog alert = dialog.Create();
                alert.SetCancelable(false);
                alert.SetTitle("EXIT GAME");
                alert.SetMessage("Are You Sure?");

                alert.SetButton("YES", (c, ev) =>
                {
                    Process.KillProcess(Process.MyPid());
                });
                alert.SetButton2("NO", (c, ev) =>
                {
                    alert.Cancel();
                });
                alert.Show();
            };
        }

    }
}

