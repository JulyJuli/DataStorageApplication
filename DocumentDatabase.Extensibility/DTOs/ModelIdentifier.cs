using System;

namespace DocumentDatabase.Extensibility.DTOs
{
    public class ModelIdentifier
    {
        public ModelIdentifier() { }

        public ModelIdentifier(string id)
        {
            Id = GetModelIdentifier(id);
        }

        public string Id { get; }

        private string GetModelIdentifier(string id)
        {
            if (string.IsNullOrEmpty(id)) {
                return Guid.NewGuid().ToString();
            }
            return id;
        }
    }
}