using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MyExpenses
{
    public partial class MainPage : ContentPage
    {
        private static string _root;
        public string ApplicationPath { get; } = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

        public MainPage()
        {
            _root = DependencyService.Get<IInterface>().GetPlatformRoot();
            InitializeComponent();
        }

        private async void Button_OnClicked(object sender, EventArgs e)
        {
            await TakePhotoAsync();
        }

        private async Task TakePhotoAsync()
        {
            var photo = await MediaPicker.CapturePhotoAsync();
            var path = await LoadPhotoAsync(photo);
            Console.WriteLine($"CapturePhotoAsync COMPLETED: {path}");
            LabelLocation.Text = path;
        }

        private static async Task<string> LoadPhotoAsync(FileResult photo)
        {

            if (photo is null) return string.Empty;

            
            
            var newFile = Path.Combine(_root, $"hey20{Path.GetExtension(photo.FileName)}");
            var stream = await photo.OpenReadAsync();
            var newStream = File.OpenWrite(newFile);

            await stream.CopyToAsync(newStream);
            return newFile;
        }
    }
}