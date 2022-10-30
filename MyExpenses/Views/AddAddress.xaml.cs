using System;
using System.Threading;
using System.Threading.Tasks;
using Mapsui;
using Mapsui.Extensions;
using Mapsui.Projections;
using Mapsui.Tiling;
using Mapsui.UI.Forms;
using Mapsui.Widgets;
using Mapsui.Widgets.ScaleBar;
using Xamarin.Essentials;
using Xamarin.Forms.Xaml;
using Map = Xamarin.Essentials.Map;

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
        MapView.Map = map;
        
        var location = await GetCurrentLocation();
        if (location is null) return;
        var smc = SphericalMercator.FromLonLat(location.Longitude, location.Latitude);
            
        MapView.MyLocationLayer.UpdateMyLocation(new Position(location.Latitude, location.Longitude));

        MapView.Navigator!.NavigateTo(new MPoint(smc.x, smc.y), MapView.Map.Resolutions[17]);

    }

    private static async Task<Location> GetCurrentLocation()
    {
        var request = new GeolocationRequest(GeolocationAccuracy.Best);
        var cts = new CancellationToken();

        var location = await Geolocation.GetLocationAsync(request, cts);

        return location;
    }

    private void MapView_OnMapClicked(object sender, MapClickedEventArgs e)
    {
        MapView.Pins.Clear();
        var position = e.Point;
        
        MapView.Pins.Add(new Pin(new MapView())
        {
            Position = position,
            Type = PinType.Pin
        });
        
        Console.WriteLine("hey");
    }
}