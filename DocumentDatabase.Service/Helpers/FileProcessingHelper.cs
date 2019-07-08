using DocumentDatabase.Extensibility.Helpers;
using System;
using System.IO;

namespace DocumentDatabase.Service.Helpers
{
   public class FileProcessingHelper : IFileProcessingHelper
    {
        public string FormFullPath(string folderPath, string fileName, string fileExtention)
        {
            return string.Format("{0}.{1}", (object)Path.Combine(folderPath, fileName), (object)fileExtention);
        }

        public string FormFullPathWitoutExtention(string folderPath, string fileName)
        {
            return string.Format(Path.Combine(folderPath, fileName));
        }

        public FileMode DefineFileMode(bool appendFileMode)
        {
            return appendFileMode ? FileMode.Append : FileMode.Create;
        }

        public string GetPathToDestinationFolder(string databaseFolderName)
        {
            return Path.Combine(this.GetPathToDocumentFolder(), databaseFolderName);
        }

        private string GetPathToDocumentFolder()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        }
    }
}
