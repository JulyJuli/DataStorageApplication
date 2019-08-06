using Authorization.Extensibility.LoginModels;
using System.Threading.Tasks;

namespace Authorization.Extensibility.Providers
{
    public interface IAuthorizationProvider
    {
        void Create(UserDTO loginCredentials);
        Task<bool> EditAsync(UserDTO loginCredentials);
        bool CheckUserCredentials(UserDTO loginCredentials);
    }
}