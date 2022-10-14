using MyExpenses.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace MyExpenses.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}