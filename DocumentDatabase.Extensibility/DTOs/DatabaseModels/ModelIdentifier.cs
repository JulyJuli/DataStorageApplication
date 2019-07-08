using System;

namespace DocumentDatabase.Extensibility.DatabaseModels
{
    public class ModelIdentifier
    {
        public ModelIdentifier()
        {
            Id = Guid.NewGuid().ToString();
        }

        public string Id { get; }
    }
}