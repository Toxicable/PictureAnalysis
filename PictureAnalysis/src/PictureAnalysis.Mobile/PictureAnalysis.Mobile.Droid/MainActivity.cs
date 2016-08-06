using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using PictureAnalysis.Mobile;
using Android.Content;
using Android.Provider;

namespace PictureAnalysis.Mobile.Droid
{
    [Activity(Label = "PictureAnalysis.Mobile", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
    {
        private static readonly string fmcDirectory =
           Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryPictures).Path;
        static readonly Java.IO.File file = new Java.IO.File(fmcDirectory, "origional.jpg");


        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());

            var app = Xamarin.Forms.Application.Current as App;

            app.ShouldTakePicture += () =>
            {
                var intent = new Intent(MediaStore.ActionImageCapture);
                intent.PutExtra(MediaStore.ExtraOutput, Android.Net.Uri.FromFile(file));

                StartActivityForResult(intent, 0);
            };

        }

        override protected void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            var compressedPath = fmcDirectory + "compressed.jpg";
            var fileContent = ImageProcessing.CompressImage(file.Path, compressedPath);

            (Xamarin.Forms.Application.Current as App).ShowImage(file.Path, fileContent);
        }
    }
}

