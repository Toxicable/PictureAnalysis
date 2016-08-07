# First clean the Release target.
#msbuild.exe PictureAnalysis.Mobile.Droid.csproj /p:Configuration=Release /t:Clean

# Now build the project, using the Release target.
#msbuild.exe PictureAnalysis.Mobile.Droid.csproj /p:Configuration=Release /t:PackageForAndroid

& 'C:\Program Files (x86)\Android\android-sdk\build-tools\21.1.2\zipalign.exe' -f -v 4 C:\Users\Fabian\Dropbox\PictureAnalysis\PictureAnalysis\src\PictureAnalysis.Mobile\PictureAnalysis.Mobile.Droid\bin\Release\com.fabian.TagSnap-Signed.apk C:\Users\Fabian\Dropbox\PictureAnalysis\PictureAnalysis\src\PictureAnalysis.Mobile\PictureAnalysis.Mobile.Droid\bin\Release\com.fabian.TagSnap.apk
#"C:\Program Files (x86)\Android\android-sdk\build-tools\21.1.2\zipalign.exe" -f -v 4 C:\Users\Fabian\Dropbox\PictureAnalysis\PictureAnalysis\src\PictureAnalysis.Mobile\PictureAnalysis.Mobile.Droid\com.fabian.TagSnap-Signed.apk C:\Users\Fabian\Dropbox\PictureAnalysis\PictureAnalysis\src\PictureAnalysis.Mobile\PictureAnalysis.Mobile.Droid\com.fabian.TagSnap.apk