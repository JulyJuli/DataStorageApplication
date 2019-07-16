using DocumentDatabase.Extensibility.Helpers;
using System;
using System.IO;

namespace DocumentDatabase.Service.Helpers
{
   public class FileProcessingHelper : IFileProcessingHelper
    {
        public string FormFullPath(string folderPath, string fileName, string fileExtension)
        {
            return $"{Path.Combine(folderPath, fileName)}.{fileExtension}";
        }

        public FileMode DefineFileMode(bool appendFileMode)
        {
            return appendFileMode ? FileMode.Append : FileMode.Create;
        }

        public string GetPathToDestinationFolder(string fileExtension, string modelType, string databaseFolderName)
        {
            return Path.Combine(this.GetPathToDocumentFolder(), databaseFolderName, fileExtension, modelType);
        }

        private string GetPathToDocumentFolder()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        }
    }
}
