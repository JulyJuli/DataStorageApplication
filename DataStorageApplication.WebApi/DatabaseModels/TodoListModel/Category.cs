using System;
using System.Collections.Generic;

namespace DataStorageApplication.WebApi.DatabaseModels.TodoListModel
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