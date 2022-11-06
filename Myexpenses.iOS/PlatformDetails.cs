using Foundation;
using MyExpenses.iOS;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(PlatformDetails))]

namespace MyExpenses.iOS
{
    public class PlatformDetails : IInterface
    {
        public string GetPlatformRoot()
        {
            throw new System.NotImplementedException();
        }

        public void Landscape()
        {
            UIDevice.CurrentDevice.SetValueForKey(new NSNumber((int)UIInterfaceOrientation.LandscapeLeft), new NSString("orientation"));
        }

        public void Portrait()
        {
            UIDevice.CurrentDevice.SetValueForKey(new NSNumber((int)UIInterfaceOrientation.Portrait), new NSString("orientation"));
        }
    }
}