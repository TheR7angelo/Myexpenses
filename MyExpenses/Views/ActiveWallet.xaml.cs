using MyExpenses.Utils.Database;
using Xamarin.Forms.Xaml;

namespace MyExpenses.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class ActiveWallet
{
    public ActiveWallet(SqLite.VWalletClass wallet)
    {
        InitializeComponent();
        //todo à finir
    }
}