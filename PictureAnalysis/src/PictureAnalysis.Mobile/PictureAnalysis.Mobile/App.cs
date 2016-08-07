using Newtonsoft.Json;
using PCLStorage;
using PictureAnalysis.Mobile.Services;
using PictureAnalysis.Mobile.Services.UserSettings;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PictureAnalysis.Mobile
{
    public class App : Application
    {
        private UserSettings _settings;

        private readonly Xamarin.Forms.Color _white = Xamarin.Forms.Color.White;
        private readonly Xamarin.Forms.Color _black = Xamarin.Forms.Color.Black;

        public App()
        {
            
            //MainPage = new MainPage1();
            //he root page of your application
            MainPage = new ContentPage
            {
                BackgroundColor = _white,
                Padding = new Thickness(0, Device.OS == TargetPlatform.iOS ? 20 : 0, 0, 0),
                Content = new StackLayout
                {

                    VerticalOptions = LayoutOptions.Center,
                    Children = {
                         new Button {
                            Text = "Toggle night mode",
                            Command = new Command( o => ToggleColorMode()),
                            BackgroundColor = Xamarin.Forms.Color.Silver
                        },
                        new Button {
                            Text = "Take a picture!",
                            Command = new Command( o => ShouldTakePicture()),
                            BackgroundColor = Xamarin.Forms.Color.Silver
                        },
                        image,
                        description

                    }
                }
                
                
            };
        }

        
        public void ToggleColorMode()
        {
            _settings.Settings.IsNightMode = !_settings.Settings.IsNightMode;
            if (IsNightMode())
            {
                MainPage.BackgroundColor = _white;
                description.TextColor = _black;
            }
            else
            {
                MainPage.BackgroundColor = _black;
                description.TextColor = _white;
            }
        }

        public async Task ShowImage(string filepath, byte[] imageBytes)
        {
            description.Text = "Loading...";
            image.Source = ImageSource.FromFile(filepath);

            using( var client = new PictureRestClient())
            {
                var analysisResult = await client.Post(imageBytes);
                
                description.Text = "It's " + analysisResult.Description.Captions.FirstOrDefault()?.Text;
            }

        }
        readonly Image image = new Image() { WidthRequest = 300 };

        public Label description = new Label() {
            HorizontalTextAlignment = TextAlignment.Center,
            TextColor = Xamarin.Forms.Color.Black };

        public event Action ShouldTakePicture = () => { };


        private bool IsNightMode()
        {
            return MainPage.BackgroundColor == _black;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
            _settings = new UserSettings();
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}

