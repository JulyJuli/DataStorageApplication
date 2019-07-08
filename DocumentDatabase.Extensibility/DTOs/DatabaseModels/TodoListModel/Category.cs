using DocumentDatabase.Extensibility.DatabaseModels.TodoListModel;
using System;
using System.Collections.Generic;

namespace ToDoList.Extensibility.Models
{
    [Serializable]
    public class Category : TaskBase
    {
        public Category()
        {
            Tasks = new List<Task>();
        }

        public virtual List<Task> Tasks { get; set; }
    }
}