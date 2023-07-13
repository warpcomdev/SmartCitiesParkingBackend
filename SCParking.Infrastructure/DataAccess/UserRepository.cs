using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using SCParking.Domain.Common;
using SCParking.Domain.Models;
using SCParking.Domain.Interfaces;
using SCParking.Infrastructure.ContextDb;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Logging;
using System.Collections.Immutable;
using System.Collections.Concurrent;
using System.Security.Cryptography;
using SCParking.Domain.Models.Authentication;

namespace SCParking.Infrastructure.DataAccess
{


    public class UserRepository : IUserRepository
    {

        private SmartCities_Context context;
        private readonly IConfiguration configuration;
        private readonly ILogger _logger;
        public IImmutableDictionary<string, RefreshToken> UsersRefreshTokensReadOnlyDictionary => _usersRefreshTokens.ToImmutableDictionary();
        private readonly ConcurrentDictionary<string, RefreshToken> _usersRefreshTokens;  // can store in a database or a distributed cache


        public UserRepository(SmartCities_Context context, IConfiguration configuration, ILogger<UserRepository> logger)
        {
            this.context = context;
            this.configuration = configuration;
            _usersRefreshTokens = new ConcurrentDictionary<string, RefreshToken>();
            _logger = logger;
        }

        public async Task<User> Authenticate(string email, string password)
        {
            try
            {
                return await  context.Users.FirstOrDefaultAsync(x => x.Email.ToLower() == email.ToLower() && x.Password == password && x.Status == 1);
               // return await context.Users.Include(x=>x.Role).Include(x => x.Account).FirstOrDefaultAsync(x => x.Email.ToLower() == email.ToLower() && x.Password == password && x.Status == 1 && x.UserType==null);
            }
            catch (Exception ex)
            {
                _logger.LogError("Method[Authenticate] - " + ex.Message);
                throw ex;
            }

        }

        public async Task<User> AuthenticateApiOnline(string email, string password)
        {
            try
            {
                return null;
                // return await context.Users.Include(x => x.Role).Include(x => x.Account).FirstOrDefaultAsync(x => x.Email.ToLower() == email.ToLower() && x.Password == password && x.Status == 1 && ( x.UserType == "API" || x.UserType == "CALLCENTER"));
            }
            catch (Exception ex)
            {
                _logger.LogError("Method[AuthenticateApiOnline] - " + ex.Message);
                throw ex;
            }

        }

     

        public Token CreateToken(User user)
        {
            // appsetting for Token JWT
            var secretKey = configuration.GetSection("AppSettings:Jwt_Secret_key").Value;
            var audienceToken = configuration.GetSection("AppSettings:Jwt_Audience_Token").Value;
            var issuerToken = configuration.GetSection("AppSettings:Jwt_Issuer_Token").Value;
            var expireTime = configuration.GetSection("AppSettings:Jwt_Expire_Minutes").Value;
            var refreshTokenExpireTime = configuration.GetSection("AppSettings:Jwt_RefreshTokenExpiration").Value;

            var securityKey = new SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(secretKey));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            var usertype = "";
            /*if(!String.IsNullOrEmpty(user.UserType))
            {
                usertype = user.UserType;
            }*/
            
            // create a claimsIdentity
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[] {
                new Claim(ClaimTypes.Name, user.Email),
                //new Claim(ClaimTypes.Role, user.Role.Name.ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                //new Claim(ClaimTypes.UserData, user.AccountId.ToString()),
                //new Claim(ClaimTypes.GroupSid, user.RoleId.ToString()),
                new Claim("usertype",usertype),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            });

            // create token to the user
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = tokenHandler.CreateJwtSecurityToken(
                audience: audienceToken,
                issuer: issuerToken,
                subject: claimsIdentity,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToInt32(expireTime)),
                signingCredentials: signingCredentials);

            var jwtTokenString = tokenHandler.WriteToken(jwtSecurityToken);

            var refreshToken = new RefreshToken
            {
                UserName = user.UserName,
                TokenString = GenerateRefreshTokenString(),
                ExpireAt = DateTime.UtcNow.AddMinutes(Convert.ToInt32(refreshTokenExpireTime))
            };

            _usersRefreshTokens.AddOrUpdate(refreshToken.TokenString, refreshToken, (s, t) => refreshToken);

            return new Token
            {
                AccessToken = jwtTokenString,
                RefreshToken = refreshToken
            };

        }

        private static string GenerateRefreshTokenString()
        {
            var randomNumber = new byte[32];
            using var randomNumberGenerator = RandomNumberGenerator.Create();
            randomNumberGenerator.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        public void RemoveRefreshTokenByUserName(string userName)
        {
            var refreshTokens = _usersRefreshTokens.Where(x => x.Value.UserName == userName).ToList();
            foreach (var refreshToken in refreshTokens)
            {
                _usersRefreshTokens.TryRemove(refreshToken.Key, out _);
            }
        }

        public async Task<User> GetByEmail(string email)
        {
            return await context.Users.SingleOrDefaultAsync(x => x.Email.ToLower() == email.ToLower() && x.Status != 3);
        }

        public async Task<User> GetExistEmail(Guid id, string email)
        {
            return await context.Users.SingleOrDefaultAsync(x => x.Email.ToLower() == email.ToLower() && x.Status != 3 && x.Id != id);
        }

        public async Task<User> GetByResetTokenPassword(string token)
        {
            return await context.Users.SingleOrDefaultAsync(x => x.ResetTokenPassword == token &&
                x.ResetTokenPasswordExpire > DateTime.UtcNow);
        }

        public async Task<User> GetByResetTokenEmail(string token)
        {
            return await context.Users.SingleOrDefaultAsync(x => x.ResetTokenEmail == token &&
                x.ResetTokenEmailExpire > DateTime.UtcNow);
        }

        public async Task<User> UpdateProfile(User usermodel)
        {
            var record = context.Users.FirstOrDefault(x => x.Id == usermodel.Id);
            try
            {
                record.FirstName = usermodel.FirstName;
                record.LastName = usermodel.LastName;
                record.Phone = usermodel.Phone;
                record.SecondPhone = usermodel.SecondPhone;
                record.AvatarUrl = usermodel.AvatarUrl;              
                record.UpdatedAt = DateTime.UtcNow;
                await context.SaveChangesAsync();
                return record;
            }
            catch (Exception ex)
            {
                _logger.LogError("Method[UpdateProfile] - " + ex.Message);
                throw ex;
            }            
        }

        public async Task<User> UpdateProfilePatch(User usermodel)
        {
            var record = await context.Users.FirstOrDefaultAsync(x => x.Id == usermodel.Id);
            try
            {
                if(!string.IsNullOrEmpty(usermodel.FirstName))
                   record.FirstName = usermodel.FirstName;
                if (!string.IsNullOrEmpty(usermodel.LastName))
                    record.LastName = usermodel.LastName;
                if (!string.IsNullOrEmpty(usermodel.Phone))
                    record.Phone = usermodel.Phone;  
                if (!string.IsNullOrEmpty(usermodel.AvatarUrl))
                    record.AvatarUrl = usermodel.AvatarUrl;
                record.UpdatedAt = DateTime.UtcNow;
                record.SecondPhone = usermodel.SecondPhone;
                await context.SaveChangesAsync();
                return record;
            }
            catch (Exception ex)
            {
                _logger.LogError("Method[UpdateProfile] - " + ex.Message);
                throw ex;
            }
        }

        public async Task<User> UpdateProfileEmail(User usermodel)
        {
            var record = context.Users.FirstOrDefault(x => x.Id == usermodel.Id);
            try
            {
                record.Email = usermodel.Email.ToLower();
                record.ResetEmail = null;
                record.UnConfirmedEmail = null;
                record.ResetTokenEmail = null;
                record.ResetTokenEmailExpire = null;
                record.UpdatedAt = DateTime.UtcNow;
                await context.SaveChangesAsync();
                return record;
            }
            catch (Exception ex)
            {
                _logger.LogError("Method[UpdateProfileEmail] - " + ex.Message);
                throw ex;
            }
        }

        public async Task<User> UpdateProfileUnConfirmedEmail(User usermodel)
        {
            var record = context.Users.FirstOrDefault(x => x.Id == usermodel.Id);
            try
            {
                record.ResetEmail = usermodel.ResetEmail;
                record.UnConfirmedEmail = true;
                record.ResetTokenEmail = usermodel.ResetTokenEmail;
                record.ResetTokenEmailExpire = usermodel.ResetTokenEmailExpire;
                record.UpdatedAt = DateTime.UtcNow;
                await context.SaveChangesAsync();
                return record;
            }
            catch (Exception ex)
            {
                _logger.LogError("Method[UpdateProfileUnConfirmedEmail] - " + ex.Message);
                throw ex;
            }
        }


        public async Task<User> Delete(Guid id)
        {
            var record = context.Users.FirstOrDefault(x => x.Id == id);
            record.Status = 3;
            record.DeletedAt = DateTime.UtcNow;
            await context.SaveChangesAsync();
            return record;
        }

        public async Task<ICollection<User>> Get()
        {
            var data = await (from b in context.Users
                              where b.Status != 3
                              select b).ToListAsync();

            return data;
           
        }

        public async Task<ICollection<User>> GetAllUsers(Guid accountId)
        {
            
            var data = await (from b in context.Users
                              where b.Status != 3 //&& b.AccountId == accountId
                              //where b.RoleId != Constants.SuperAdmin
                              select b).ToListAsync();

            return data;

        }

        public async Task<ICollection<User>> GetAdministrators(Guid accountId)
        {
            var roleId = Constants.Administrator;
            var data = await (from b in context.Users
                              where b.Status != 3 //&& b.AccountId == accountId && b.RoleId == roleId
                              select b).ToListAsync();

            return data;

        }

        public async Task<ICollection<User>> GetOperators(Guid accountId)
        {
            var roleId = Constants.Operator;
            var data = await (from b in context.Users
                              where b.Status != 3 //&& b.AccountId == accountId && b.RoleId== roleId
                              select b).ToListAsync();

            return data;

        }

        public async Task<ICollection<User>> GetAgents(Guid accountId)
        {
            var roleId = Constants.Agent;
            var data = await (from b in context.Users
                              where b.Status != 3 //&& b.AccountId == accountId && b.RoleId == roleId
                              select b).ToListAsync();

            return data;

        }


        public async Task<User> GetById(Guid id)
        {
            return await context.Users.SingleOrDefaultAsync(x => x.Id == id);
           // return await context.Users.SingleOrDefaultAsync(x => x.Id == id && x.Status != 3);
        }

        public async Task<User> GetByIdByAccount(Guid id, Guid accountId)
        {
            return await context.Users.SingleOrDefaultAsync(x =>
                x.Id == id && x.Status != 3); //&& x.AccountId == accountId);
        }

        public async Task<User> GetByIdAllUsers(Guid id, Guid accountId)
        {

            return await context.Users.SingleOrDefaultAsync(x =>
                x.Id == id && x.Status != 3); //&& x.AccountId == accountId);
        }

        public async Task<User> GetByIdOperators(Guid id, Guid accountId)
        {
            var roleId = Constants.Operator;
            return await context.Users.SingleOrDefaultAsync(x =>
                x.Id == id && x.Status != 3); //&& x.AccountId == accountId && x.RoleId == roleId);
        }

        public async Task<User> GetByIdOperatorsAdministrators(Guid id, Guid accountId)
        {
            var roleIdOperator = Constants.Operator;
            var roleIdAdministrator = Constants.Administrator;
            return await context.Users.SingleOrDefaultAsync(x => x.Id == id && x.Status != 3 /*&& x.AccountId == accountId && (x.RoleId == roleIdOperator || x.RoleId== roleIdAdministrator)*/);
        }




        public async Task<User> GetByLogin(string name)
        {
            return null;
           // return await context.User.FirstOrDefaultAsync(x => x.Name == name && x.Status!=3);
        }

        public async Task<User> Insert(User usermodel)
        {
            try
            {
                context.Users.Add(usermodel);
                await context.SaveChangesAsync();
                return usermodel;
            }
            catch (Exception ex)
            {
                _logger.LogError("Method[AuthUser] - " + ex.Message);
                throw ex;
            }         
                     
            
        }

        public async Task<User> UpdatePassword(User usermodel)
        {
            var record = context.Users.FirstOrDefault(x => x.Id == usermodel.Id);
            try
            {
               
                record.Password = usermodel.Password;
                record.UpdatedAt = usermodel.UpdatedAt;
                record.ModifiedBy = usermodel.ModifiedBy;
                record.PasswordLastChange = usermodel.PasswordLastChange;
                await context.SaveChangesAsync();
                return record;
            }
            catch (Exception ex)
            {

            }

            return record;
        }

        public async Task<User> ForgotPassword(User usermodel)
        {
            var record = context.Users.FirstOrDefault(x => x.Id == usermodel.Id);
            try
            {
                record.ResetTokenPassword = usermodel.ResetTokenPassword;
                record.ResetTokenPasswordExpire = usermodel.ResetTokenPasswordExpire;
                await context.SaveChangesAsync();
                return record;
            }
            catch (Exception ex)
            {

            }

            return record;
        }

        public async Task<User> ResetPassword(User usermodel)
        {
            var record = context.Users.FirstOrDefault(x => x.Id == usermodel.Id);
            try
            {
                
                record.Password = usermodel.Password;
                record.PasswordLastChange = usermodel.PasswordLastChange;
                record.ResetTokenPassword = usermodel.ResetTokenPassword;
                record.ResetTokenPasswordExpire = usermodel.ResetTokenPasswordExpire;

                await context.SaveChangesAsync();
                return record;
            }
            catch (Exception ex)
            {

            }

            return record;
        }

        public async Task<User> Update(User usermodel)
        {
            var record = context.Users.FirstOrDefault(x => x.Id == usermodel.Id);
            try {
                record.FirstName = usermodel.FirstName;
                record.LastName = usermodel.LastName;
                record.Email = usermodel.Email.ToLower();
                /*record.RoleId = usermodel.RoleId;
                record.CustomerId = usermodel.CustomerId;*/
                /*record.AllowCreateCampaigns = usermodel.AllowCreateCampaigns;
                record.AllowCreateCustomers = usermodel.AllowCreateCustomers;
                record.AllowManageWorkflows = usermodel.AllowManageWorkflows;
                record.TargetId = usermodel.TargetId;
                record.TargetType = usermodel.TargetType;*/
                record.UpdatedAt = usermodel.UpdatedAt;
                record.ModifiedBy = usermodel.ModifiedBy;
                if (record.Password != usermodel.Password)
                {
                    record.PasswordLastChange = DateTime.UtcNow;
                }

                await context.SaveChangesAsync();
                return record;
            }
            catch(Exception ex)
            {

            }

            return record;
        }

        public async Task<User> Patch(User usermodel)
        {
            var record = context.Users.FirstOrDefault(x => x.Id == usermodel.Id);
            try
            {
                if (!string.IsNullOrEmpty(usermodel.FirstName))
                record.FirstName = usermodel.FirstName;
                if (!string.IsNullOrEmpty(usermodel.LastName))
                    record.LastName = usermodel.LastName;
                if (!string.IsNullOrEmpty(usermodel.Email))
                    record.Email = usermodel.Email.ToLower();
               /* if (usermodel.RoleId!=null && usermodel.RoleId!=Guid.Empty)
                   record.RoleId = usermodel.RoleId;
                if (usermodel.CustomerId!= null && usermodel.CustomerId != Guid.Empty)
                   record.CustomerId = usermodel.CustomerId;
               */
                /*if (usermodel.AllowCreateCampaigns != null)
                     record.AllowCreateCampaigns = usermodel.AllowCreateCampaigns;

                if (usermodel.AllowCreateCustomers != null)
                    record.AllowCreateCustomers = usermodel.AllowCreateCustomers;

                if (usermodel.AllowManageWorkflows != null)
                    record.AllowManageWorkflows = usermodel.AllowManageWorkflows;

                if (usermodel.TargetId != null)
                    record.TargetId = usermodel.TargetId;

                if (usermodel.TargetType != null)
                    record.TargetType = usermodel.TargetType;
                */
                record.UpdatedAt = usermodel.UpdatedAt;               
                    record.ModifiedBy = usermodel.ModifiedBy;
               

                await context.SaveChangesAsync();
                return record;
            }
            catch (Exception ex)
            {

            }

            return record;
        }

        public async Task<bool> AuthUser(string username, string password, int usertype)
        {
            try
            {
                var exist = await context.Users.CountAsync(x => x.UserName == username && x.Password == password && x.Status == 1);
                if (exist > 0)
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError("Method[AuthUser] - " + ex.Message);
               throw ex;
            }          

        }

        public async Task<User> GetByUserPass(string username, string password, int usertype)
        {
            try
            {
                using (var connection = new SqlConnection(this.context.Database.GetDbConnection().ConnectionString))
                {
                    using (var command = new SqlCommand("pro_user_authenticate", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@p_username", username);
                        command.Parameters.AddWithValue("@p_password", password);

                        await connection.OpenAsync();
                        var user = new User();

                        var reader = await command.ExecuteReaderAsync();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                 user = new User
                                {
                                     Id = reader.GetGuid(0)
                                    ,FirstName = reader.GetString(1)
                                    ,LastName = reader.GetString(2)                                    
                                 };
                               
                            }
                            return user;
                        }
                        else
                        {
                            return null;
                        }
;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Method[GetByUserPass] - " + ex.Message);
                return null;
            }

        }

        

        public async Task<ICollection<User>> GetByRoleId(Guid roleId)
        {
            var data = await(from b in context.Users
                            // where b.RoleId == roleId
                             where b.DeletedAt == null
                             select b).ToListAsync();

            return data;
        }

        public async Task<ICollection<User>> GetByRoleIdByAccount(Guid roleId, Guid accountId)
        {
            var data = await (from b in context.Users
                              //where b.RoleId == roleId
                              where b.DeletedAt == null
                              //where b.AccountId == accountId
                              where b.Status!=3
                              select b).ToListAsync();

            return data;
        }


    }
}
