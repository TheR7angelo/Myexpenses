using System.Collections.Generic;
using MyExpenses.Utils.Database;
using Xamarin.Forms.Xaml;

namespace MyExpenses.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class DisplayMagasin
{
    private static List<SqLite.TLieuClass> _dataStore;
    public DisplayMagasin()
    {
        InitializeComponent();
        _dataStore = SqLite.GetAllLieu();
    }
}