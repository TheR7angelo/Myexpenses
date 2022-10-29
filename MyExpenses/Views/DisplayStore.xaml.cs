using System.Collections.Generic;
using MyExpenses.Utils.Database;
using Xamarin.Forms.Xaml;

namespace MyExpenses.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class DisplayStore
{
    private static List<SqLite.TLieuClass> _dataStore;
    public DisplayStore()
    {
        InitializeComponent();
        _dataStore = SqLite.GetAllLieu();
    }
}