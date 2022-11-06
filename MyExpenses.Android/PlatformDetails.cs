using Android.App;
using Android.Content.PM;
using MyExpenses.Droid;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(PlatformDetails))]

namespace MyExpenses.Droid
{
    public class PlatformDetails : IInterface
    {
        public string GetPlatformRoot()
        {
            return Android.App.Application.Context.GetExternalFilesDir(null)?.AbsolutePath;
        }

        public void Landscape()
        {
            ((Activity)Forms.Context).RequestedOrientation = ScreenOrientation.Landscape;
        }

        public void Portrait()
        {
            ((Activity)Forms.Context).RequestedOrientation = ScreenOrientation.Portrait;
        }
    }
}