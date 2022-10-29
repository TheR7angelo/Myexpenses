using System;
using System.Collections.Generic;
using MyExpenses.Utils.Database;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyExpenses.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class DisplayStore
{
    private static readonly Style RdAddressStyle = Utils.Ressources.Style.RadioButton.GetStyle("rdAddress");
    
    private static List<SqLite.TLieuClass> _dataStore;
    public DisplayStore()
    {
        InitializeComponent();

        FillAddress();
    }

    private void FillAddress()
    {
        _dataStore = SqLite.GetAllLieu();
        foreach (var store in _dataStore)
        {
            var rdButton = new RadioButton
            {
                // Style = RdAddress,
                Content = store.Name,
                BindingContext = store
            };
            
            StackLayoutAddress.Children.Add(rdButton);
        }

        var btn = new Button
        {
            Text = "Add"
        };
        btn.Clicked += BtnOnClicked;
        StackLayoutAddress.Children.Add(btn);
    }

    private void BtnOnClicked(object sender, EventArgs e) => Navigation.PushAsync(new AddAddress());
}