using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Mapsui;
using Mapsui.Extensions;
using Mapsui.Tiling;
using Mapsui.UI.Forms;
using Mapsui.Widgets;
using Mapsui.Widgets.ScaleBar;
using MyExpenses.Utils.Database;
using MyExpenses.Utils.Function;
using MyExpenses.Utils.Function.WebApi;
using Xamarin.Forms.Xaml;

namespace MyExpenses.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class AddAddress
{
    private readonly DisplayStore _displayStore;

    private const string Crs = "EPSG:4326";
    private const string UserAgent = "C#";
    private static readonly Nominatim Nominatim = new(UserAgent);

    private static SqLite.LieuClass? _modify; 
    public AddAddress(DisplayStore previous, SqLite.LieuClass? modify=null)
    {
        _displayStore = previous;
        
        InitializeComponent();
        InitializeMap();

        if (modify is null) return;
        _modify = modify;
        Modify(modify);
    }

    #region Function

    private async void InitializeMap()
    {
        var map = new Map { CRS = Crs };
        var tileLayer = OpenStreetMap.CreateTileLayer(UserAgent);
        map.Layers.Add(tileLayer);

        map.Widgets.Add(new ScaleBarWidget(map)
        {
            TextAlignment = Alignment.Center,
            HorizontalAlignment = HorizontalAlignment.Left,
            VerticalAlignment = VerticalAlignment.Bottom
        });
        MapView.Map = map;
        
        var location = await Essential.GetCurrentLocation();
        if (location is null) return;
        var position = new Position(location.Latitude, location.Longitude);
            
        MapView.MyLocationLayer.UpdateMyLocation(position);
        ZoomToPosition(position, MapView.Map.Resolutions[17]);
    }

    private void Modify(SqLite.LieuClass lieuClass)
    {
        EditorName.Text = lieuClass.Name;
        EditorNums.Text = lieuClass.Nums;
        EditorRoad.Text = lieuClass.Rue;
        EditorCityName.Text = lieuClass.City;
        EditorCityPostal.Text = lieuClass.Postal;
        EditorCityCountry.Text = lieuClass.Pays;
        EditorCityCountryCode.Text = lieuClass.CountryCode;
        
        EditorLongitude.Text = lieuClass.Longitude.ToString();

        if (lieuClass.Latitude is not null) EditorLatitude.Text = lieuClass.Latitude.ToString();
        if (lieuClass.Longitude is not null) EditorLongitude.Text = lieuClass.Longitude.ToString();

        if (lieuClass.Latitude is null || lieuClass.Longitude is null) return;
        
        ZoomToPosition(new Position((double)lieuClass.Latitude, (double)lieuClass.Longitude), MapView.Map!.Resolutions[17]);
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

        EditorLatitude.Text = position.Latitude.ToString(CultureInfo.InvariantCulture);
        EditorLongitude.Text = position.Longitude.ToString(CultureInfo.InvariantCulture);

        var nums = string.Empty;
        var road = string.Empty;
        var cityName = string.Empty;
        var postal = string.Empty;
        var country = string.Empty;
        var countryCode = string.Empty;
        
        var address = Nominatim.PointToNominatim(position);
        if (address is not null)
        {
            nums = address.Value.address.house_number;
            road = address.Value.address.road;
            cityName = address.Value.address.city ?? address.Value.address.village;
            postal = address.Value.address.postcode.ToString();
            country = address.Value.address.country;
            countryCode = address.Value.address.country_code;
        }

        EditorNums.Text = nums;
        EditorRoad.Text = road;
        EditorCityName.Text = cityName;
        EditorCityPostal.Text = postal;
        EditorCityCountry.Text = country;
        EditorCityCountryCode.Text = countryCode;
    }

    private void UpdateZoom(Position position)
    {
        UpdatePosition(position);
        ZoomToPosition(position, MapView.Map!.Resolutions[17]);
    }

    private void ZoomToPosition(Position position, double resolution) => MapView.Navigator!.NavigateTo(position.ToMapsui(), resolution);

    #endregion

    #region Actions

    private void EditorAddress_OnCompleted(object sender, EventArgs e)
    {
        var nums = ParseToEmpty(EditorNums.Text);
        var road = ParseToEmpty(EditorRoad.Text);
        var cityName = ParseToEmpty(EditorCityName.Text);
        var postal = ParseToEmpty(EditorCityPostal.Text);
        var country =  ParseToEmpty(EditorCityCountry.Text);

        if (nums.Equals(string.Empty) || road.Equals(string.Empty) || cityName.Equals(string.Empty) || 
            postal.Equals(string.Empty) || country.Equals(string.Empty)) return;
        
        var address = Nominatim.AddressToNominatim($"{nums} {road}, {postal} {cityName} {country}");
        if (address is null) return;
        if (address.Count.Equals(0)) return;
        var position = new Position(address[0].lat, address[0].lon);
        
        UpdateZoom(position);
    }

    private void EditorCoord_OnCompleted(object sender, EventArgs e)
    {
        var latitude = ParseToCoord(EditorLatitude.Text);
        var longitude = ParseToCoord(EditorLongitude.Text);

        if (latitude is null || longitude is null) return;

        var position = new Position((double)latitude, (double)longitude);
        UpdateZoom(position);
    }
    
    private async void ButtonValid_OnClicked(object sender, EventArgs e)
    {
        var name = ParseToEmpty(EditorName.Text);
        var nums = ParseToEmpty(EditorNums.Text);
        var road = ParseToEmpty(EditorRoad.Text);
        var cityName = ParseToEmpty(EditorCityName.Text);
        var postal = ParseToEmpty(EditorCityPostal.Text);
        var country = ParseToEmpty(EditorCityCountry.Text);
        var countryCode = ParseToEmpty(EditorCityCountryCode.Text);
        var latitude = ParseToCoord(EditorLatitude.Text);
        var longitude = ParseToCoord(EditorLongitude.Text);

        var error = true;
        var msg = "Le lieu à étais ajouté";
        
        if (name.Equals(string.Empty)) msg = "Le nom du lieu ne peut pas etre vide";
        else if (!DisplayStore.DataStore.Where(s => s.Name.Equals(name)).ToList().Count.Equals(0))
            msg = "Le nom est déja utiliser";
        else error = false;

        if (_modify is not null && error.Equals(true)) error = false;

        if (error)
        {
            await DisplayAlert("Erreur", msg, "Ok");
            return;
        }

        if (_modify is null)
        {
            var insert = new SqLite.LieuClass
            {
                Name = name,
                Nums = nums,
                Rue = road,
                Postal = postal,
                City = cityName,
                Pays = country,
                CountryCode = countryCode,
                Latitude = latitude,
                Longitude = longitude
            };
            insert.Insert();

            await DisplayAlert("Réussi", msg, "Ok");
            DisplayStore.DataStore.Add(insert);
            _displayStore.AddDisplayStore(insert);
            _modify = insert;
        }
        else
        {
            msg = "Mise à jour réussi";
            
            _modify.Name = name;
            _modify.Nums = nums;
            _modify.Rue = road;
            _modify.Postal = postal;
            _modify.City = cityName;
            _modify.Pays = country;
            _modify.CountryCode = countryCode;
            _modify.Latitude = latitude;
            _modify.Longitude = longitude;
            _modify.DateAdd = DateTime.UtcNow;

            _modify.UpdateLieuClass();
            
            await DisplayAlert("Réussi", msg, "Ok");
            
            var index = DisplayStore.DataStore.FindIndex(s => s.Id.Equals(_modify.Id));
            DisplayStore.DataStore[index] = _modify;
            _displayStore.AddDisplayStoreReset();
        }
        

    }

    private void MapView_OnMapClicked(object sender, MapClickedEventArgs e) => UpdatePosition(e.Point);
    
    #endregion

    #region Parser

    private static string ParseToEmpty(string? str) => str ?? string.Empty;

    private static double? ParseToCoord(string coord)
    {
        var value = ParseToEmpty(coord).Replace(',', '.');
        if (value.Equals(string.Empty)) return null;
        return double.Parse(value);
    } 

    #endregion
}