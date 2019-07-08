using System;

namespace DocumentDatabase.Extensibility.DatabaseModels.TodoListModel
{
    [Serializable]
    public abstract class TaskBase : ModelIdentifier
    {
        public TaskBase() { }

        public TaskBase(string title, string description, bool deleted)
        {
            Title = title;
            Description = description;
            Deleted = deleted;
        }
        
        public string Title { get; set; }

        public string Description { get; set; }

        public bool Deleted { get; set; }
    }
}