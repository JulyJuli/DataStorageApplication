using System;

namespace DataStorageApplication.TestProject.DatabaseModels.TodoListModel
{
    [Serializable]
    public class Task : TaskBase
    {
        public Task() { }
        public Task(string title, string description, bool deleted, bool completed, DateTime dueDate, string comment)
            : base(title, description, deleted)
        {
            Completed = completed;
            DueDate = dueDate;
            Comment = comment;
        }
        public DateTime DueDate { get; set; }

        public string Comment { get; set; }

        public bool Completed { get; set; }
    }
}