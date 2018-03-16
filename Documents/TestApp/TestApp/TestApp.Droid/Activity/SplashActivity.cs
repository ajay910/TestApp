using Android.App;
using Android.Content.PM;
using Android.OS;
using Ninject;
using TestApp.Database;
using TestApp.Droid.Utilities;

namespace TestApp.Droid
{
    [Activity(Label = "@string/app_name", MainLauncher = true, NoHistory = true, Theme = "@style/Theme.SplashScreen", ScreenOrientation = ScreenOrientation.SensorPortrait)]
    
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            FinishedLaunching();
        }

        public async void FinishedLaunching()
        {
           var databaseInitializer = Dependencies.Container.Get<DatabaseInitializer>();
            await databaseInitializer.Execute();
            StartActivity(typeof(UserListActivity));
        }
    }
}