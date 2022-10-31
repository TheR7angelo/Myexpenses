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
using MyExpenses.Utils.Function.WebApi;
using Xamarin.Essentials;
using Xamarin.Forms.Xaml;

namespace MyExpenses.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class AddAddress
{
    private static readonly Nominatim Nominatim = new();
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
        var position = new Position(location.Latitude, location.Longitude);
            
        MapView.MyLocationLayer.UpdateMyLocation(position);
        MapView.Navigator!.NavigateTo(new MPoint(smc.x, smc.y), MapView.Map.Resolutions[17]);

        UpdatePosition(position);
    }

    private static async Task<Location?> GetCurrentLocation()
    {
        try
        {
            var request = new GeolocationRequest(GeolocationAccuracy.Best);
            var cts = new CancellationToken();

            var location = await Geolocation.GetLocationAsync(request, cts);
            return location;
        }
        catch (Exception) 
        {
            return null;
        }
    }

    private void MapView_OnMapClicked(object sender, MapClickedEventArgs e)
    {
        UpdatePosition(e.Point);
    }

    private void UpdatePosition(Position position)
    {
        Task.Run(() =>
        {
            MapView.Pins.Clear();
            MapView.Pins.Add(new Pin(new MapView())
            {
                Position = position,
                Type = PinType.Pin
            });
        });

        Task.Run(() =>
        {
            var address = Nominatim.PointToAddress(position);
            if (address is null) return;
            EditorNums.Text = address.Value.address.house_number;
            EditorCityName.Text = address.Value.address.city;
            EditorCityPostal.Text = address.Value.address.postcode.ToString();
            EditorCityCountry.Text = address.Value.address.country;
            EditorCityCountryCode.Text = address.Value.address.country_code;
        });
    }
}