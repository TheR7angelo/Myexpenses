using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MyExpenses.Utils.Database;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyExpenses.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class AddWallet
{
    private readonly ActiveAccount _previous;
    
    private readonly ObservableCollection<string> _data = new ();
    
    private readonly List<SqLite.ColorsClass> _dataColor;
    private readonly List<SqLite.ImageClass> _dataImage;
    private List<SqLite.WalletType> _walletTypes = new();

    private readonly Random _random = new();
    
    public AddWallet(ActiveAccount previous)
    {
        _previous = previous;
        _dataImage = SqLite.GetAllImages().OrderBy(s => s.Name).ToList();
        _dataColor = SqLite.GetAllColor().OrderBy(s => s.Nom).ToList();
        
        
        InitializeComponent();
        
        FillWalletType(); 
        FillComboColor();
        FillComboImage();
    }

    private void FillComboImage()
    {
        // _dataImage = SqLite.GetAllImages().OrderBy(s => s.Name).ToList();
        PickerImage.ItemsSource = _dataImage.Select(image => image.Name).ToList();
        PickerImage.SelectedIndex = _random.Next(0, _dataImage.Count - 1);
    }
    
    private void FillComboColor()
    {
        // _dataColor = SqLite.GetAllColor().OrderBy(s => s.Nom).ToList();
        PickerColor.ItemsSource = _dataColor.Select(color => color.Nom).ToList();
        PickerColor.SelectedIndex = _random.Next(0, _dataColor.Count - 1);
    }

    private async void FillWalletType()
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

    private static IEnumerable<string> CheckError(string walletName, string typeName)
    {
        const string msg = "- Le {0} du portefeuille ne peut pas etre vide";
        var error = new List<string>();
        
        if (walletName.Equals(string.Empty)) error.Add(string.Format(msg, "nom"));
        if (typeName.Equals(string.Empty)) error.Add(string.Format(msg, "type"));

        return error;
    }

    private async void ButtonValid_OnClicked(object sender, EventArgs e)
    {
        var walletName = EditorName.Text ?? string.Empty;
        var typeName = EditorWalletType.Text ?? string.Empty;

        var check = CheckError(walletName, typeName).ToList();
        
        if (!check.Count.Equals(0))
        {
            var msg = string.Join(Environment.NewLine, check);
            await DisplayAlert("Alert", msg, "OK");
            return;
        }

        var walletStart = EntryStartSolde.Text is null ? 0 :
            decimal.Round(decimal.Parse(EntryStartSolde.Text.Replace(',', '.')), 2, MidpointRounding.AwayFromZero);

        var lstType = _walletTypes.Where(s => s.Name.Equals(typeName)).ToList();
        
        var type = lstType.Count.Equals(0) ? new SqLite.WalletType { Name = typeName }.InsertWalletType() : lstType[0];

        var color = FrameColor.BindingContext as SqLite.ColorsClass;
        var image = ImageLogo.BindingContext as SqLite.ImageClass;

        var wallet = new SqLite.WalletClass
        {
            Name = walletName,
            Color = color!.Id,
            Image = image!.Id,
            TypeCompteFk = type.Id
        }.InsertWallet();

        new SqLite.HistoriqueClass
        {
            Order = "Init",
            WalletFk = wallet.Id,
            Montant = walletStart,
            Date = DateTime.Now.ToString("yyyy-MM-dd"),
        }.InsertHistorique();
        
        _previous.DisplayWallet();
        await Navigation.PopAsync();
    }

    private void PickerColor_OnSelectedItemChanged(object sender, EventArgs eventArgs)
    {
        var combo = sender as Picker;
        var colorName = combo!.SelectedItem as string;

        var color = _dataColor.Where(s => s.Nom.Equals(colorName)).ToList()[0];
        
        FrameColor.BackgroundColor = Color.FromHex(color.Value);
        FrameColor.BindingContext = color;
    }

    private void PickerImage_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        // SkCanvasView.InvalidateSurface();
        SetImage();
    }

    // private void SkCanvasView_OnPaintSurface(object sender, SKPaintSurfaceEventArgs e)
    // {
    //     var info = e.Info;
    //     var surface = e.Surface;
    //     var canvas = surface.Canvas;
    //     
    //     var imageName = PickerImage.SelectedItem as string;
    //     var img = _dataImage.Where(s => s.Name.Equals(imageName)).ToList()[0];
    //     var bitmap = Utils.Ressources.Images.GetSkBitmap(imageName);
    //     
    //     canvas.Clear();
    //     var resize = bitmap.Resize(info, SKFilterQuality.High);
    //     
    //     var x = (info.Width - resize.Width) / 2f;
    //     var y = (info.Height - resize.Height) / 2f;
    //     
    //     canvas.DrawBitmap(resize, x, y);
    //     SkCanvasView.BindingContext = img;
    // }

    private void SetImage()
    {

        var imageName = PickerImage.SelectedItem as string;
        var img = _dataImage.Where(s => s.Name.Equals(imageName)).ToList();

        if (img.Count.Equals(0)) return;

        var image = Utils.Ressources.Images.GetImageSource(imageName!);

        ImageLogo.Source = image;
        ImageLogo.BindingContext = img[0];
    }

    private void Button_OnClicked(object sender, EventArgs e)
    {
        SqLite.RefreshImage(); 
        FillComboImage();
    }
}