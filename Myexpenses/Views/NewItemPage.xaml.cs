using MyExpenses.Models;
using MyExpenses.ViewModels;
using Xamarin.Forms;

namespace MyExpenses.Views
{
    public partial class NewItemPage : ContentPage
    {
        public Item Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();
            BindingContext = new NewItemViewModel();
        }
    }
}