using Xamarin.Forms;

namespace MyExpenses.Utils.Database;

public static partial class SqLite
{
    private static readonly string Root = DependencyService.Get<IInterface>().GetPlatformRoot();

}