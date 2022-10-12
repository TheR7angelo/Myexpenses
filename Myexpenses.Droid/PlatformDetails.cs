using MyExpenses.Droid;

[assembly: Xamarin.Forms.Dependency(typeof(PlatformDetails))]

namespace MyExpenses.Droid
{
    public class PlatformDetails : IInterface
    {
        public string GetPlatformRoot()
        {
            return Android.App.Application.Context.GetExternalFilesDir(null)?.AbsolutePath;
        }
    }
}