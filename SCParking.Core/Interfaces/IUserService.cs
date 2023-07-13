using SCParking.Domain.Models.Authentication;
using SCParking.Domain.Views.DTOs;
using SCParking.Domain.Views.Paged;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SCParking.Core.Interfaces
{
    public interface IUserService
    {
        public Task<Token> Authenticate(string email, string password);

        public Task<Token> AuthenticateApiOnline(string email, string password);
       
        public Task ForgotPassword(ForgotPasswordDto model,string origin);

        public Task ResetPassword(ResetPasswordDto model);

        public Task ChangePassword(Guid id, ChangePasswordDto model);

        public Task<CurrentSessionDto> GetProfile(Guid id);

        public Task UpdateProfile(Domain.Models.User model, IFormFile avatar);

        public Task UpdateProfilePatch(Domain.Models.User model, IFormFile avatar);

        public Task UpdateProfileEmail(Domain.Models.User model);

        public Task UpdateProfileEmailByToken(string token);

        public Task<UserResponsePostDto> Insert(UserRequestPostDto model);

        public Task<ICollection<UserResponseGetDto>> Get();

        public Task<PagedList<UserResponseGetDto>> GetUsersByFilter(UserFilterDto userFilter);

        public Task<PagedList<UserResponseToAssignGetDto>> GetUsersToAssign(UserFilterDto userFilter);


        public Task<UserResponseGetDto> GetById(Guid id, Guid currentAccountId,Guid currentRoleId);


        public Task<ICollection<UserResponseGetDto>> GetByRole(Guid roleId);

        public Task<PagedList<UserResponseGetDto>> GetUsersByRoleFilter(Guid roleId, RoleFilterDto roleFilter);

        public Task<Domain.Models.User> GetByLogin(string name);

        public Task<Domain.Models.User> Delete(Guid id, Guid currentAccountId, Guid currentRoleId);

        public Task<UserResponsePutDto> Update(UserRequestPutDto model, Guid currentAccountId, Guid currentRoleId);

        public Task<UserResponsePutDto> Patch(UserRequestPutDto model, Guid currentAccountId, Guid currentRoleId);

        public Task<bool> AuthUser(string username, string password, int usertype);

        public Task<Domain.Models.User> GetByUserPass(string username, string password, int usertype);

        public Token CreateToken(Domain.Models.User user);

    }
}
