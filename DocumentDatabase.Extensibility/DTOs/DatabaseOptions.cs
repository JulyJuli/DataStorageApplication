namespace DocumentDatabase.Extensibility.DTOs
{
    public class DatabaseOptions
    {
        public DatabaseOptions() { }

        public DatabaseOptions(string databaseExtention, string folderName)
        {
            DatabaseExtention = databaseExtention;
            FolderName = folderName;
        }

        public string DatabaseExtention { get; set; }

        public string FolderName { get; set; }
    }
}
