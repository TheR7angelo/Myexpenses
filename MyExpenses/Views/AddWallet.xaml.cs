using System;
using System.Collections.Generic;
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
        var i = new List<string>();
        foreach (var color in colors)
        {
            
            // var la = new Label { BackgroundColor = Color.FromHex(color.Value), Text = color.Nom};
            i.Add(color.Nom);
        }

        ComboBoxColor.ItemsSource = i;
        // todo à finir marche po cette merde
    }
    
    private void Button_OnClicked(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }
}