using MyExpenses.Utils.Database;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyExpenses.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class DisplaySubscription
{
    public DisplaySubscription()
    {
        InitializeComponent();

        var data = SqLite.GetAllSubscription();
        DataGrid.ItemsSource = data;
    }
}