using System.IO;

namespace DocumentDatabase.Extensibility.Helpers
{
    public interface IFileProcessingHelper
    {
        string FormFullPath(string folderPath, string fileName, string fileExtention);

        FileMode DefineFileMode(bool appendFileMode);

        string FormFullPathWitoutExtention(string folderPath, string fileName);

        string GetPathToDestinationFolder(string databaseFolderName = "database");
    }
}