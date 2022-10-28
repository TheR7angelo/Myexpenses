using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyExpenses.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class Home
{
    public static string Root;
    public static string Db;
    public static List<string> Dbs = new ();
    public Home()
    {
        Root = DependencyService.Get<IInterface>().GetPlatformRoot();
        Db = Path.Combine(Root, "dbFiles");

        FindAccount();
        
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

    private static void FindAccount()
    {
        if (!Directory.Exists(Db)) return;
        Dbs = Directory.GetFiles(Db, "*.sqlite").Select(Path.GetFileNameWithoutExtension).ToList();
    }
    
    private async void SelectAccount()
    {
        if (Dbs.Count.Equals(0))
        {
            await DisplayAlert("Alert", "Aucun compte n'a étais crée merci d'en crée un", "OK");
            return;
        }
        // if (!Directory.Exists(Db)) await DisplayAlert("Alert", "Aucun compte n'a étais crée merci d'en crée un", "OK");
        await Navigation.PushAsync( new SelectedAccount());
    }
}