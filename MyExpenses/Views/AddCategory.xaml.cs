using MyExpenses.Utils.Database;
using Xamarin.Forms.Xaml;

namespace MyExpenses.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class AddCategory
{
    private readonly DisplayCategory _displayCategory;
    
    public AddCategory(DisplayCategory previous, SqLite.CategoryClass? modify=null)
    {
        _displayCategory = previous;
        
        InitializeComponent();

        if (modify is null) return;
    }
}