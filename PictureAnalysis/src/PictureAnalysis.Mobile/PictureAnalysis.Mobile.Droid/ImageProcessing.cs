using System;
using Android.Graphics;

namespace PictureAnalysis.Mobile
{
    public class ImageProcessing
    {

        public static byte[] CompressImage(string inputPath, string outputPath)
        {
            Bitmap scaledBitmap = null;

            BitmapFactory.Options options = new BitmapFactory.Options();

            //by setting this field as true, the actual bitmap pixels are not loaded in the memory.
            //Just the bounds are loaded.
            //If you try the use the bitmap here, you will get null.
            options.InJustDecodeBounds = true;
            //loads on the bitmap
            Bitmap bmp = BitmapFactory.DecodeFile(inputPath, options);

            //assign the actual height and width for referance incase we change anything
            int actualHeight = options.OutHeight;
            int actualWidth = options.OutWidth;

            //max Height and width values of the compressed image is taken as 816x612
            //no idea what f means but it's all good apparently
            float maxHeight = 816.0f;
            float maxWidth = 612.0f;

            //ratio = aspect ratio
            //calculate the actual image ratio
            float imgRatio = actualWidth / actualHeight;
            //calculate the ratio we're going to I think
            float maxRatio = maxWidth / maxHeight;

            //width and height values are set maintaining the aspect ratio of the image
            if (actualHeight > maxHeight || actualWidth > maxWidth)
            {
                if (imgRatio < maxRatio) { imgRatio = maxHeight / actualHeight; actualWidth = (int)(imgRatio * actualWidth); actualHeight = (int)maxHeight; }
                else if (imgRatio > maxRatio)
                {
                    imgRatio = maxWidth / actualWidth;
                    actualHeight = (int)(imgRatio * actualHeight);
                    actualWidth = (int)maxWidth;
                }
                else
                {
                    actualHeight = (int)maxHeight;
                    actualWidth = (int)maxWidth;

                }
            }

            //setting inSampleSize value allows to load a scaled down version of the original image
            options.InSampleSize = calculateInSampleSize(options, actualWidth, actualHeight);

            //InJustDecodeBounds set to false to load the actual bitmap
            options.InJustDecodeBounds = false;

            //this options allow android to claim the bitmap memory if it runs low on memory
            options.InPurgeable = true;
            options.InInputShareable = true;
            //16KB of temp storage? kden
            options.InTempStorage = new byte[16 * 1024];

            //load the bitmap from its path
            bmp = BitmapFactory.DecodeFile(inputPath, options);

            scaledBitmap = Bitmap.CreateBitmap(actualWidth, actualHeight, Bitmap.Config.Argb8888);

            float ratioX = actualWidth / (float)options.OutWidth;
            float ratioY = actualHeight / (float)options.OutHeight;

            //still dunno what's with these f's 
            float middleX = actualWidth / 2.0f;
            float middleY = actualHeight / 2.0f;

            //new matrix so we can use it below in the canvus 
            var scaleMatrix = new Matrix();
            scaleMatrix.SetScale(ratioX, ratioY, middleX, middleY);

            Canvas canvas = new Canvas(scaledBitmap);
            canvas.Matrix = scaleMatrix;
            canvas.DrawBitmap(bmp, middleX - bmp.Width / 2, middleY - bmp.Height / 2, new Paint());

            //check the rotation of the image and display it properly
            
            //try
            //{
            //    exif = new ExifInterface(filePath);

            //    int orientation = exif.getAttributeInt(
            //            ExifInterface.TAG_ORIENTATION, 0);
            //    Log.d("EXIF", "Exif: " + orientation);
            //    Matrix matrix = new Matrix();
            //    if (orientation == 6)
            //    {
            //        matrix.postRotate(90);
            //        Log.d("EXIF", "Exif: " + orientation);
            //    }
            //    else if (orientation == 3)
            //    {
            //        matrix.postRotate(180);
            //        Log.d("EXIF", "Exif: " + orientation);
            //    }
            //    else if (orientation == 8)
            //    {
            //        matrix.postRotate(270);
            //        Log.d("EXIF", "Exif: " + orientation);
            //    }
            //    scaledBitmap = Bitmap.createBitmap(scaledBitmap, 0, 0,
            //            scaledBitmap.getWidth(), scaledBitmap.getHeight(), matrix,
            //            true);
            //}
            //catch (IOException e)
            //{
            //    e.printStackTrace();
            //}
            using( var fileStream = new System.IO.FileStream(outputPath, System.IO.FileMode.OpenOrCreate))
            {
                scaledBitmap.Compress(Bitmap.CompressFormat.Jpeg, 80, fileStream);               
            }
            scaledBitmap.Dispose();
            return System.IO.File.ReadAllBytes(outputPath);

        }

        public static int calculateInSampleSize(BitmapFactory.Options options, int reqWidth, int reqHeight)
        {
            int height = options.OutHeight;
            int width = options.OutWidth;
            int inSampleSize = 1;

            if (height > reqHeight || width > reqWidth)
            {
                int heightRatio = (int)Math.Round((float)height / (float)reqHeight);
                int widthRatio = (int)Math.Round((float)width / (float)reqWidth);
                inSampleSize = heightRatio < widthRatio ? heightRatio : widthRatio;
            }
            float totalPixels = width * height;
            float totalReqPixelsCap = reqWidth * reqHeight * 2; while (totalPixels / (inSampleSize * inSampleSize) > totalReqPixelsCap)
            {
                inSampleSize++;
            }

            return inSampleSize;
        }

    }
}