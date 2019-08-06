using Authorization.Extensibility.LoginModels;
using Authorization.Extensibility.Providers;
using Authorization.Extensibility.Repository;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Authorization.Service
{
    public class AuthorizationProvider : IAuthorizationProvider
    {
        private readonly IUserRepository userRepository;
        private readonly IPasswordHasher<UserDTO> passwordHasher;

        public AuthorizationProvider(IUserRepository userRepository, IPasswordHasher<UserDTO> passwordHasher)
        {
            this.userRepository = userRepository;
            this.passwordHasher = passwordHasher;
        }

        public bool CheckUserCredentials(UserDTO loginCredentials)
        {
            var hashedPassword = userRepository.GetUserPassword(loginCredentials);
            if (!string.IsNullOrEmpty(hashedPassword))
            {
                var result = passwordHasher.VerifyHashedPassword(loginCredentials, hashedPassword, loginCredentials.Password);
                if (result == PasswordVerificationResult.Success) {
                    return true;
                }
            }
            return false;
        }

        public void Create(UserDTO loginCredentials)
        {
           var hashedPassword = passwordHasher.HashPassword(loginCredentials, loginCredentials.Password);
           userRepository.CreateAsync(new UserDTO() { Password = hashedPassword, UserName = loginCredentials.UserName });
        }

        public async Task<bool> EditAsync(UserDTO loginCredentials)
        {
            return await userRepository.Edit(loginCredentials);
        }
    }
}