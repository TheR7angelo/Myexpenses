using System;
using System.Collections.ObjectModel;
using MyExpenses.Utils.Database;
using MyExpenses.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyExpenses.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class DisplaySubscription
{
    public DisplaySubscription()
    {
        InitializeComponent();
        BindingContext = new SubscriptionModel();
    }

    private void DataGrid_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        Console.WriteLine("hey");
    }

    private void DataGrid_OnRefreshing(object sender, EventArgs e)
    {
        //pass
    }
}