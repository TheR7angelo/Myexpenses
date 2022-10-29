using System.Threading;
using Xamarin.Essentials;
using Xamarin.Forms.Xaml;

namespace MyExpenses.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class AddAddress
{
    public AddAddress()
    {
        //todo finir
        InitializeComponent();
        Lo();
    }

    private async void Lo()
    {
        string str;
        
        var request = new GeolocationRequest(GeolocationAccuracy.Best);
        var cts = new CancellationToken();

        var location = await Geolocation.GetLocationAsync(request, cts);
        
        str = location is not null ? $"{location.Latitude}, {location.Longitude}" : "nop";

        LabelTest.Text = str;
    }
}