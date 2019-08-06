using Authorization.Domain.Context;
using Authorization.Extensibility.LoginModels;
using Authorization.Extensibility.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Authorization.Domain
{
    public class UserRepository : IUserRepository
    {
        private AuthorizationContext dbContext;

        public UserRepository(AuthorizationContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task CreateAsync(UserDTO user)
        {
            if (!IsUserExist(user.UserName))
            {
                dbContext.Add(user);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task<bool> Edit(UserDTO userInfo)
        {
            dbContext.Entry(userInfo).State = EntityState.Modified;
            int result = await dbContext.SaveChangesAsync();

            if (result != -1)
            {
                return true;
            }
            return false;
        }

        public string GetUserPassword(UserDTO loginCredentials)
        {
            return dbContext.Users.ToList().First(user => user.UserName.Equals(loginCredentials.UserName)).Password;
        }

        private bool IsUserExist(string login)
        {
            return dbContext.Users.ToList().Any(user => user.UserName.Equals(login));
        }
    }
}
