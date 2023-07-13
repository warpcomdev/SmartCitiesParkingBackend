using SCParking.Domain.Models;
using SCParking.Domain.Models.Authentication;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SCParking.Domain.Interfaces
{
    public interface IUserRepository
    {

        public Task<User> Authenticate(string email, string password);

        public Task<User> AuthenticateApiOnline(string email, string password);
                

        public Task<User> GetByEmail(string email);

        public Task<User> GetExistEmail(Guid id, string email);

        public Task<User> GetByResetTokenPassword(string token);

        public Task<User> GetByResetTokenEmail(string token);

        public Task<User> UpdateProfile(User usermodel);

        public Task<User> UpdateProfilePatch(User usermodel);

        public Task<User> UpdateProfileEmail(User usermodel);

        public Task<User> UpdateProfileUnConfirmedEmail(User usermodel);

        

        public  Task<ICollection<User>> Get();

        public Task<User> Insert(User usermodel);

        public Task<User> GetById(Guid id);

        public Task<User> GetByIdByAccount(Guid id, Guid accountId);

        public Task<User> GetByIdOperators(Guid id, Guid accountId);

        public Task<User> GetByIdOperatorsAdministrators(Guid id, Guid accountId);

        public Task<User> GetByIdAllUsers(Guid id, Guid accountId);

        public Task<ICollection<User>> GetAllUsers(Guid accountId);

        public Task<ICollection<User>> GetAdministrators(Guid accountId);

        public Task<ICollection<User>> GetOperators(Guid accountId);

        public Task<ICollection<User>> GetAgents(Guid accountId);

        public Task<ICollection<User>> GetByRoleId(Guid roleId);

        public Task<ICollection<User>> GetByRoleIdByAccount(Guid roleId, Guid accountId);

        public Task<User> GetByLogin(string name);

        public Task<User> Delete(Guid id);

        public Task<User> Update(User usermodel);

        public Task<User> Patch(User usermodel);

        public Task<User> UpdatePassword(User usermodel);

        public Task<User> ForgotPassword(User usermodel);

        public Task<User> ResetPassword(User usermodel);

        public Task<bool> AuthUser(string username, string password, int usertype);

        public Task<User> GetByUserPass(string username, string password, int usertype);

        public Token CreateToken(User user);
    }
}
