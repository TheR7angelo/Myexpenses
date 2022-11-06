using System;
using MyExpenses.Utils.Database;
using MyExpenses.Utils.Function;
using Xamarin.Forms.Xaml;

namespace MyExpenses.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class DisplayPaymentType
{
    private readonly BaseDisplay _baseDisplay;
    
    public DisplayPaymentType()
    {
        InitializeComponent();
        
        _baseDisplay = new BaseDisplay(StackLayoutPaymentType, ButtonAddNew, SqLite.GetAllPaymentType(), typeof(SqLite.PaymentClass));
    }

    private void ButtonAddPaymentType_OnClicked(object sender, EventArgs e) =>
        _baseDisplay.ButtonAdd_OnClicked(sender, e);
}