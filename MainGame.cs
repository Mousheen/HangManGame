using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.OS;
using Android.Widget;
using System.IO;
using SQLite;
using Android.Content.PM;

namespace HangManGame
{

    [Activity(Label = "MainGame", ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainGame : Activity
    {
        //path for the database
        string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "dbPlayer.db3");
        string dbPath2 = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "dbWordList.db3");
        char buttonPressed; //stores value of the button pressed
        int chances = 0;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Game);

            var db = new SQLiteConnection(dbPath); //adds connection to the database
            var table = db.Table<PlayerDatabase>(); //gets the table from the database

            var db2 = new SQLiteConnection(dbPath2);
            var table2 = db2.Table<WordsListDatabase>();

            TextView txtWord = FindViewById<TextView>(Resource.Id.txtWord);
            TextView txtWelcome = FindViewById<TextView>(Resource.Id.txtWelcome);
            TextView txtScore = FindViewById<TextView>(Resource.Id.txtScore);
            Button btnMainMenu = FindViewById<Button>(Resource.Id.btnMainMenu);
            Button btnReset = FindViewById<Button>(Resource.Id.btnReset);
            ImageView images = FindViewById<ImageView>(Resource.Id.ivHang);

            Button A = FindViewById<Button>(Resource.Id.bA);
            Button B = FindViewById<Button>(Resource.Id.bB);
            Button C = FindViewById<Button>(Resource.Id.bC);
            Button D = FindViewById<Button>(Resource.Id.bD);
            Button E = FindViewById<Button>(Resource.Id.bE);
            Button F = FindViewById<Button>(Resource.Id.bF);
            Button G = FindViewById<Button>(Resource.Id.bG);
            Button H = FindViewById<Button>(Resource.Id.bH);
            Button I = FindViewById<Button>(Resource.Id.bI);
            Button J = FindViewById<Button>(Resource.Id.bJ);
            Button K = FindViewById<Button>(Resource.Id.bK);
            Button L = FindViewById<Button>(Resource.Id.bL);
            Button M = FindViewById<Button>(Resource.Id.bM);
            Button N = FindViewById<Button>(Resource.Id.bN);
            Button O = FindViewById<Button>(Resource.Id.bO);
            Button P = FindViewById<Button>(Resource.Id.bP);
            Button Q = FindViewById<Button>(Resource.Id.bQ);
            Button R = FindViewById<Button>(Resource.Id.bR);
            Button S = FindViewById<Button>(Resource.Id.bS);
            Button T = FindViewById<Button>(Resource.Id.bT);
            Button U = FindViewById<Button>(Resource.Id.bU);
            Button V = FindViewById<Button>(Resource.Id.bV);
            Button W = FindViewById<Button>(Resource.Id.bW);
            Button X = FindViewById<Button>(Resource.Id.bX);
            Button Y = FindViewById<Button>(Resource.Id.bY);
            Button Z = FindViewById<Button>(Resource.Id.bZ);

            Random ran = new Random();

            AlertDialog.Builder dialog = new AlertDialog.Builder(this, AlertDialog.ThemeHoloDark);
            AlertDialog alert = dialog.Create();
            alert.SetCancelable(false);
            alert.SetTitle("GAME OVER");
            alert.SetMessage("Would You Like to Play Again?");

            PlayerDatabase Player = table.Last();
            txtWelcome.Text = "PLAYER:" + Player.PlayerName;
            txtScore.Text = "SCORE:" + Player.Scores;

            WordsListDatabase Word = table2.ElementAt(ran.Next(1, 25)); //randomly picks a word from the table

            string wordHolder = Word.Words; //stores the word

            var array1 = new char[1]; //creates an array

            Array.Resize<char>(ref array1, wordHolder.Length); //resizes the array to the size of the word 
            array1 = array1.Select(i => '-').ToArray(); //replaces the letters with '-'
            txtWord.Text = new string(array1); //shows the array in textview

            string temp = new string(array1);

            var buttonList = new List<Button>
            {
                A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z
            };

            void Winner()
            {
                if (temp.IndexOf('-') == -1 ) //checks if the string has any blank letters
                {
                    alert.SetTitle("CONGRATULATIONS");
                    alert.SetMessage("Play Again?");
                    alert.SetButton("YES", (c, ev) =>
                    {
                        Player.Scores += 100;
                        db.Update(Player);

                        StartActivity(typeof(MainGame));
                        Finish();
                    });

                    alert.SetButton2("NO", (c, ev) =>
                    {
                        StartActivity(typeof(MainActivity));
                        Finish();
                    });
                    alert.Show();
                }
            }

            for (var i = 0; i < buttonList.Count; i++) //cycles through all the buttons to find out which one is pressed
            {
                var button = buttonList[i];
                button.Tag = i; //stores buttons value from its tag

                button.Click += (sender, e) =>
                {
                    button.Enabled = false; //disables that button
                    buttonPressed = Convert.ToChar(button.Text);

                    for (int l = 0; l < wordHolder.Length; l++) //checks for the length of the random word
                    {
                        if (wordHolder[l] == buttonPressed)
                        {
                            //Update the correctly guessed letters, (using value of i to determine which letter to make visible.)
                            int indexNum = wordHolder.IndexOf(buttonPressed,l);

                            StringBuilder sb = new StringBuilder(temp); //builds the string when the buttonPressed is correct
                            sb[indexNum] = buttonPressed;
                            temp = sb.ToString();
                        }
                    }
                    txtWord.Text = temp;
                    if (wordHolder.Contains(buttonPressed)) { } //checks for false buttons pressed
                    else //if wrong button is pressed than chances of loosing goes up
                    {
                        chances++;
                        switch (chances)
                        {
                            case 0: // number of tries and consequences before player looses
                                images.SetImageResource(Resource.Drawable.Hang0);
                                break;
                            case 1:
                                images.SetImageResource(Resource.Drawable.Hang1);
                                break;
                            case 2:
                                images.SetImageResource(Resource.Drawable.Hang2);
                                break;
                            case 3:
                                images.SetImageResource(Resource.Drawable.Hang3);
                                break;
                            case 4:
                                images.SetImageResource(Resource.Drawable.Hang4);
                                break;
                            case 5:
                                images.SetImageResource(Resource.Drawable.Hang5);
                                break;
                            case 6:
                                images.SetImageResource(Resource.Drawable.Hang6);
                                Toast.MakeText(this, "LAST CHANCE!", ToastLength.Short).Show();
                                break;
                            case 7: //player looses if they cant guess on the last try
                                images.SetImageResource(Resource.Drawable.Hang7);

                                alert.SetButton("YES", (c, ev) =>
                                {
                                    StartActivity(typeof(MainGame));
                                    Finish();
                                });
                                alert.SetButton2("MAIN MENU", (c, ev) =>
                                {
                                    StartActivity(typeof(MainActivity));
                                    Finish();
                                });
                                alert.Show();
                                break;
                        }
                    }
                    Winner();
                };
            }

            btnReset.Click += delegate
            {
                alert.SetTitle("RESET GAME");
                alert.SetMessage("Are You Sure?");

                alert.SetButton("YES", (c, ev) =>
                {
                    StartActivity(typeof(MainGame));
                    Finish();
                });
                alert.SetButton2("NO", (c, ev) =>
                {
                    alert.Cancel();
                });
                alert.Show();
            };

            btnMainMenu.Click += delegate
            {
                alert.SetTitle("MAIN MENU");
                alert.SetMessage("Are You Sure?");

                alert.SetButton("YES", (c, ev) =>
                {
                    StartActivity(typeof(MainActivity));
                    Finish();
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