//using Newtonsoft.Json;
//using PCLStorage;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace PictureAnalysis.Mobile.Services.UserSettings
//{
//    public class UserSettings
//    {
//        public UserSettings()
//        {
//            Settings = ReadSettingsFromFile().Result;
//        }

//        private const string _userSettingsFolder = "UserSettings";
//        private const string _userSettingsFile = "usersettings.json";
//        public UserSettingsModel Settings { get; set; }

//        public async Task SaveSettings()
//        {
//            var folder = await NavigateToFolder(_userSettingsFolder);
//            await SerializeSettings(folder, Settings);
//        }

//        public async Task ToggleNightMode(bool isNight)
//        {
//            if (isNight)
//            {
//                Settings.BackgroundColor = Xamarin.Forms.Color.White;
//                Settings.DescriptionTextColor = Xamarin.Forms.Color.Black;
//            }
//            else
//            {
//                Settings.BackgroundColor = Xamarin.Forms.Color.Black;
//                Settings.DescriptionTextColor = Xamarin.Forms.Color.White;
//            }
//            await SaveSettings();

//        }

//        private async Task<IFolder> NavigateToFolder(string targetFolder)
//        {
//            IFolder rootFolder = FileSystem.Current.LocalStorage;
//            IFolder folder = await rootFolder.CreateFolderAsync(targetFolder,
//                CreationCollisionOption.OpenIfExists);

//            return folder;
//        }

//        private async Task SerializeSettings(IFolder folder, UserSettingsModel companies)
//        {
//            IFile file = await folder.CreateFileAsync(_userSettingsFile, CreationCollisionOption.ReplaceExisting);
//            var companiesString = JsonConvert.SerializeObject(companies);
//            await file.WriteAllTextAsync(companiesString);
//        }
                

//        private async Task<UserSettingsModel> ReadSettingsFromFile()
//        {
//            var folder = await NavigateToFolder(_userSettingsFolder);

//            if ((await folder.CheckExistsAsync(_userSettingsFile)) == ExistenceCheckResult.NotFound)
//            {
//                return new UserSettingsModel();
//            }

//            IFile file = await folder.GetFileAsync(_userSettingsFile);
//            var jsonSettings = await file.ReadAllTextAsync();

//            if (string.IsNullOrEmpty(jsonSettings)) return new UserSettingsModel();

//            var settings = JsonConvert.DeserializeObject<UserSettingsModel>(jsonSettings);

//            return settings;
//        }

//    }
//}
