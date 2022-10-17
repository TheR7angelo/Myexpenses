using Xamarin.Forms;

namespace MyExpenses.Utils.Database;

public static class Strucs
{
    public struct TCompte
    {
        public int Id;
        public string Name;
        public int TypeAccountFk;
        public SolidColorBrush SolidColorBrush;
        public ImageSource ImageSource;
    }
}