using System;
using MyExpenses.Utils;

namespace MyExpenses
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