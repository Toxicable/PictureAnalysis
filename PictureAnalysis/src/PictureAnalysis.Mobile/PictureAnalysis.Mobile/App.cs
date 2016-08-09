using PictureAnalysis.Mobile.Services;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PictureAnalysis.Mobile
{
    public class App : Application
    {

        private static readonly Xamarin.Forms.Color _white = Xamarin.Forms.Color.White;
        private static readonly Xamarin.Forms.Color _black = Xamarin.Forms.Color.Black;

        public App()
        {
            

            MainPage = new ContentPage
            {
                BackgroundColor = _black,
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

        
        public async Task ToggleColorMode()
        {
            if (MainPage.BackgroundColor == Xamarin.Forms.Color.Black)
            {
                MainPage.BackgroundColor = Xamarin.Forms.Color.White;
                description.TextColor = Xamarin.Forms.Color.Black;                
            }
            else
            {
                MainPage.BackgroundColor = Xamarin.Forms.Color.Black;
                description.TextColor = Xamarin.Forms.Color.White;                
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

        public Label description = new Label()
        {
            HorizontalTextAlignment = TextAlignment.Center,
            TextColor = _black,
            FontSize = 20
        };

        public event Action ShouldTakePicture = () => { };

        protected override void OnStart()
        {
            // Handle when your app starts
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

