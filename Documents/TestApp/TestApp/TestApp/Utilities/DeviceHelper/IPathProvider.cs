namespace TestApp.Utilities
{
    public interface IPathProvider
    {
        string PersonalFolderPath { get; }
        string AppLibraryPath { get; }
        string AppExternalDriveFolderPath { get; }
        string AppDocumentFolderPath { get; }
        string AppImageFolderPath { get; }
        string MainDatabasePath { get; }
        string TempFolder { get; }
    }
}