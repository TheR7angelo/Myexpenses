using System;
using MyExpenses.Utils.Database;
using MyExpenses.Utils.Function;
using Xamarin.Forms.Xaml;

namespace MyExpenses.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class DisplayWalletType
{
    private readonly BaseDisplay _baseDisplay;
    
    public DisplayWalletType()
    {
        InitializeComponent();
        
        _baseDisplay = new BaseDisplay(StackLayoutAccountType, ButtonAddNew, SqLite.GetAllWalletType(), typeof(SqLite.WalletTypeClass));
    }
    
    private void ButtonAddWalletType_OnClicked(object sender, EventArgs e) => _baseDisplay.ButtonAdd_OnClicked(sender, e);
}