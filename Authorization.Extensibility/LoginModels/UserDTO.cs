using System.ComponentModel.DataAnnotations;

namespace Authorization.Extensibility.LoginModels
{
    public class UserDTO
    {
        [Key]
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }
    }
}