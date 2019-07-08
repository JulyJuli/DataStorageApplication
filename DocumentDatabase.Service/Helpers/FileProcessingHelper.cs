using DocumentDatabase.Extensibility.Helpers;
using System;
using System.IO;

namespace DocumentDatabase.Service.Helpers
{
   public class FileProcessingHelper : IFileProcessingHelper
    {
        public string FormFullPath(string folderPath, string fileName, string fileExtention)
        {
            return string.Format("{0}.{1}", (object)Path.Combine(folderPath, fileName), fileExtention);
        }

        public FileMode DefineFileMode(bool appendFileMode)
        {
            return appendFileMode ? FileMode.Append : FileMode.Create;
        }

        public string GetPathToDestinationFolder(string fileExtention, string modelType, string databaseFolderName)
        {
            return Path.Combine(this.GetPathToDocumentFolder(), databaseFolderName, fileExtention, modelType);
        }

        private string GetPathToDocumentFolder()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        }
    }
}
