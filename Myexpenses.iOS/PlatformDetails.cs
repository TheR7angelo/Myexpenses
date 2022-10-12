using MyExpenses.iOS;
[assembly: Xamarin.Forms.Dependency(typeof(PlatformDetails))]

namespace MyExpenses.iOS
{
    public class PlatformDetails : IInterface
    {
        public PlatformDetails()
        {
        }


        public string GetPlatformRoot()
        {
            throw new System.NotImplementedException();
        }
    }
}