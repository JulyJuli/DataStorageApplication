namespace DocumentDatabase.Extensibility.DTOs
{
    public class Security
    {
        public Security() { }
        public Security(string securityKey)
        {
            SecurityKey = securityKey;
        }

        public string SecurityKey { get; set; }
    }
}
