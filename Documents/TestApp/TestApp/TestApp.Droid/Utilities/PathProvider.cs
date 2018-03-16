
using Android.App;
using Environment = Android.OS.Environment;

namespace TestApp.Droid.Utilities
{

    using System.IO;
    using TestApp.Utilities;

    public class PathProvider : PathProviderAbstract
    {
        public override string PersonalFolderPath { get; }
        public override string AppLibraryPath { get; }
        public override string AppExternalDriveFolderPath { get; }
        public override string AppDocumentFolderPath { get; }
        public override string AppImageFolderPath { get; }

        public PathProvider()
        {
            var personalFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            PersonalFolderPath = personalFolder;
            AppLibraryPath = personalFolder;
            AppDocumentFolderPath = Path.Combine(personalFolder, "Documents");
            AppImageFolderPath = Path.Combine(personalFolder, "Images");
            AppExternalDriveFolderPath = Path.Combine(Environment.ExternalStorageDirectory.AbsolutePath, "TestApp");
        }



       
    }
    public abstract class PathProviderAbstract : IPathProvider
    {
        public string MainDatabasePath
        {
            get
            {
                var destinationDbFilePath = Path.Combine(AppLibraryPath, "Database.db3");
                return destinationDbFilePath;
            }
        }

        public abstract string PersonalFolderPath { get; }
        public abstract string AppLibraryPath { get; }
        public abstract string AppExternalDriveFolderPath { get; }
        public abstract string AppDocumentFolderPath { get; }
        public abstract string AppImageFolderPath { get; }

        public string TempFolder
        {
            get
            {
                return Path.Combine(AppExternalDriveFolderPath, "Temp");
            }
        }
    }
}
