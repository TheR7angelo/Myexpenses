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
    private readonly ObservableCollection<string> _data = new ();
    private List<SqLite.TWalletType> _walletTypes = new();

    public AddWallet()
    {
        InitializeComponent();
        FillWalletType(); 
        FillComboColor();
        //todo finir les inputs et récupérer les données
    }

    private void FillComboColor()
    {
        var colors = SqLite.GetAllColor();
        PickerColor.ItemsSource = colors.OrderBy(s => s.Nom).Select(color => color.Nom).ToList();
    }

    private async void FillWalletType() //List of Countries  
    {  
        try
        {
            _walletTypes = SqLite.GetAllWalletType();
            _walletTypes.Add(new SqLite.TWalletType
            {
                Id = 1,
                Nom = "test"
            });
            foreach (var typeWallets in _walletTypes) _data.Add(typeWallets.Nom);
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
        EditorWalletType.Text =  e.Item as string; ;  
        CountryListView.IsVisible = false;  
  
        ((ListView)sender).SelectedItem = null;  
    } 
    
    private void ButtonValid_OnClicked(object sender, EventArgs e)
    {
        var walletName = EditorName.Text;
        var walletType = EditorWalletType.Text;
        var walletStart = EntryStartSolde.Text is null ? 0 :
            decimal.Round(decimal.Parse(EntryStartSolde.Text.Replace(',', '.')), 2, MidpointRounding.AwayFromZero);

        //bug crash ici ?
        var color = PickerColor.SelectedItem.ToString();
        Console.WriteLine("hr");
    }

    private void PickerColor_OnSelectedItemChanged(object sender, EventArgs eventArgs)
    {
        var combo = sender as Picker;
        var colorName = combo!.SelectedItem as string;
        var colorValue = colorName.GetColorHex();
        FrameColor.BackgroundColor = Color.FromHex(colorValue);

    }
}