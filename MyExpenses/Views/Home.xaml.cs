using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyExpenses.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class Home
{
    public static string _root;
    public static string _db;
    public Home()
    {
        _root = DependencyService.Get<IInterface>().GetPlatformRoot();
        _db = Path.Combine(_root, "dbFiles");
        InitializeComponent();
    }

    private async void Button_OnClicked(object sender, EventArgs e)
    {
        var btn = sender as Button;
        switch (btn!.ClassId)
        {
            case "ButtonConnexion":
                await Navigation.PushAsync( new SelectedAccount());
                break;
            case "ButtonNewAccount":
                await Navigation.PushAsync(new AddAccount());
                break;
        }
    }
}