using Authorization.Extensibility.LoginModels;
using System.Threading.Tasks;

namespace Authorization.Extensibility.Repository
{
    public interface IUserRepository
    {
        Task CreateAsync(UserDTO user);
        Task<bool> Edit(UserDTO userInfo);
        string GetUserPassword(UserDTO loginCredentials);
    }
}