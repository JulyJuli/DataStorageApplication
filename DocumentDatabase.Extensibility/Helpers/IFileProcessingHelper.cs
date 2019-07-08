using System.IO;

namespace DocumentDatabase.Extensibility.Helpers
{
    public interface IFileProcessingHelper
    {
        string FormFullPath(string folderPath, string fileName, string fileExtention);

        FileMode DefineFileMode(bool appendFileMode);

        string GetPathToDestinationFolder(string fileExtention, string modelType, string databaseFolderName = "database");
    }
}