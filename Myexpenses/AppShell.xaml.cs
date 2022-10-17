using MyExpenses.Views;
using Xamarin.Forms;

namespace MyExpenses
{
    public partial class AppShell
    {

        public AppShell()
        {
            InitializeComponent();

            ShellTest.Items.Add(new ShellSection{Title = "home", Items = { new ShellContent{Content = new Home()} }});
        }
    }
}
