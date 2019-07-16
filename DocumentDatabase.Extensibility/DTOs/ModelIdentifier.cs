using System;

namespace DocumentDatabase.Extensibility.DTOs
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