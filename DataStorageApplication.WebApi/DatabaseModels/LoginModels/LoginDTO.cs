namespace DataStorageApplication.WebApi.DatabaseModels.LoginModels
{
    public class LoginDTO
    {
        public LoginDTO(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }

        public string UserName { get; }
        public string Password { get; }
    }
}
