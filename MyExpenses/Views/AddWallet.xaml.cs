using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MyExpenses.Utils.Database;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Shapes;
using Xamarin.Forms.Xaml;

namespace MyExpenses.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class AddWallet
{
    private ActiveAccount _previous;
    
    private readonly ObservableCollection<string> _data = new ();
    
    private List<SqLite.TColorsClass> _dataColor;
    private List<SqLite.TImageClass> _dataImage;
    private List<SqLite.TWalletType> _walletTypes = new();

    private readonly Random _random = new();
    
    public AddWallet(ActiveAccount previous)
    {
        _previous = previous;
        
        InitializeComponent();
        FillWalletType(); 
        FillComboColor();
        FillComboImage();
        //todo finir les inputs et récupérer les données
    }

    private void FillComboImage()
    {
        _dataImage = SqLite.GetAllImages().OrderBy(s => s.Name).ToList();
        PickerImage.ItemsSource = _dataImage.Select(image => image.Name).ToList();
        PickerImage.SelectedIndex = _random.Next(0, _dataImage.Count - 1);
    }
    
    private void FillComboColor()
    {
        _dataColor = SqLite.GetAllColor().OrderBy(s => s.Nom).ToList();
        PickerColor.ItemsSource = _dataColor.Select(color => color.Nom).ToList();
        PickerColor.SelectedIndex = _random.Next(0, _dataColor.Count - 1);
    }

    private async void FillWalletType() //List of Countries  
    {  
        try
        {
            _walletTypes = SqLite.GetAllWalletType();
            foreach (var typeWallets in _walletTypes) _data.Add(typeWallets.Name);
        }  
        catch (Exception ex)  
        {  
            await DisplayAlert("Error", $"{ex}", "Ok");  
        }  
    }
    
    private void SearchBar_OnTextChanged(object sender, TextChangedEventArgs e)  
    {  
        CountryListView.IsVisible = true;  
        CountryListView.BeginRefresh();  
  
        try  
        {  
            var dataEmpty = _data.Where(i => i.ToLower().Contains(e.NewTextValue.ToLower()));  
  
            if (string.IsNullOrWhiteSpace(e.NewTextValue))  
                CountryListView.IsVisible = false;  
            else if (dataEmpty.Max().Length == 0)  
                CountryListView.IsVisible = false;  
            else  
                CountryListView.ItemsSource = _data.Where(i => i.ToLower().Contains(e.NewTextValue.ToLower()));  
        }  
        catch (Exception)  
        {  
            CountryListView.IsVisible = false;  
  
        }  
        CountryListView.EndRefresh();  
  
    } 
    
    private void ListView_OnItemTapped(object sender, ItemTappedEventArgs e)  
    {
        EditorWalletType.Text =  e.Item as string;
        CountryListView.IsVisible = false;  
  
        ((ListView)sender).SelectedItem = null;  
    } 
    
    private void ButtonValid_OnClicked(object sender, EventArgs e)
    {
        var walletName = EditorName.Text;
        var typeName = EditorWalletType.Text;
        var colorName = PickerColor.SelectedItem as string;
        
        var walletStart = EntryStartSolde.Text is null ? 0 :
            decimal.Round(decimal.Parse(EntryStartSolde.Text.Replace(',', '.')), 2, MidpointRounding.AwayFromZero);

        var type = _walletTypes.Where(s => s.Name.Equals(typeName)).ToList()[0];
        var color = _dataColor.Where(s => s.Nom.Equals(colorName)).ToList()[0];

        type ??= new SqLite.TWalletType { Name = typeName }.InsertWalletType();

        var wallet = new SqLite.TWalletClass
        {
            Name = walletName,
            Color = color.Id,
            Image = 0,
            TypeCompteFk = type.Id
        }.InsertWallet();

        new SqLite.THistoriqueClass
        {
            Order = "Init",
            WalletFk = wallet.Id,
            Montant = walletStart,
            date = DateTime.Now.ToString("yyyy-MM-dd"),
        }.InsertHistorique();
        
        _previous.DisplayWallet();
        Navigation.PopAsync();
    }

    private void PickerColor_OnSelectedItemChanged(object sender, EventArgs eventArgs)
    {
        var combo = sender as Picker;
        var colorName = combo!.SelectedItem as string;
        var colorValue = colorName.GetColorHex();
        FrameColor.BackgroundColor = Color.FromHex(colorValue);

    }

    private void PickerImage_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        var combo = sender as Picker;
        var imageName = combo!.SelectedItem as string;
        var image = Utils.Ressources.Images.GetImage(imageName);
        

        // PathImage.Data = image.Data;
    }

    private void SkCanvasView_OnPaintSurface(object sender, SKPaintSurfaceEventArgs e)
    {
        var info = e.Info;
        var surface = e.Surface;
        var canvas = surface.Canvas;
        
        var imageName = PickerImage.SelectedItem as string;
        var image = Utils.Ressources.Images.GetImage(imageName);

        // todo a fini xD
        canvas.Clear();
        // // var path = SKPath.ParseSvgPathData("M430.545,232.727c-26.764,0-51.2,12.8-65.164,33.745C353.745,260.655,340.945,256,325.818,256\n\tc-40.727,0-74.473,30.255-80.291,69.818h243.2c11.636,0,20.945-8.145,23.273-19.782\n\tC507.345,265.309,472.436,232.727,430.545,232.727z");
        var pathStyle = new SKPaint()
        {
            Style = SKPaintStyle.Stroke,
            Color = SKColors.Brown,
            StrokeWidth = 10
        };
        //
        // //https://stackoverflow.com/questions/50288603/dynamically-draw-lines-with-skiasharp-in-xamarin
        //
        // canvas.DrawLine(0, 0, 2, 50, pathStyle);
        canvas.DrawBitmap(image, 0, 0);
    }
}