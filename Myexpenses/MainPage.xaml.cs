using System;
using Myexpenses.Utils;
using Xamarin.Essentials;

namespace Myexpenses
{
    public partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            LabelLocation.Text = Permission.Camera() ? "gagner" : "perdu";
        }
        
        
    }
}