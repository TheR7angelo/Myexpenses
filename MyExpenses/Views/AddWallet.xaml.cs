using System;
using System.Linq;
using MyExpenses.Utils.Database;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyExpenses.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class AddWallet
{
    public AddWallet()
    {
        InitializeComponent();

        FillComboColor();
        //todo finir les inputs et récupérer les données
    }

    private void FillComboColor()
    {
        var colors = SqLite.GetAllColor();
        ComboBoxColor.ItemsSource = colors.OrderBy(s => s.Nom).Select(color => color.Nom).ToList();
        
        // todo à finir marche po cette merde
    }
    
    private void Button_OnClicked(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    private void ComboBoxColor_OnSelectedItemChanged(object sender, EventArgs eventArgs)
    {
        var combo = sender as Picker;
        var colorName = combo!.SelectedItem as string;
        var colorValue = colorName.GetColorHex();
        FrameColor.BackgroundColor = Color.FromHex(colorValue);

    }
}