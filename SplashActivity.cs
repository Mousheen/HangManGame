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
using Android.Support.V7.App;
using System.Threading.Tasks;
using Android.Content.PM;

namespace HangManGame
{
    [Activity(Theme = "@style/MyTheme.Splash", MainLauncher = true, Icon = "@drawable/Icon", NoHistory = true, ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        protected override void OnResume()
        {
            base.OnResume();
            
            Task startupWork = new Task(() =>
            {
                Task.Delay(3000);
            }); //defines the splash screen time

            startupWork.ContinueWith(t =>
            {
               StartActivity(new Intent(Application.Context, typeof(MainActivity))); //start mainactivity after splash screen
            }, TaskScheduler.FromCurrentSynchronizationContext());

            startupWork.Start();
        }
    }
}