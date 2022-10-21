using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyExpenses.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class Home
{
    public static string Root;
    public static string Db;
    public Home()
    {
        Root = DependencyService.Get<IInterface>().GetPlatformRoot();
        Db = Path.Combine(Root, "dbFiles");

        InitializeComponent();
    }

    private async void Button_OnClicked(object sender, EventArgs e)
    {
        var btn = sender as Button;
        switch (btn!.ClassId)
        {
            case "ButtonConnexion":
                SelectAccount();
                break;
            case "ButtonNewAccount":
                await Navigation.PushAsync(new AddAccount());
                break;
        }
    }

    private async void SelectAccount()
    {
        if (!Directory.Exists(Db)) await DisplayAlert("Alert", "Aucun compte n'a étais crée merci d'en crée un", "OK");
        
        await Navigation.PushAsync( new SelectedAccount());
    }
}