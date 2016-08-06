using PictureAnalysis.Mobile.Services;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PictureAnalysis.Mobile
{
    public class App : Application
    {
        public App()
        {
            //MainPage = new MainPage1();
            //he root page of your application
            MainPage = new ContentPage
            {
                BackgroundColor = Xamarin.Forms.Color.White,
                Padding = new Thickness(0, Device.OS == TargetPlatform.iOS ? 20 : 0, 0, 0),
                Content = new StackLayout
                {

                    VerticalOptions = LayoutOptions.Center,
                    Children = {
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
        readonly Image image = new Image() { WidthRequest = 200 };

        public Label description = new Label() {
            HorizontalTextAlignment = TextAlignment.Center,
            TextColor = Xamarin.Forms.Color.Black };

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

