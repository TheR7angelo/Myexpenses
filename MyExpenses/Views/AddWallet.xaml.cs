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
    private ActiveAccount _previous;
    
    private readonly ObservableCollection<string> _data = new ();
    
    private List<SqLite.TColorsClass> _dataColor;
    private List<SqLite.TWalletType> _walletTypes = new();

    private readonly Random _random = new();
    
    public AddWallet(ActiveAccount previous)
    {
        _previous = previous;
        
        InitializeComponent();
        FillWalletType(); 
        FillComboColor();
        //todo finir les inputs et récupérer les données
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

        type ??= SqLite.InsertWalletType(new SqLite.TWalletType { Name = typeName });

        SqLite.InsertWallet(new SqLite.TWalletClass
        {
            Name = walletName,
            Color = color.Id,
            Image = 0,
            TypeCompteFk = type.Id
        });
        
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
}