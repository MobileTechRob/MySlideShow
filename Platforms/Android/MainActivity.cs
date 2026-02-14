using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;

namespace MySlideShow
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, LaunchMode = LaunchMode.SingleTop, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {
            public static event Action<int, Result, Intent?>? ActivityResultReceived;

            protected override void OnActivityResult(int requestCode, Result resultCode, Intent? data)
            {
                base.OnActivityResult(requestCode, resultCode, data);
                ActivityResultReceived?.Invoke(requestCode, resultCode, data);
            }
    }

}
