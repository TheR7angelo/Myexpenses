using System.Threading;
using System.Threading.Tasks;
using Mapsui;
using Mapsui.Extensions;
using Mapsui.Tiling;
using Mapsui.Widgets;
using Mapsui.Widgets.ScaleBar;
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

        Ui();
    }

    private async void Ui()
    {
        var map = new Mapsui.Map { CRS = "EPSG:4326" };
        var tileLayer = OpenStreetMap.CreateTileLayer();
        map.Layers.Add(tileLayer);

        map.Widgets.Add(new ScaleBarWidget(map)
        {
            TextAlignment = Alignment.Center,
            HorizontalAlignment = HorizontalAlignment.Left,
            VerticalAlignment = VerticalAlignment.Bottom
        });

        var location = await GetCurrentLocation();
        //todo point centrale ?
        map.Home = n => n.NavigateTo(new MPoint(location.Longitude, location.Latitude), 10);

        MapView.Map = map;
    }

    private static async Task<Location> GetCurrentLocation()
    {
        var request = new GeolocationRequest(GeolocationAccuracy.Best);
        var cts = new CancellationToken();

        var location = await Geolocation.GetLocationAsync(request, cts);

        return location;
    }
}