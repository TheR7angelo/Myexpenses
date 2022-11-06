using System;
using MyExpenses.Utils.Database;
using MyExpenses.Utils.Function;
using Xamarin.Forms.Xaml;

namespace MyExpenses.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class DisplayCategory
{
    private readonly BaseDisplay _baseDisplay;

    public DisplayCategory()
    {
        InitializeComponent();

        _baseDisplay = new BaseDisplay(StackLayoutCategory, ButtonAddNew, SqLite.GetAllCategory(), typeof(SqLite.CategoryClass));
    }
    
    private void ButtonAddCategory_OnClicked(object sender, EventArgs e) => _baseDisplay.ButtonAdd_OnClicked(sender, e);
}