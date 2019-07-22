using System;

namespace DataStorageApplication.WebApi.DatabaseModels.TodoListModel
{
    [Serializable]
    public class Task : TaskBase
    {
        public Task() { }
        public Task(string title, string description, bool deleted, bool completed, DateTime dueDate, string comment, string id)
            : base(title, description, deleted, id)
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