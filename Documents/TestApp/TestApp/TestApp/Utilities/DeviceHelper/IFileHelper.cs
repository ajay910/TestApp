namespace Aglive.Business.Infrastructure.Utilities
{
    using ApiServices;

    public interface IFileHelper
    {
        bool Save(FileResponse file, string absoluteFolderPath, bool canOverwrite = true);

        bool Exists(string absoluteFilePath);

        byte[] ReadAllBytes(string absoluteFilePath);

        bool DirectoryExists(string folderPath);

        string[] GetDirectoryFiles(string folderPath);
    }
}