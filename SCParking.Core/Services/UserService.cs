using SCParking.Core.Interfaces;
using System;
using System.Collections.Generic;
using SCParking.Domain.Interfaces;
using SCParking.Domain.Common;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SCParking.Domain.Validation;
using SCParking.Domain.Views.DTOs;
using System.Linq;
using SCParking.Domain.Models.Authentication;
using SCParking.Domain.Messages;
using System.Text.RegularExpressions;
using SCParking.Domain.Views.Paged;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace SCParking.Core.Services
{
    public class UserService:IUserService
    {
        public IUserRepository _repository;      
        private readonly ILogger _logger;        
        public static IWebHostEnvironment _environment;       
        public IHelpers _helpers;

        public UserService(IUserRepository repository, ILogger<UserService> logger,
             IWebHostEnvironment environment, IHelpers helpers
            )
        {
            _repository = repository;           
            _logger = logger;           
            _environment = environment;
            _helpers = helpers;            
        }


        public async Task<Token> Authenticate(string email, string password)
        {

            try
            {
                //Validations                 
                var error = new ErrorDto();                
                var arr = new List<ErrorDetailDto>();
                dynamic errorField = new System.Dynamic.ExpandoObject();
                var existErrors = false;

                if (string.IsNullOrEmpty(email))
                {                   
                    arr = new List<ErrorDetailDto>();
                    arr.Add(new ErrorDetailDto() { error = TokenError.Blank });
                    errorField.email = arr;
                    existErrors = true;
                }
                else
                {
                    Regex regex = new Regex(RegularExpression.Email);
                    bool isValid = regex.IsMatch(email.Trim());
                    if (!isValid)
                    {
                        arr = new List<ErrorDetailDto>();
                        arr.Add(new ErrorDetailDto() { error = TokenError.Format });
                        errorField.email = arr;
                        existErrors = true;
                    }

                    //Length Validation
                    if(email.Trim().Length > 254)
                    {
                        arr = new List<ErrorDetailDto>();
                        arr.Add(new ErrorDetailDto() { error = TokenError.TooLong, count= email.Trim().Length });
                        errorField.email = arr;
                        existErrors = true;
                    }
                }

                if (string.IsNullOrEmpty(password))
                {                   
                    arr = new List<ErrorDetailDto>();
                    arr.Add(new ErrorDetailDto() { error = TokenError.Blank });
                    errorField.password = arr;
                    existErrors = true;
                }
                else
                {
                    if (password.Length > 72)
                    {
                        arr = new List<ErrorDetailDto>();
                        arr.Add(new ErrorDetailDto() { error = TokenError.TooLong, count = password.Length });
                        errorField.password = arr;
                        existErrors = true;
                    }
                }
               

                error.errors = errorField;
                if (existErrors)
                {
                    error.status = 422;                  
                    throw new InvalidDynamicCommandException(error);
                }
                   

                var passwordMd5 = Tools.GetMD5Hash(password);
                var user = await _repository.Authenticate(email.Trim(), passwordMd5);
                if (user != null)
                {
                   return CreateToken(user);
                }
                else
                {                    
                    arr = new List<ErrorDetailDto>();
                    arr.Add(new ErrorDetailDto() { error = TokenError.Invalid });
                    errorField.login = arr;  
                   
                    error.errors = errorField;
                    error.status = 422;
                    throw new InvalidDynamicCommandException(error);
                }
                
               

            }
            catch (InvalidDynamicCommandException ex)
            {
                _logger.LogError("Method[Authenticate] - " + ex.Message);
                throw ex;
            }
            catch (Exception ex)
                {
                throw ex;
            }

        }

        public async Task<Token> AuthenticateApiOnline(string email, string password)
        {

            try
            {
                //Validations                 
                var error = new ErrorDto();
                var arr = new List<ErrorDetailDto>();
                dynamic errorField = new System.Dynamic.ExpandoObject();
                var existErrors = false;

                if (string.IsNullOrEmpty(email))
                {
                    arr = new List<ErrorDetailDto>();
                    arr.Add(new ErrorDetailDto() { error = TokenError.Blank });
                    errorField.email = arr;
                    existErrors = true;
                }
                else
                {
                    Regex regex = new Regex(RegularExpression.Email);
                    bool isValid = regex.IsMatch(email.Trim());
                    if (!isValid)
                    {
                        arr = new List<ErrorDetailDto>();
                        arr.Add(new ErrorDetailDto() { error = TokenError.Format });
                        errorField.email = arr;
                        existErrors = true;
                    }

                    //Length Validation
                    if (email.Trim().Length > 254)
                    {
                        arr = new List<ErrorDetailDto>();
                        arr.Add(new ErrorDetailDto() { error = TokenError.TooLong, count = email.Trim().Length });
                        errorField.email = arr;
                        existErrors = true;
                    }
                }

                if (string.IsNullOrEmpty(password))
                {
                    arr = new List<ErrorDetailDto>();
                    arr.Add(new ErrorDetailDto() { error = TokenError.Blank });
                    errorField.password = arr;
                    existErrors = true;
                }
                else
                {
                    if (password.Length > 72)
                    {
                        arr = new List<ErrorDetailDto>();
                        arr.Add(new ErrorDetailDto() { error = TokenError.TooLong, count = password.Length });
                        errorField.password = arr;
                        existErrors = true;
                    }
                }


                error.errors = errorField;
                if (existErrors)
                {
                    error.status = 422;
                    throw new InvalidDynamicCommandException(error);
                }


                var passwordMd5 = Tools.GetMD5Hash(password);
                var user = await _repository.AuthenticateApiOnline(email.Trim(), passwordMd5);
                if (user != null)
                {
                    return CreateToken(user);
                }
                else
                {
                    arr = new List<ErrorDetailDto>();
                    arr.Add(new ErrorDetailDto() { error = TokenError.Invalid });
                    errorField.login = arr;

                    error.errors = errorField;
                    error.status = 422;
                    throw new InvalidDynamicCommandException(error);
                }



            }
            catch (InvalidDynamicCommandException ex)
            {
                _logger.LogError("Method[AuthenticateApiOnline] - " + ex.Message);
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

      
        public Token CreateToken(Domain.Models.User user)
        {
            return _repository.CreateToken(user);
        }

        public async Task ForgotPassword(ForgotPasswordDto model,string origin)
        {
            //Validations                 
            var error = new ErrorDto();
            var arr = new List<ErrorDetailDto>();
            dynamic errorField = new System.Dynamic.ExpandoObject();
            var existErrors = false;

            if (string.IsNullOrEmpty(model.email))
            {
                arr = new List<ErrorDetailDto>();
                arr.Add(new ErrorDetailDto() { error = TokenError.Blank });
                errorField.email = arr;
                existErrors = true;
            }
            else
            {
                Regex regex = new Regex(RegularExpression.Email);
                bool isValid = regex.IsMatch(model.email.Trim());
                if (!isValid)
                {
                    arr = new List<ErrorDetailDto>();
                    arr.Add(new ErrorDetailDto() { error = TokenError.Format });
                    errorField.email = arr;
                    existErrors = true;
                }

                //Length Validation
                if (model.email.Trim().Length > 254)
                {
                    arr = new List<ErrorDetailDto>();
                    arr.Add(new ErrorDetailDto() { error = TokenError.TooLong, count = model.email.Trim().Length });
                    errorField.email = arr;
                    existErrors = true;
                }

            }
           

            error.errors = errorField;
            if (existErrors)
            {
                error.status = 422;
                throw new InvalidDynamicCommandException(error);
            }

            var user = await _repository.GetByEmail(model.email);

            // return ok response to prevent email enumeration
            if (user == null) return;

            // create reset token expires after 1 day
            user.ResetTokenPassword = Tools.RandomToken();
            user.ResetTokenPasswordExpire = DateTime.UtcNow.AddDays(1);

            await _repository.ForgotPassword(user);

            //Send Email
            SendEmailPasswordReset(user,origin);
        }

        public async Task ResetPassword(ResetPasswordDto model)
        {
            try {

                //Validations                 
                var error = new ErrorDto();
                var arr = new List<ErrorDetailDto>();
                dynamic errorField = new System.Dynamic.ExpandoObject();
                var existErrors = false;

                if (string.IsNullOrEmpty(model.token))
                {
                    arr = new List<ErrorDetailDto>();
                    arr.Add(new ErrorDetailDto() { error = TokenError.Blank });
                    errorField.token = arr;
                    existErrors = true;
                }

                if (string.IsNullOrEmpty(model.password))
                {
                   
                    arr = new List<ErrorDetailDto>();
                    arr.Add(new ErrorDetailDto() { error = TokenError.Blank });
                    errorField.password = arr;
                    existErrors = true;
                }

                if (string.IsNullOrEmpty(model.confirmPassword))
                {
                    arr = new List<ErrorDetailDto>();
                    arr.Add(new ErrorDetailDto() { error = TokenError.Blank });
                    errorField.confirmPassword = arr;
                    existErrors = true;
                }

                if (!string.IsNullOrEmpty(model.password) && !string.IsNullOrEmpty(model.confirmPassword))
                {
                    var arrPass = new List<ErrorDetailDto>();
                    if (model.password.Length > 72)
                    {
                        arrPass = new List<ErrorDetailDto>();
                        arrPass.Add(new ErrorDetailDto() { error = TokenError.TooLong, count = model.password.Length });
                        errorField.password = arrPass;
                        existErrors = true;
                    }

                    if (model.confirmPassword.Length > 72)
                    {
                        arr = new List<ErrorDetailDto>();
                        arr.Add(new ErrorDetailDto() { error = TokenError.TooLong, count = model.confirmPassword.Length });
                        errorField.confirmPassword = arr;
                        existErrors = true;
                    }

                    Regex regex = new Regex(RegularExpression.Password);
                    bool isValid = regex.IsMatch(model.password.Trim());
                   
                    if (!isValid)
                    {
                        arrPass.Add(new ErrorDetailDto() { error = TokenError.Format });
                        errorField.password = arrPass;
                        existErrors = true;
                    }


                    if (model.confirmPassword != model.password)
                    {                       
                        arr.Add(new ErrorDetailDto() { error = TokenError.Confirmation });
                        errorField.confirmPassword = arr;
                        existErrors = true;
                    }
                }
               

                error.errors = errorField;
                if (existErrors)
                {
                    error.status = 422;
                    throw new InvalidDynamicCommandException(error);
                }

                var user = await _repository.GetByResetTokenPassword(model.token);

                if (user == null)
                {
                    arr = new List<ErrorDetailDto>();
                    arr.Add(new ErrorDetailDto() { error = TokenError.Invalid });
                    errorField.token = arr;

                    error.errors = errorField;
                    error.status = 422;
                    throw new InvalidDynamicCommandException(error);
                }

                // update password and remove reset token
                user.Password = Tools.GetMD5Hash(model.password);
                user.PasswordLastChange = DateTime.UtcNow;
                user.ResetTokenPassword = null;
                user.ResetTokenPasswordExpire = null;

                await _repository.ResetPassword(user);

                SendPasswordUpdated(user,"");

            }
            catch (InvalidDynamicCommandException ex)
            {
                _logger.LogError("Method[ResetPassword] - " + ex.Message);
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }


        private void SendEmailPasswordReset(Domain.Models.User user, string origin)
        {
            string FilePath = @"\\Templates\\password-cambio-solicitud.html";
            string contentRootPath = _environment.ContentRootPath;
            StreamReader str = new StreamReader(contentRootPath + FilePath);
            string mailTemplate = str.ReadToEnd();
            str.Close();

            var resetUrl = $"{Environment.GetEnvironmentVariable("LAIA_FrontendHost")}/#/reset-password?token={user.ResetTokenPassword}";
            
            mailTemplate = mailTemplate.Replace("[reset_password]", resetUrl);
            mailTemplate = mailTemplate.Replace("[user_name]", user.FirstName);
           /* _emailService.Send(
                to: user.Email,
                subject: "Restablecer contraseña",
                html: $@"{mailTemplate}",null, contentRootPath
            );*/

        }

        private void SendEmailUserCreated(Domain.Models.User user, string origin)
        {

            /* string FilePath = @"\\Templates\\invitacion.html";
             string contentRootPath = _environment.ContentRootPath;
             StreamReader str = new StreamReader(contentRootPath + FilePath);
             string mailTemplate = str.ReadToEnd();
             str.Close();
             mailTemplate = mailTemplate.Replace("[role_name]", Tools.GetRoleName(user.Role.Name));
             mailTemplate = mailTemplate.Replace("[account_name]", user.Account.Name);
             mailTemplate = mailTemplate.Replace("[email]", user.Email);
             mailTemplate = mailTemplate.Replace("[password]", user.Password);
             mailTemplate = mailTemplate.Replace("[site]", Environment.GetEnvironmentVariable("LAIA_FrontendHost"));
             _emailService.Send(
                 to: user.Email,
                 subject: "Bienvenido a Laia",
                 html: $@"{mailTemplate}", null, contentRootPath
             );*/
        }

        private void SendEmailChange(Domain.Models.User user, string origin)
        {

            string FilePath = @"\\Templates\\email-cambio-ok.html";
            string contentRootPath = _environment.ContentRootPath;
            StreamReader str = new StreamReader(contentRootPath + FilePath);
            string mailTemplate = str.ReadToEnd();
            str.Close();

            mailTemplate = mailTemplate.Replace("[user_resetEmail]", user.ResetEmail);
            mailTemplate = mailTemplate.Replace("[user_name]", user.FirstName);
           /* _emailService.Send(
                to: user.Email,
                subject: "Cambio de email",
                html: $@"{mailTemplate}", null, contentRootPath
            );*/

        }

        private void SendEmailConfirmation(Domain.Models.User user, string origin)
        {

            string FilePath = @"\\Templates\\email-cambio-cuenta-nueva.html";
            string contentRootPath = _environment.ContentRootPath;
            StreamReader str = new StreamReader(contentRootPath + FilePath);
            string mailTemplate = str.ReadToEnd();
            str.Close();          

            var resetUrl = $"{Environment.GetEnvironmentVariable("LAIA_FrontendHost")}/confirm-email?token={user.ResetTokenEmail}";

            mailTemplate = mailTemplate.Replace("[user_name]", user.FirstName);
            mailTemplate = mailTemplate.Replace("[reset_email]", resetUrl);
          /*  _emailService.Send(
                to: user.Email,
                subject: "Confirmación cambio de email cuenta",
                html: $@"{mailTemplate}", null, contentRootPath
            );*/

        }

        private void SendPasswordUpdated(Domain.Models.User user, string origin)
        {

            string FilePath = @"\\Templates\\password-cambio-ok.html";
            string contentRootPath = _environment.ContentRootPath;
            StreamReader str = new StreamReader(contentRootPath + FilePath);
            string mailTemplate = str.ReadToEnd();
            str.Close();
            mailTemplate = mailTemplate.Replace("[user_name]", user.FirstName);
   
            mailTemplate = mailTemplate.Replace("[site]", Environment.GetEnvironmentVariable("LAIA_FrontendHost"));
           /* _emailService.Send(
                to: user.Email,
                subject: "Se ha actualizado la contraseña",
                html: $@"{mailTemplate}", null, contentRootPath
            );*/
        }

        public async Task ChangePassword(Guid id, ChangePasswordDto model)
        {
            try
            {
                //Validations                 
                var error = new ErrorDto();
                var arr = new List<ErrorDetailDto>();
                dynamic errorField = new System.Dynamic.ExpandoObject();
                var existErrors = false;

                if (string.IsNullOrEmpty(model.currentPassword))
                {
                    var arrcurrentPassword = new List<ErrorDetailDto>();
                    arrcurrentPassword.Add(new ErrorDetailDto() { error = TokenError.Blank });
                    errorField.currentPassword = arrcurrentPassword;
                    existErrors = true;
                }
                else
                {


                    if (model.currentPassword.Length > 72)
                    {
                        arr = new List<ErrorDetailDto>();
                        arr.Add(new ErrorDetailDto() { error = TokenError.TooLong, count = model.currentPassword.Length });
                        errorField.currentPassword = arr;
                        existErrors = true;
                    }

                    var userChange = await _repository.GetById(id);
                    if (userChange.Password != Tools.GetMD5Hash(model.currentPassword))
                    {
                        var arrcurrentPassword = new List<ErrorDetailDto>();
                        arrcurrentPassword.Add(new ErrorDetailDto() { error = TokenError.Invalid });
                        errorField.currentPassword = arrcurrentPassword;
                        existErrors = true;
                    }

                }

                if (string.IsNullOrEmpty(model.password))
                {
                    var arrpassword = new List<ErrorDetailDto>();
                    arrpassword.Add(new ErrorDetailDto() { error = TokenError.Blank });
                    errorField.password = arrpassword;
                    existErrors = true;
                }

                if (string.IsNullOrEmpty(model.confirmPassword))
                {
                    var arrconfirmPassword = new List<ErrorDetailDto>();
                    arrconfirmPassword.Add(new ErrorDetailDto() { error = TokenError.Blank });
                    errorField.confirmPassword = arrconfirmPassword;
                    existErrors = true;
                }

                if (!string.IsNullOrEmpty(model.password))
                {
                    Regex regex = new Regex(RegularExpression.Password);
                    bool isValid = regex.IsMatch(model.password.Trim());
                    var arrPass = new List<ErrorDetailDto>();
                    var arrCompare = new List<ErrorDetailDto>();

                    if (model.password.Length > 72)
                    {
                        arrPass = new List<ErrorDetailDto>();
                        arrPass.Add(new ErrorDetailDto() { error = TokenError.TooLong, count = model.password.Length });
                        errorField.password = arrPass;
                        existErrors = true;
                    }

                    if (!isValid)
                    {
                        arrPass.Add(new ErrorDetailDto() { error = TokenError.Format });
                        errorField.password = arrPass;
                        existErrors = true;
                    }
                }

                if (!string.IsNullOrEmpty(model.confirmPassword))
                {
                    Regex regex = new Regex(RegularExpression.Password);
                    bool isValid = regex.IsMatch(model.confirmPassword.Trim());
                    var arrPass = new List<ErrorDetailDto>();
                    var arrCompare = new List<ErrorDetailDto>();
                    if (model.confirmPassword.Length > 72)
                    {

                        arrCompare = new List<ErrorDetailDto>();
                        arrCompare.Add(new ErrorDetailDto() { error = TokenError.TooLong, count = model.confirmPassword.Length });
                        errorField.confirmPassword = arrCompare;
                        existErrors = true;
                    }

                    if (!isValid)
                    {
                        arrPass.Add(new ErrorDetailDto() { error = TokenError.Format });
                        errorField.confirmPassword = arrPass;
                        existErrors = true;
                    }
                }

                

                if (!string.IsNullOrEmpty(model.password) && !string.IsNullOrEmpty(model.confirmPassword))
                {
                    var arrPass = new List<ErrorDetailDto>();
                    var arrCompare = new List<ErrorDetailDto>();
                    if (model.confirmPassword != model.password)
                    {                       
                        arrCompare.Add(new ErrorDetailDto() { error = TokenError.Confirmation });
                        errorField.confirmPassword = arrCompare;
                        existErrors = true;
                    }
                }
               

                error.errors = errorField;

                if (existErrors)
                {
                    error.status = 422;
                    throw new InvalidDynamicCommandException(error);
                }


                var user = await _repository.GetById(id);

                // update password 
                user.ModifiedBy = id;
                user.Password = Tools.GetMD5Hash(model.password);
                user.PasswordLastChange = DateTime.UtcNow; 
                user.UpdatedAt = DateTime.UtcNow;
                await _repository.UpdatePassword(user);
                SendPasswordUpdated(user, "");
            }
            catch (InvalidDynamicCommandException ex)
            {
                _logger.LogError("Method[ChangePassword] - " + ex.Message);
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }


        public async Task<CurrentSessionDto> GetProfile(Guid id)
        {
            var userModel = await _repository.GetById(id);
            var unconfirm = userModel.UnConfirmedEmail;
            var resetEmail = userModel.ResetEmail;
            if(userModel.UnConfirmedEmail != null)
            {
                if ((bool)unconfirm && DateTime.UtcNow > userModel.ResetTokenEmailExpire)
                {
                    resetEmail = string.Empty;
                    unconfirm = false;
                }
                    
            }
            else
            {
                resetEmail = string.Empty;
                unconfirm = false;
            }
            var currentSession = new CurrentSessionDto()
            {
                id = (Guid)userModel.Id,
                email = userModel.Email,
                unconfirmedEmail = resetEmail,
                firstName = userModel.FirstName,
                lastName = userModel.LastName,
                phone = String.IsNullOrEmpty(userModel.Phone) ? string.Empty : userModel.Phone,
                secondPhone = String.IsNullOrEmpty(userModel.SecondPhone) ? string.Empty : userModel.SecondPhone,
                avatarUrl = String.IsNullOrEmpty(userModel.AvatarUrl) ? string.Empty : _helpers.GeneralPathWeb(userModel.AvatarUrl),
                //customerId =  userModel.CustomerId,
               /* accountId = userModel.AccountId,
                allowCreateCampaigns = (bool)userModel.AllowCreateCampaigns,
                allowCreateCustomers = (bool)userModel.AllowCreateCustomers,
                allowManageWorkflows = (bool)userModel.AllowManageWorkflows,
                targetType = userModel.TargetType*/
            };
          
            /*if (userModel.TargetId != null)
            {
                if (userModel.TargetId.Contains(","))
                {
                    currentSession.targetId = userModel.TargetId.Split(",").ToList();
                }
                else
                {
                    if (userModel.TargetType == "site")
                    {
                        currentSession.targetId = userModel.TargetId.Split(",").ToList();
                    }
                    else
                    {
                        currentSession.targetId = userModel.TargetId;
                    }

                }
            }
            */
            return currentSession;
        }

        public async Task UpdateProfile(Domain.Models.User model, IFormFile avatar)
        {
            try
            {
                //Validations                 
                var error = new ErrorDto();
                var arr = new List<ErrorDetailDto>();
                dynamic errorField = new System.Dynamic.ExpandoObject();
                var existErrors = false;

                if (string.IsNullOrEmpty(model.FirstName))
                {
                    var arrFirstName = new List<ErrorDetailDto>();
                    arrFirstName.Add(new ErrorDetailDto() { error = TokenError.Blank });
                    errorField.firstName = arrFirstName;
                    existErrors = true;
                }else if (model.FirstName.Length > 32) {
                    var arrFirstName = new List<ErrorDetailDto>();
                    arrFirstName.Add(new ErrorDetailDto() { error = TokenError.TooLong, count = model.FirstName.Length });
                    errorField.firstName = arrFirstName;
                    existErrors = true;
                }

                if (string.IsNullOrEmpty(model.LastName))
                {
                    var arrLastName = new List<ErrorDetailDto>();
                    arrLastName.Add(new ErrorDetailDto() { error = TokenError.Blank });
                    errorField.lastName = arrLastName;
                    existErrors = true;
                } else if (model.LastName.Length > 64)
                {
                    var arrLastName = new List<ErrorDetailDto>();
                    arrLastName.Add(new ErrorDetailDto() { error = TokenError.TooLong, count = model.LastName.Length });
                    errorField.lastName = arrLastName;
                    existErrors = true;
                }

                if (string.IsNullOrEmpty(model.Phone))
                {
                    var arrPhone = new List<ErrorDetailDto>();
                    arrPhone.Add(new ErrorDetailDto() { error = TokenError.Blank });
                    errorField.phone = arrPhone;
                    existErrors = true;
                }
                else
                {
                    Regex regex = new Regex(RegularExpression.Phone);
                    bool isValid = regex.IsMatch(model.Phone.Trim());
                    var arrPhone = new List<ErrorDetailDto>();
                    if (!isValid)
                    {
                        arrPhone.Add(new ErrorDetailDto() { error = TokenError.Format });
                        errorField.phone = arrPhone;
                        existErrors = true;
                    }

                    if (model.Phone.Length > 32)
                    {
                        arrPhone.Add(new ErrorDetailDto() { error = TokenError.TooLong, count = model.Phone.Length});
                        errorField.phone = arrPhone;
                        existErrors = true;
                    }                    
                }

                if (!string.IsNullOrEmpty(model.SecondPhone))
                {
                    Regex regex = new Regex(RegularExpression.Phone);
                    bool isValid = regex.IsMatch(model.SecondPhone.Trim());
                    var arrSecondPhone = new List<ErrorDetailDto>();
                    if (!isValid)
                    {
                        arrSecondPhone.Add(new ErrorDetailDto() { error = TokenError.Format });
                        errorField.secondPhone = arrSecondPhone;
                        existErrors = true;
                    }

                    if (model.SecondPhone.Length > 32)
                    {                       
                        arrSecondPhone.Add(new ErrorDetailDto() { error = TokenError.TooLong, count = model.SecondPhone.Length });
                        errorField.secondPhone = arrSecondPhone;
                        existErrors = true;
                    }
                }

             
                if (avatar != null)
                    if (avatar.Length > 0)
                    {
                        try
                        {
                            var allowedExtensions = new[] { ".jpg", ".png", ".jpeg" };

                            if(avatar.Length > (5 * 1024 * 1024) )
                            {
                                var arrAvatar = new List<ErrorDetailDto>();
                                arrAvatar.Add(new ErrorDetailDto() { error = TokenError.FileSizeOutOfRange});
                                errorField.avatar = arrAvatar;
                                existErrors = true;
                            }

                            if (!allowedExtensions.Contains(Path.GetExtension(avatar.FileName).ToLower()))
                            {
                               
                                    var arrAvatar = new List<ErrorDetailDto>();
                                    arrAvatar.Add(new ErrorDetailDto() { error = TokenError.ContentTypeInvalid , authorized_types= "png, jpg, jpeg" });
                                    errorField.avatar = arrAvatar;
                                    existErrors = true;                               
                            }
                            else
                            {
                                model.AvatarUrl = _helpers.UploadFile(avatar, model.Id.ToString(), "avatars");
                            }

                          
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }

                error.errors = errorField;
                if (existErrors)
                {
                    error.status = 422;
                    throw new InvalidDynamicCommandException(error);
                }



                var userModel = await _repository.UpdateProfile(model);               
            }
            catch (InvalidDynamicCommandException ex)
            {
                _logger.LogError("Method[UpdateProfile] - " + ex.Message);
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }          
        }

        public async Task UpdateProfilePatch(Domain.Models.User model, IFormFile avatar)
        {
            try
            {
                //Validations                 
                var error = new ErrorDto();
                var arr = new List<ErrorDetailDto>();
                dynamic errorField = new System.Dynamic.ExpandoObject();
                var existErrors = false;

                if (model.FirstName != null && model.FirstName == string.Empty)
                {
                    var arrfirstName = new List<ErrorDetailDto>();
                    arrfirstName.Add(new ErrorDetailDto() { error = TokenError.Blank });
                    errorField.firstName = arrfirstName;
                    existErrors = true;
                }
                else
                {
                    if (!string.IsNullOrEmpty(model.FirstName) && model.FirstName.Length > 32)
                    {
                        var arrFirstName = new List<ErrorDetailDto>();
                        arrFirstName.Add(new ErrorDetailDto() { error = TokenError.TooLong, count = model.FirstName.Length });
                        errorField.firstName = arrFirstName;
                        existErrors = true;
                    }
                }

                if (model.LastName != null && model.LastName == string.Empty)
                {
                    var arrLastName = new List<ErrorDetailDto>();
                    arrLastName.Add(new ErrorDetailDto() { error = TokenError.Blank });
                    errorField.lastName = arrLastName;
                    existErrors = true;
                }
                else
                {
                    if (!string.IsNullOrEmpty(model.LastName) && model.LastName.Length > 64)
                    {
                        var arrLastName = new List<ErrorDetailDto>();
                        arrLastName.Add(new ErrorDetailDto() { error = TokenError.TooLong, count = model.LastName.Length });
                        errorField.lastName = arrLastName;
                        existErrors = true;
                    }
                }

                if (model.Phone != null && model.Phone == string.Empty)
                {
                    var arrPhone = new List<ErrorDetailDto>();
                    arrPhone.Add(new ErrorDetailDto() { error = TokenError.Blank });
                    errorField.phone = arrPhone;
                    existErrors = true;
                }

             
                if (!string.IsNullOrEmpty(model.Phone))               
                {
                    Regex regex = new Regex(RegularExpression.Phone);
                    bool isValid = regex.IsMatch(model.Phone.Trim());
                    var arrPhone = new List<ErrorDetailDto>();
                    if (!isValid)
                    {
                        arrPhone.Add(new ErrorDetailDto() { error = TokenError.Format });
                        errorField.phone = arrPhone;
                        existErrors = true;
                    }

                    if (model.Phone.Length > 32)
                    {
                        arrPhone.Add(new ErrorDetailDto() { error = TokenError.TooLong, count = model.Phone.Length });
                        errorField.phone = arrPhone;
                        existErrors = true;
                    }
                }

                if (!string.IsNullOrEmpty(model.SecondPhone))
                {
                    Regex regex = new Regex(RegularExpression.Phone);
                    bool isValid = regex.IsMatch(model.SecondPhone.Trim());
                    var arrSecondPhone = new List<ErrorDetailDto>();
                    if (!isValid)
                    {
                        arrSecondPhone.Add(new ErrorDetailDto() { error = TokenError.Format });
                        errorField.secondPhone = arrSecondPhone;
                        existErrors = true;
                    }

                    if (model.SecondPhone.Length > 32)
                    {
                        arrSecondPhone.Add(new ErrorDetailDto() { error = TokenError.TooLong, count = model.SecondPhone.Length});
                        errorField.secondPhone = arrSecondPhone;
                        existErrors = true;
                    }
                }

                if (avatar != null)
                    if (avatar.Length > 0)
                    {
                        try
                        {
                            var allowedExtensions = new[] { ".jpg", ".png", ".jpeg" };

                            if (avatar.Length > (5 * 1024 * 1024))
                            {
                                var arrAvatar = new List<ErrorDetailDto>();
                                arrAvatar.Add(new ErrorDetailDto() { error = TokenError.FileSizeOutOfRange });
                                errorField.avatar = arrAvatar;
                                existErrors = true;
                            }

                            if (!allowedExtensions.Contains(Path.GetExtension(avatar.FileName).ToLower()))
                            {

                                var arrAvatar = new List<ErrorDetailDto>();
                                arrAvatar.Add(new ErrorDetailDto() { error = TokenError.ContentTypeInvalid, authorized_types = "png, jpg, jpeg" });
                                errorField.avatar = arrAvatar;
                                existErrors = true;
                            }
                            else
                            {
                                model.AvatarUrl = _helpers.UploadFile(avatar, model.Id.ToString(), "avatars");
                            }


                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }

                error.errors = errorField;
                if (existErrors)
                {
                    error.status = 422;
                    throw new InvalidDynamicCommandException(error);
                }



                var userModel = await _repository.UpdateProfilePatch(model);
            }
            catch (InvalidDynamicCommandException ex)
            {
                _logger.LogError("Method[UpdateProfile] - " + ex.Message);
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task UpdateProfileEmail(Domain.Models.User model)
        {
            try
            {
                //Validations                 
                var error = new ErrorDto();
                var arr = new List<ErrorDetailDto>();
                dynamic errorField = new System.Dynamic.ExpandoObject();
                var existErrors = false;

                var currentUser = await _repository.GetById(model.Id);

                if (string.IsNullOrEmpty(model.Email))
                {
                    var arrEmail = new List<ErrorDetailDto>();
                    arrEmail.Add(new ErrorDetailDto() { error = TokenError.Blank });
                    errorField.email = arrEmail;
                    existErrors = true;
                }
                else
                {
                    Regex regex = new Regex(RegularExpression.Email);
                    bool isValid = regex.IsMatch(model.Email.Trim());
                    if (!isValid)
                    {
                        arr = new List<ErrorDetailDto>();
                        arr.Add(new ErrorDetailDto() { error = TokenError.Format });
                        errorField.email = arr;
                        existErrors = true;
                    }
                    if (model.Email.Length > 254)
                    {
                        arr.Add(new ErrorDetailDto() { error = TokenError.TooLong, count = model.Email.Length });
                        errorField.email = arr;
                        existErrors = true;
                    }
                    var existeUser = await _repository.GetExistEmail(model.Id, model.Email.Trim());
                    if(existeUser!=null)
                    {
                        arr = new List<ErrorDetailDto>();
                        arr.Add(new ErrorDetailDto() { error = TokenError.Taken });
                        errorField.email = arr;
                        existErrors = true;
                    }

                    if(currentUser.Email==model.Email)
                    {
                        arr = new List<ErrorDetailDto>();
                        arr.Add(new ErrorDetailDto() { error = TokenError.Invalid });
                        errorField.email = arr;
                        existErrors = true;
                    }
                }

                if (string.IsNullOrEmpty(model.Password))
                {
                    var arrPassword = new List<ErrorDetailDto>();
                    arrPassword.Add(new ErrorDetailDto() { error = TokenError.Blank });
                    errorField.password = arrPassword;
                    existErrors = true;
                }
                else
                {

                    if (model.Password.Length > 72)
                    {
                        arr = new List<ErrorDetailDto>();
                        arr.Add(new ErrorDetailDto() { error = TokenError.TooLong, count = model.Password.Length });
                        errorField.password = arr;
                        existErrors = true;
                    }

                    var user = await _repository.GetById(model.Id);
                    if(user.Password!=Tools.GetMD5Hash(model.Password))
                    {
                        var arrPassword = new List<ErrorDetailDto>();
                        arrPassword.Add(new ErrorDetailDto() { error = TokenError.Invalid });
                        errorField.password = arrPassword;
                        existErrors = true;
                    }
                }

                error.errors = errorField;

                if (existErrors)
                {
                    error.status = 422;
                    throw new InvalidDynamicCommandException(error);
                }

                

                // create reset token expires after 1 day
                model.ResetTokenEmail = Tools.RandomToken();
                model.ResetTokenEmailExpire = DateTime.UtcNow.AddDays(1);
                model.ResetEmail = model.Email;
                var userModel = await _repository.UpdateProfileUnConfirmedEmail(model);

                // Send email Notice
                SendEmailChange(currentUser, "");
                currentUser.Email = model.Email;
                currentUser.ResetTokenEmail = model.ResetTokenEmail;
                SendEmailConfirmation(currentUser, "");

            }
            catch (InvalidDynamicCommandException ex)
            {
                _logger.LogError("Method[UpdateProfileEmail] - " + ex.Message);
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }          
        }

        public async Task UpdateProfileEmailByToken(string token)
        {
            try
            {

                //Validations                 
                var error = new ErrorDto();
                var arr = new List<ErrorDetailDto>();
                dynamic errorField = new System.Dynamic.ExpandoObject();

                var user = await _repository.GetByResetTokenEmail(token);

                if (user == null)
                {
                    arr = new List<ErrorDetailDto>();
                    arr.Add(new ErrorDetailDto() { error = TokenError.Invalid });
                    errorField.token = arr;

                    error.errors = errorField;
                    error.status = 422;
                    throw new InvalidDynamicCommandException(error);
                }


                user.Email = user.ResetEmail;                
                var userModel = await _repository.UpdateProfileEmail(user);

              

            }
            catch (InvalidDynamicCommandException ex)
            {
                _logger.LogError("Method[UpdateProfileEmailByToken] - " + ex.Message);
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<UserResponsePostDto> Insert(UserRequestPostDto model)
        {
            var error = new ErrorDto();
            var arr = new List<ErrorDetailDto>();
            dynamic errorField = new System.Dynamic.ExpandoObject();
            var existErrors = false;

            //Se valida si tiene permiso para crear el usuario
            /*var currentAccountId = model.currentAccountId;
            var currentRoleId = model.currentRoleId;


            bool isValidIdRole = _helpers.IsGuid(model.roleId.ToString());

            if(isValidIdRole)
            {

                if (currentRoleId == Constants.SuperAdmin)  //Only it can to create admin and operator users
                {
                    if (Guid.Parse(model.roleId) != Guid.Parse(Constants.Administrator.ToString()) && Guid.Parse(model.roleId) != Guid.Parse(Constants.Operator.ToString()))
                    {
                        var arrRoleId = new List<ErrorDetailDto>();
                        arrRoleId.Add(new ErrorDetailDto() { error = TokenError.Invalid });
                        errorField.roleId = arrRoleId;
                        existErrors = true;
                    }
                }

                if (currentRoleId == Constants.Administrator) //Only it can to create operator users
                {
                    if (Guid.Parse(model.roleId) != Guid.Parse(Constants.Operator.ToString()))
                    {
                        var arrRoleId = new List<ErrorDetailDto>();
                        arrRoleId.Add(new ErrorDetailDto() { error = TokenError.Invalid });
                        errorField.roleId = arrRoleId;
                        existErrors = true;
                    }
                }

                if (currentRoleId == Constants.Operator) //it can't to create any users
                {
                    var arrRoleId = new List<ErrorDetailDto>();
                    arrRoleId.Add(new ErrorDetailDto() { error = TokenError.Invalid });
                    errorField.roleId = arrRoleId;
                    existErrors = true;
                }

            }
            else
            {

                var arrRoleId = new List<ErrorDetailDto>
                        {
                            new ErrorDetailDto() { error = TokenError.Format }
                        };
                errorField.roleId = arrRoleId;
                existErrors = true;
            }
           
            */
            if (string.IsNullOrEmpty(model.email))
            {
                var arrEmail = new List<ErrorDetailDto>();
                arrEmail.Add(new ErrorDetailDto() { error = TokenError.Blank });
                errorField.email = arrEmail;
                existErrors = true;
            }
            else
            {

                if (model.email.Length > 254)
                {
                    arr = new List<ErrorDetailDto>();
                    arr.Add(new ErrorDetailDto() { error = TokenError.TooLong, count = model.email.Length });
                    errorField.email = arr;
                    existErrors = true;
                }

                Regex regex = new Regex(RegularExpression.Email);
                bool isValid = regex.IsMatch(model.email.Trim());
                if (!isValid)
                {
                    arr = new List<ErrorDetailDto>();
                    arr.Add(new ErrorDetailDto() { error = TokenError.Format });
                    errorField.email = arr;
                    existErrors = true;
                }

                var userExist = await _repository.GetByEmail(model.email);
                if (userExist != null)
                {
                    arr.Add(new ErrorDetailDto() { error = TokenError.Taken });
                    errorField.email = arr;
                    existErrors = true;
                }
            }

            if (string.IsNullOrEmpty(model.firstName))
            {
                var arrfirstName = new List<ErrorDetailDto>();
                arrfirstName.Add(new ErrorDetailDto() { error = TokenError.Blank });
                errorField.firstName = arrfirstName;
                existErrors = true;
            }
            else if (model.firstName.Length > 32)
                {
                    var arrfirstName = new List<ErrorDetailDto>();
                    arrfirstName.Add(new ErrorDetailDto() { error = TokenError.TooLong, count = model.firstName.Length });
                    errorField.firstName = arrfirstName;
                    existErrors = true;
                }

            if (string.IsNullOrEmpty(model.lastName))
            {
                var arrlastName = new List<ErrorDetailDto>();
                arrlastName.Add(new ErrorDetailDto() { error = TokenError.Blank });
                errorField.lastName = arrlastName;
                existErrors = true;
            }
            else if (model.lastName.Length > 64)
            {
                var arrlastName = new List<ErrorDetailDto>();
                arrlastName.Add(new ErrorDetailDto() { error = TokenError.TooLong, count = model.lastName.Length });
                errorField.lastName = arrlastName;
                existErrors = true;
            }


            if (!string.IsNullOrEmpty(model.customerId))
            {
               /* Guid newGuid;
                if (Guid.TryParse(model.customerId, out newGuid) == true)
                {
                    var customer = await _customerRepository.GetById(Guid.Parse(model.customerId));
                    if (customer == null)
                    {
                        var arrClientId = new List<ErrorDetailDto>();
                        arrClientId.Add(new ErrorDetailDto() { error = TokenError.Invalid });
                        errorField.customerId = arrClientId;
                        existErrors = true;
                    }
                }
                else
                {
                    var arrClientId = new List<ErrorDetailDto>();
                    arrClientId.Add(new ErrorDetailDto() { error = TokenError.Format });
                    errorField.customerId = arrClientId;
                    existErrors = true;

                }*/
                
            }

            if (!string.IsNullOrEmpty(model.targetType))
            {

                var typTarget = model.targetId.GetType();
                if (typTarget.Name != "JArray")
                {
                    if (string.IsNullOrEmpty(model.targetId.ToString()))
                    {
                        var arrtargetId = new List<ErrorDetailDto>();
                        arrtargetId.Add(new ErrorDetailDto() { error = TokenError.Blank });
                        errorField.targetId = arrtargetId;
                        existErrors = true;
                    }
                }
                else
                {
                    if(model.targetId==null)
                    {
                        var arrtargetId = new List<ErrorDetailDto>();
                        arrtargetId.Add(new ErrorDetailDto() { error = TokenError.Blank });
                        errorField.targetId = arrtargetId;
                        existErrors = true;
                    }
                }

               

            }

            /*if (string.IsNullOrEmpty(model.roleId))
            {
                var arrRole = new List<ErrorDetailDto>();
                arrRole.Add(new ErrorDetailDto() { error = TokenError.Blank });
                errorField.roleId = arrRole;
                existErrors = true;
            }
            else
            {
                Guid newGuid;
                if (Guid.TryParse(model.roleId, out newGuid) == true)
                {
                   /* var role = await _roleRepository.GetById(Guid.Parse(model.roleId));
                    if (role != null) //If invalid role
                    {
                        if (role.Name == Roles.Operador || role.Name == Roles.Admin)
                        {
                            if (string.IsNullOrEmpty(model.customerId))
                            {
                                var arrClientId = new List<ErrorDetailDto>();
                                arrClientId.Add(new ErrorDetailDto() { error = TokenError.Blank });
                                errorField.customerId = arrClientId;
                                existErrors = true;
                            }
                            else
                            {
                                var customer = await _customerRepository.GetById(Guid.Parse(model.customerId));
                                if (customer == null)
                                {
                                    var arrClientId = new List<ErrorDetailDto>();
                                    arrClientId.Add(new ErrorDetailDto() { error = TokenError.Invalid });
                                    errorField.customerId = arrClientId;
                                    existErrors = true;
                                }
                            }
                        }

                    }
                    else
                    {
                        var arrRole = new List<ErrorDetailDto>();
                        arrRole.Add(new ErrorDetailDto() { error = TokenError.Invalid });
                        errorField.roleId = arrRole;
                        existErrors = true;

                    }
                }
                else
                {
                    var arrRole = new List<ErrorDetailDto>();
                    arrRole.Add(new ErrorDetailDto() { error = TokenError.Format });
                    errorField.roleId = arrRole;
                    existErrors = true;

                }    */                        

            //}

            error.errors = errorField;

            if (existErrors)
            {
                error.status = 422;
                throw new InvalidDynamicCommandException(error);
            }

            var entity = new Domain.Models.User();
            entity.Id = Guid.NewGuid();
            entity.FirstName = model.firstName;
            entity.LastName = model.lastName;
            entity.Email = model.email.ToLower();
            /*entity.CustomerId = string.IsNullOrEmpty(model.customerId) ? entity.CustomerId : Guid.Parse(model.customerId);
            entity.RoleId = Guid.Parse(model.roleId);*/
            entity.CreatedAt = DateTime.UtcNow;
            entity.CreatedBy = model.createdBy;
            entity.Status = 1;
            /*entity.AllowCreateCampaigns = model.allowCreateCampaigns;
            entity.AllowCreateCustomers = model.allowCreateCustomers;
            entity.AllowManageWorkflows = model.allowManageWorkflows;
            entity.TargetType = model.targetType;
            */
            var typ = model.targetId.GetType();
            /*if (typ.Name == "JArray")
            {
                var targetIdArray = (JArray)model.targetId;
                var targetIds = targetIdArray.ToObject<List<string>>();
                entity.TargetId = string.Join(",", targetIds);
            }
            else
            {
                entity.TargetId = model.targetId.ToString();
            }*/
            var passwordGenerated = Tools.GeneratePassword(true, true, true, true, 16);
            entity.Password = Tools.GetMD5Hash(passwordGenerated);
            entity.PasswordLastChange = DateTime.UtcNow;

            //Set AccountId from User           
           // entity.AccountId = currentAccountId;

            var userModel = await _repository.Insert(entity);

           /* var account = await _accountRepository.GetById(entity.AccountId, currentAccountId);
            userModel.Account = account;*/

            UserResponsePostDto user = new UserResponsePostDto
            {
                id = userModel.Id.ToString(),
                firstName = userModel.FirstName,
                lastName = userModel.LastName,
                email = userModel.Email.ToLower(),                
                /*customerId = userModel.CustomerId == null ? null : userModel.CustomerId.ToString(),
                roleId = userModel.RoleId.ToString()*/
            };


            //Send Email
            userModel.Password = passwordGenerated;
            SendEmailUserCreated(userModel, "");
            //Verificamos si fue creado y lo creamos posterior en el tracking 
            // var customer = await _customerService.GetById((Guid)model.CustomerId);
            //await _repositorytracking.AddUser(model, customer.MatomoSiteId);
            return user;

        }


        public async Task<Domain.Models.User> Delete(Guid id, Guid currentAccountId, Guid currentRoleId)
        {

            //Set AccountId from User 
         

            var userExist = new Domain.Models.User();

            userExist = null;

            if (currentRoleId == Constants.SuperAdmin)
            {
                userExist = await _repository.GetByIdAllUsers(id, currentAccountId);
            }

            if (currentRoleId == Constants.Administrator)
            {
                userExist = await _repository.GetByIdOperators(id, currentAccountId);
            }

            var error = new ErrorDto();           
            dynamic errorField = new System.Dynamic.ExpandoObject();
           
            if(userExist==null)
            {
                var arrRole = new List<ErrorDetailDto>();
                arrRole.Add(new ErrorDetailDto() { error = TokenError.Invalid });
                errorField.id = arrRole;               
                error.errors = errorField;
                error.status = 422;
                throw new InvalidDynamicCommandException(error);
            }
            var user =  await _repository.Delete(id);
            //await _repositorytracking.DeleteUser(user);
            return user;
        }

        public async Task<ICollection<UserResponseGetDto>> Get()
        {
            var users = await _repository.Get();
            var listUsers = users.Select(u => new UserResponseGetDto
            {
                id = u.Id,
                firstName = u.FirstName,
                lastName = u.LastName,
                email = u.Email,
                phone = String.IsNullOrEmpty(u.Phone) ? string.Empty : u.Phone,
                status = (int)u.Status,
               /* roleId = u.RoleId,
                customerId = (Guid)u.CustomerId,*/
                avatarUrl = String.IsNullOrEmpty(u.AvatarUrl) ? string.Empty : _helpers.GeneralPathWeb(u.AvatarUrl)
            }).ToList();

            return listUsers;
        }

        public async Task<PagedList<UserResponseGetDto>> GetUsersByFilter(UserFilterDto userFilter)
        {
            //Set AccountId from User 
            var currentAccountId  = userFilter.currentAccountId;
            var currentRoleId = userFilter.currentRoleId;

            var users =new List<Domain.Models.User>();

            if (currentRoleId == Constants.SuperAdmin)
            {
                users = (List<Domain.Models.User>)await _repository.GetAllUsers(currentAccountId);
            }

            if (currentRoleId == Constants.Administrator)
            {
                users = (List<Domain.Models.User>) await _repository.GetOperators(currentAccountId);               
            }           

            if (userFilter.filter != null)
            {
                //Filters
                if (userFilter.filter.ContainsKey("firstName") && !string.IsNullOrEmpty(userFilter.filter["firstName"]))
                {
                    var value = userFilter.filter["firstName"].ToString().ToUpper();
                    users = (from c in users
                             where RegularExpression.RemoveAccents(c.FirstName).ToUpper().Contains(RegularExpression.RemoveAccents(value))
                             select c).ToList();
                }

                if (userFilter.filter.ContainsKey("fullName") && !string.IsNullOrEmpty(userFilter.filter["fullName"]))
                {
                    var value = userFilter.filter["fullName"].ToString().ToUpper();
                    users = (from c in users
                             where RegularExpression.RemoveAccents(c.FullName).ToUpper().Contains(RegularExpression.RemoveAccents(value))
                             select c).ToList();
                }

                if (userFilter.filter.ContainsKey("lastName") && !string.IsNullOrEmpty(userFilter.filter["lastName"]))
                {
                    var value = userFilter.filter["lastName"].ToString().ToUpper();
                    users = (from c in users
                             where RegularExpression.RemoveAccents(c.LastName).ToUpper().Contains(RegularExpression.RemoveAccents(value))
                             select c).ToList();
                }
                if (userFilter.filter.ContainsKey("phone") && !string.IsNullOrEmpty(userFilter.filter["phone"]))
                {
                    var value = userFilter.filter["phone"].ToString().ToUpper();
                    users = (from c in users
                             where RegularExpression.RemoveAccents(c.Phone).ToUpper().Contains(RegularExpression.RemoveAccents(value))
                             select c).ToList();
                }
                if (userFilter.filter.ContainsKey("email") && !string.IsNullOrEmpty(userFilter.filter["email"]))
                {
                    var value = userFilter.filter["email"].ToString().ToUpper();
                    users = (from c in users
                             where RegularExpression.RemoveAccents(c.Email).ToUpper().Contains(RegularExpression.RemoveAccents(value))
                             select c).ToList();
                }

            }

            //Se cambio de los datos a las propiedades de respuesta
            var userResponses = new List<UserResponseGetDto>();
            foreach (var user in users)
            {
                userResponses.Add(SetUserResponse(user));
            }

            //pagination And ordering
            var pagedUsers = PagedList<UserResponseGetDto>.Create(userResponses, userFilter.page, userFilter.order);
            return pagedUsers;
        }

        public async Task<PagedList<UserResponseToAssignGetDto>> GetUsersToAssign(UserFilterDto userFilter)
        {
            //Set AccountId from User 
            var currentAccountId = userFilter.currentAccountId;
            var currentRoleId = userFilter.currentRoleId;

            var users = new List<Domain.Models.User>();

            var admins = (List<Domain.Models.User>)await _repository.GetAdministrators(currentAccountId);

            var operators = (List<Domain.Models.User>)await _repository.GetOperators(currentAccountId);

            if(admins!=null)
                 users.AddRange(admins);
            if (operators != null)
                users.AddRange(operators);

            users = users.OrderBy(x => x.FirstName).ToList();

            if (userFilter.filter != null)
            {
                //Filters
                if (userFilter.filter.ContainsKey("firstName") && !string.IsNullOrEmpty(userFilter.filter["firstName"]))
                {
                    var value = userFilter.filter["firstName"].ToString().ToUpper();
                    users = (from c in users
                             where RegularExpression.RemoveAccents(c.FirstName).ToUpper().Contains(RegularExpression.RemoveAccents(value))
                             select c).ToList();
                }

                if (userFilter.filter.ContainsKey("fullName") && !string.IsNullOrEmpty(userFilter.filter["fullName"]))
                {
                    var value = userFilter.filter["fullName"].ToString().ToUpper();
                    users = (from c in users
                             where RegularExpression.RemoveAccents(c.FullName).ToUpper().Contains(RegularExpression.RemoveAccents(value))
                             select c).ToList();
                }

                if (userFilter.filter.ContainsKey("lastName") && !string.IsNullOrEmpty(userFilter.filter["lastName"]))
                {
                    var value = userFilter.filter["lastName"].ToString().ToUpper();
                    users = (from c in users
                             where RegularExpression.RemoveAccents(c.LastName).ToUpper().Contains(RegularExpression.RemoveAccents(value))
                             select c).ToList();
                }
                if (userFilter.filter.ContainsKey("phone") && !string.IsNullOrEmpty(userFilter.filter["phone"]))
                {
                    var value = userFilter.filter["phone"].ToString().ToUpper();
                    users = (from c in users
                             where RegularExpression.RemoveAccents(c.Phone).ToUpper().Contains(RegularExpression.RemoveAccents(value))
                             select c).ToList();
                }
                if (userFilter.filter.ContainsKey("email") && !string.IsNullOrEmpty(userFilter.filter["email"]))
                {
                    var value = userFilter.filter["email"].ToString().ToUpper();
                    users = (from c in users
                             where RegularExpression.RemoveAccents(c.Email).ToUpper().Contains(RegularExpression.RemoveAccents(value))
                             select c).ToList();
                }

            }

            //Se cambio de los datos a las propiedades de respuesta
            var userResponses = new List<UserResponseToAssignGetDto>();
            foreach (var user in users)
            {
                userResponses.Add(SetUserResponseToAssign(user));
            }

            //pagination And ordering
            var pagedUsers = PagedList<UserResponseToAssignGetDto>.Create(userResponses, userFilter.page, userFilter.order);
            return pagedUsers;
        }

        public async Task<UserResponseGetDto> GetById(Guid id, Guid currentAccountId, Guid currentRoleId)
        {           
            try
            {                         

                var userModel = new Domain.Models.User();
                
                userModel = null;

                if (currentRoleId == Constants.SuperAdmin)
                {
                    userModel = await _repository.GetByIdAllUsers(id,currentAccountId);
                }

                if (currentRoleId == Constants.Administrator)
                {
                    userModel = await _repository.GetByIdOperatorsAdministrators(id,currentAccountId);
                }

                if (currentRoleId == Constants.Operator)
                {
                    userModel = await _repository.GetByIdOperatorsAdministrators(id, currentAccountId);
                }

                var error = new ErrorDto();
                dynamic errorField = new System.Dynamic.ExpandoObject();

                if (userModel == null)
                {
                    var arr = new List<ErrorDetailDto>();
                    arr.Add(new ErrorDetailDto() { error = TokenError.Invalid });
                    errorField.id = arr;

                    error.errors = errorField;
                    error.status = 422;
                    throw new InvalidDynamicCommandException(error);

                }
                var user = new UserResponseGetDto
                {
                    id = userModel.Id,
                    firstName = userModel.FirstName,
                    lastName = userModel.LastName,
                    email = userModel.Email,
                    phone = String.IsNullOrEmpty(userModel.Phone) ? string.Empty : userModel.Phone,
                    status = (int)userModel.Status,
                    /*roleId = userModel.RoleId,
                    customerId = userModel.CustomerId == null ? null : (Guid?)userModel.CustomerId,*/
                    avatarUrl = String.IsNullOrEmpty(userModel.AvatarUrl) ? string.Empty : _helpers.GeneralPathWeb(userModel.AvatarUrl),
                    /*allowCreateCampaigns = (bool)userModel.AllowCreateCampaigns,
                    allowCreateCustomers = (bool)userModel.AllowCreateCustomers,
                    allowManageWorkflows = (bool)userModel.AllowManageWorkflows,
                    targetType = userModel.TargetType
                    */
                };
                /*if(userModel.TargetId!=null)
                {
                    if (userModel.TargetId.Contains(","))
                    {
                        user.targetId = userModel.TargetId.Split(",").ToList();
                    }
                    else
                    {
                        if(userModel.TargetType == "site")
                        {
                            user.targetId = userModel.TargetId.Split(",").ToList();
                        }
                        else
                        {
                            user.targetId = userModel.TargetId;
                        }
                       
                    }
                }*/
                
                

                return user;

            }
            catch (InvalidDynamicCommandException ex)
            {
                _logger.LogError("Method[GetById] - " + ex.Message);
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<UserResponsePutDto> Update(UserRequestPutDto model,Guid currentAccountId, Guid currentRoleId)
        {
            //Set AccountId from User 
          
            var existEntity = new Domain.Models.User();

            existEntity = null;

            if (currentRoleId == Constants.SuperAdmin)
            {
                existEntity = await _repository.GetByIdAllUsers(model.id, currentAccountId);
            }

            if (currentRoleId == Constants.Administrator)
            {
                existEntity = await _repository.GetByIdOperators(model.id, currentAccountId);
            }

            var error = new ErrorDto();
            var arr = new List<ErrorDetailDto>();
            dynamic errorField = new System.Dynamic.ExpandoObject();
            var existErrors = false;

            if (existEntity == null)
            {
                arr = new List<ErrorDetailDto>();
                arr.Add(new ErrorDetailDto() { error = TokenError.Invalid });
                errorField.id = arr;
                existErrors = true;
            }

            if (string.IsNullOrEmpty(model.email))
            {
                var arrEmail = new List<ErrorDetailDto>();
                arrEmail.Add(new ErrorDetailDto() { error = TokenError.Blank });
                errorField.email = arrEmail;
                existErrors = true;
            }
            else
            {
                Regex regex = new Regex(RegularExpression.Email);
                bool isValid = regex.IsMatch(model.email.Trim());
                if (!isValid)
                {
                    arr = new List<ErrorDetailDto>();
                    arr.Add(new ErrorDetailDto() { error = TokenError.Format });
                    errorField.email = arr;
                    existErrors = true;
                }

                if (model.email.Length > 254)
                {
                    arr = new List<ErrorDetailDto>();
                    arr.Add(new ErrorDetailDto() { error = TokenError.TooLong, count = model.email.Length });
                    errorField.email = arr;
                    existErrors = true;
                }

                var existeUser = await _repository.GetExistEmail(model.id, model.email.Trim());
                if (existeUser != null)
                {
                    arr = new List<ErrorDetailDto>();
                    arr.Add(new ErrorDetailDto() { error = TokenError.Taken });
                    errorField.email = arr;
                    existErrors = true;
                }
            }

            if (string.IsNullOrEmpty(model.firstName))
            {
                var arrfirstName = new List<ErrorDetailDto>();
                arrfirstName.Add(new ErrorDetailDto() { error = TokenError.Blank });
                errorField.firstName = arrfirstName;
                existErrors = true;
            }
            else if (model.firstName.Length > 32)
            {
                var arrfirstName = new List<ErrorDetailDto>();
                arrfirstName.Add(new ErrorDetailDto() { error = TokenError.TooLong, count = model.firstName.Length });
                errorField.firstName = arrfirstName;
                existErrors = true;
            }

            if (string.IsNullOrEmpty(model.lastName))
            {
                var arrlastName = new List<ErrorDetailDto>();
                arrlastName.Add(new ErrorDetailDto() { error = TokenError.Blank });
                errorField.lastName = arrlastName;
                existErrors = true;
            }
            else if (model.lastName.Length > 64)
            {
                var arrlastName = new List<ErrorDetailDto>();
                arrlastName.Add(new ErrorDetailDto() { error = TokenError.TooLong, count = model.lastName.Length });
                errorField.lastName = arrlastName;
                existErrors = true;
            }


            if (!string.IsNullOrEmpty(model.customerId))
            {
                /*Guid newGuid;
                if (Guid.TryParse(model.customerId, out newGuid) == true)
                {
                    var customer = await _customerRepository.GetById(Guid.Parse(model.customerId));
                    if (customer == null)
                    {
                        var arrClientId = new List<ErrorDetailDto>();
                        arrClientId.Add(new ErrorDetailDto() { error = TokenError.Invalid });
                        errorField.customerId = arrClientId;
                        existErrors = true;
                    }
                }
                else
                {
                    var arrClientId = new List<ErrorDetailDto>();
                    arrClientId.Add(new ErrorDetailDto() { error = TokenError.Format });
                    errorField.customerId = arrClientId;
                    existErrors = true;

                }*/

            }

            if (string.IsNullOrEmpty(model.roleId))
            {
                var arrRole = new List<ErrorDetailDto>();
                arrRole.Add(new ErrorDetailDto() { error = TokenError.Blank });
                errorField.roleId = arrRole;
                existErrors = true;
            }
            else
            {
                Guid newGuid;
                if (Guid.TryParse(model.roleId, out newGuid) == true)
                {
                   /* var role = await _roleRepository.GetById(Guid.Parse(model.roleId));
                    if (role != null) //If invalid role
                    {
                        if (role.Name == Roles.Operador || role.Name == Roles.Admin)
                        {
                            if (string.IsNullOrEmpty(model.customerId))
                            {
                                var arrClientId = new List<ErrorDetailDto>();
                                arrClientId.Add(new ErrorDetailDto() { error = TokenError.Blank });
                                errorField.customerId = arrClientId;
                                existErrors = true;
                            }
                            else
                            {
                                var customer = await _customerRepository.GetById(Guid.Parse(model.customerId));
                                if (customer == null)
                                {
                                    var arrClientId = new List<ErrorDetailDto>();
                                    arrClientId.Add(new ErrorDetailDto() { error = TokenError.Invalid });
                                    errorField.customerId = arrClientId;
                                    existErrors = true;
                                }
                            }
                        }

                    }
                    else
                    {
                        var arrRole = new List<ErrorDetailDto>();
                        arrRole.Add(new ErrorDetailDto() { error = TokenError.Invalid });
                        errorField.roleId = arrRole;
                        existErrors = true;

                    }*/
                }
                else
                {
                    var arrRole = new List<ErrorDetailDto>();
                    arrRole.Add(new ErrorDetailDto() { error = TokenError.Format });
                    errorField.roleId = arrRole;
                    existErrors = true;

                }

            }

            error.errors = errorField;

            if (existErrors)
            {
                error.status = 422;
                throw new InvalidDynamicCommandException(error);
            }

            var entity = new Domain.Models.User();
            entity.Id = model.id;
            entity.FirstName = model.firstName;
            entity.LastName = model.lastName;
            entity.Email = model.email;
            /*entity.CustomerId = string.IsNullOrEmpty(model.customerId) ? entity.CustomerId : Guid.Parse(model.customerId);
            entity.RoleId = Guid.Parse(model.roleId);*/
            entity.UpdatedAt = DateTime.UtcNow;
            entity.ModifiedBy = model.modifiedBy;
            /*entity.AllowCreateCampaigns = model.allowCreateCampaigns;
            entity.AllowCreateCustomers = model.allowCreateCustomers;
            entity.AllowManageWorkflows = model.allowManageWorkflows;
            entity.TargetType = model.targetType;
            */
            var typ = model.targetId.GetType();
            /*if(typ.Name == "JArray")
            {
                var targetIdArray = (JArray)model.targetId;
                var targetIds = targetIdArray.ToObject<List<string>>();
                entity.TargetId = string.Join(",", targetIds);
            }
            else
            {
                entity.TargetId = model.targetId.ToString();
            }*/
            var userModel = await _repository.Update(entity);

            var userUpdated = new UserResponsePutDto
            {               
                firstName = userModel.FirstName,
                lastName = userModel.LastName,
                email = userModel.Email,
                /*customerId = userModel.CustomerId.ToString(),
                roleId = userModel.RoleId.ToString()*/
            };


            return userUpdated;
        }


        public async Task<UserResponsePutDto> Patch(UserRequestPutDto model, Guid currentAccountId, Guid currentRoleId)
        {

            //Set AccountId from User 
            var existEntity = new Domain.Models.User();

            existEntity = null;

            if (currentRoleId == Constants.SuperAdmin)
            {
                existEntity = await _repository.GetByIdAllUsers(model.id, currentAccountId);
            }

            if (currentRoleId == Constants.Administrator)
            {
                existEntity = await _repository.GetByIdOperators(model.id, currentAccountId);
            }


            var error = new ErrorDto();
            var arr = new List<ErrorDetailDto>();
            dynamic errorField = new System.Dynamic.ExpandoObject();
            var existErrors = false;
           
            if(existEntity == null)
            {
                arr = new List<ErrorDetailDto>();
                arr.Add(new ErrorDetailDto() { error = TokenError.Invalid });
                errorField.id = arr;
                existErrors = true;
            }

            if (model.firstName!=null && model.firstName==string.Empty)
            {
                var arrfirstName = new List<ErrorDetailDto>();
                arrfirstName.Add(new ErrorDetailDto() { error = TokenError.Blank });
                errorField.firstName = arrfirstName;
                existErrors = true;
            }
           
            if (!string.IsNullOrEmpty(model.firstName) && model.firstName.Length > 32)
            {
                var arrfirstName = new List<ErrorDetailDto>();
                arrfirstName.Add(new ErrorDetailDto() { error = TokenError.TooLong, count = model.firstName.Length });
                errorField.firstName = arrfirstName;
                existErrors = true;
            }

            if (model.lastName != null && model.lastName == string.Empty)
            {
                var arrlastName = new List<ErrorDetailDto>();
                arrlastName.Add(new ErrorDetailDto() { error = TokenError.Blank });
                errorField.lastName = arrlastName;
                existErrors = true;
            }
            
            if (!string.IsNullOrEmpty(model.lastName) &&  model.lastName.Length > 32)
            {
                var arrlastName = new List<ErrorDetailDto>();
                arrlastName.Add(new ErrorDetailDto() { error = TokenError.TooLong, count = model.lastName.Length });
                errorField.lastName = arrlastName;
                existErrors = true;
            }

            if (model.email != null &&  model.email == string.Empty)
            {
                var arrEmail = new List<ErrorDetailDto>();
                arrEmail.Add(new ErrorDetailDto() { error = TokenError.Blank });
                errorField.email = arrEmail;
                existErrors = true;
            }
            

            if (model.roleId != null && model.roleId == string.Empty)
            {
                var arrRoleId = new List<ErrorDetailDto>();
                arrRoleId.Add(new ErrorDetailDto() { error = TokenError.Blank });
                errorField.roleId = arrRoleId;
                existErrors = true;
            }

            if (model.customerId != null && model.customerId == string.Empty)
            {
                var arrCustomerId = new List<ErrorDetailDto>();
                arrCustomerId.Add(new ErrorDetailDto() { error = TokenError.Blank });
                errorField.customerId = arrCustomerId;
                existErrors = true;
            }

            if (!string.IsNullOrEmpty(model.email))
            {

                var arrEmail = new List<ErrorDetailDto>();

                if (model.email.Length > 254)
                {
                   
                    arrEmail.Add(new ErrorDetailDto() { error = TokenError.TooLong, count = model.email.Length });
                    errorField.email = arrEmail;
                    existErrors = true;
                }

                Regex regex = new Regex(RegularExpression.Email);
                bool isValid = regex.IsMatch(model.email.Trim());
                if (!isValid)
                {

                    arrEmail.Add(new ErrorDetailDto() { error = TokenError.Format });
                    errorField.email = arrEmail;
                    existErrors = true;
                }

                var existeUser = await _repository.GetExistEmail(model.id, model.email.Trim());
                if (existeUser != null)
                {                   
                    arrEmail.Add(new ErrorDetailDto() { error = TokenError.Taken });
                    errorField.email = arrEmail;
                    existErrors = true;
                }
            }

          

            if (!string.IsNullOrEmpty(model.customerId))
            {
               /* Guid newGuid;
                if (Guid.TryParse(model.customerId, out newGuid) == true)
                {
                    var customer = await _customerRepository.GetById(Guid.Parse(model.customerId));
                    if (customer == null)
                    {
                        var arrClientId = new List<ErrorDetailDto>();
                        arrClientId.Add(new ErrorDetailDto() { error = TokenError.Invalid });
                        errorField.customerId = arrClientId;
                        existErrors = true;
                    }
                }
                else
                {
                    var arrClientId = new List<ErrorDetailDto>();
                    arrClientId.Add(new ErrorDetailDto() { error = TokenError.Format });
                    errorField.customerId = arrClientId;
                    existErrors = true;

                }*/

            }

            
            if (!string.IsNullOrEmpty(model.roleId))
            {
                Guid newGuid;
                if (Guid.TryParse(model.roleId, out newGuid) == true)
                {
                   /* var role = await _roleRepository.GetById(Guid.Parse(model.roleId));
                    if (role != null) //If invalid role
                    {
                        if (role.Name == Roles.Operador || role.Name == Roles.Admin)
                        {
                            if (!string.IsNullOrEmpty(model.customerId))                           
                            {
                                var customer = await _customerRepository.GetById(Guid.Parse(model.customerId));
                                if (customer == null)
                                {
                                    var arrClientId = new List<ErrorDetailDto>();
                                    arrClientId.Add(new ErrorDetailDto() { error = TokenError.Invalid });
                                    errorField.customerId = arrClientId;
                                    existErrors = true;
                                }
                            }
                        }

                    }
                    else
                    {
                        var arrRole = new List<ErrorDetailDto>();
                        arrRole.Add(new ErrorDetailDto() { error = TokenError.Invalid });
                        errorField.roleId = arrRole;
                        existErrors = true;

                    }*/
                }
                else
                {
                    var arrRole = new List<ErrorDetailDto>();
                    arrRole.Add(new ErrorDetailDto() { error = TokenError.Format });
                    errorField.roleId = arrRole;
                    existErrors = true;

                }

            }

            error.errors = errorField;

            if (existErrors)
            {
                error.status = 422;
                throw new InvalidDynamicCommandException(error);
            }

            var entity = new Domain.Models.User();
            entity.Id = model.id;
            entity.FirstName = model.firstName;
            entity.LastName = model.lastName;
            entity.Email = model.email;
            /*entity.AllowCreateCampaigns = model.allowCreateCampaigns;
            entity.AllowCreateCustomers = model.allowCreateCustomers;
            entity.AllowManageWorkflows = model.allowManageWorkflows;
            entity.TargetType = model.targetType;
            */
            var typ = model.targetId.GetType();
            /*if (typ.Name == "JArray")
            {
                var targetIdArray = (JArray)model.targetId;
                var targetIds = targetIdArray.ToObject<List<string>>();
                entity.TargetId = string.Join(",", targetIds);
            }
            else
            {
                entity.TargetId = model.targetId.ToString();
            }*/

            //entity.CustomerId = string.IsNullOrEmpty(model.customerId) ? entity.CustomerId : Guid.Parse(model.customerId);
            /*if(!string.IsNullOrEmpty(model.roleId))
               entity.RoleId = Guid.Parse(model.roleId);*/
            entity.UpdatedAt = DateTime.UtcNow;
            entity.ModifiedBy = model.modifiedBy;
            var userModel = await _repository.Patch(entity);

            var userUpdated = new UserResponsePutDto
            {
                firstName = userModel.FirstName,
                lastName = userModel.LastName,
                email = userModel.Email,
                /*customerId = userModel.CustomerId.ToString(),
                roleId = userModel.RoleId.ToString()*/
            };


            return userUpdated;
        }

        public async Task<Domain.Models.User> GetByLogin(string name)
        {
            return await _repository.GetByLogin(name);
        }

        public async Task<bool> AuthUser(string username, string password, int usertype)
        {

            try
            {
                bool resultado = false;
                if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
                {                  
                    resultado = await _repository.AuthUser(username, password, usertype);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                _logger.LogError("Method[AuthUser] - " + ex.Message);
                throw ex;
            }
           
        }

        public async Task<Domain.Models.User> GetByUserPass(string username, string password, int usertype)
        {
            try
            {
               
                if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
                {
                   return await _repository.GetByUserPass(username, password, usertype);
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError("Method[GetByUserPass] - " + ex.Message);
                throw ex;
            }

        }

        public async Task<ICollection<UserResponseGetDto>> GetByRole(Guid roleId)
        {
            try
            {
                if (roleId != null)
                {
                    
                    var users = await _repository.GetByRoleId(roleId);
                    var listUsers = users.Select(u => new UserResponseGetDto
                    {
                        id = u.Id,
                        firstName = u.FirstName,
                        lastName = u.LastName,
                        email = u.Email,
                        phone = String.IsNullOrEmpty(u.Phone) ? string.Empty : u.Phone,
                        status = (int)u.Status,
                        /*roleId = u.RoleId,
                        customerId = (Guid)u.CustomerId,*/
                        avatarUrl = String.IsNullOrEmpty(u.AvatarUrl) ? string.Empty : _helpers.GeneralPathWeb(u.AvatarUrl)
                    }).ToList();

                    return listUsers;
                }
                return null;
            }
            catch(Exception ex)
            {
                _logger.LogError("Method[GetByRoleId] - " + ex.Message);
                throw ex;
            }
        }

        public async Task<PagedList<UserResponseGetDto>> GetUsersByRoleFilter(Guid roleId, RoleFilterDto roleFilter)
        {

            //Set AccountId from User 
            var currentRoleId = roleFilter.currentRoleId;
            var currentAccountId = roleFilter.currentAccountId;

            var users = new List<Domain.Models.User>();

            if (currentRoleId == Constants.SuperAdmin && roleId != Constants.SuperAdmin) // Only Administrators or operators
            {
                users = (List<Domain.Models.User>)await _repository.GetByRoleIdByAccount(roleId,currentAccountId);
            }

            if (currentRoleId == Constants.Administrator && roleId == Constants.Operator)// Only Operators
            {
                users = (List<Domain.Models.User>)await _repository.GetByRoleIdByAccount(roleId,currentAccountId);
            }

            // If Operators not show users

            
            if (roleFilter.filter != null)
            {
                //Filters
                if (roleFilter.filter.ContainsKey("firstName") && !string.IsNullOrEmpty(roleFilter.filter["firstName"]))
                {
                    var value = roleFilter.filter["firstName"].ToString().ToUpper();
                    //Filtro sin acentos
                    users = (from c in users
                             where RegularExpression.RemoveAccents(c.FirstName).ToUpper().Contains(RegularExpression.RemoveAccents(value))
                                 select c).ToList();
                }
                if (roleFilter.filter.ContainsKey("lastName") && !string.IsNullOrEmpty(roleFilter.filter["lastName"]))
                {
                    var value = roleFilter.filter["lastName"].ToString().ToUpper();
                    users = (from c in users
                             where RegularExpression.RemoveAccents(c.LastName).ToUpper().Contains(RegularExpression.RemoveAccents(value))
                                 select c).ToList();
                }
                if (roleFilter.filter.ContainsKey("fullName") && !string.IsNullOrEmpty(roleFilter.filter["fullName"]))
                {
                    var value = roleFilter.filter["fullName"].ToString().ToUpper();
                    users = (from c in users
                             where RegularExpression.RemoveAccents(c.FullName).ToUpper().Contains(RegularExpression.RemoveAccents(value))
                             select c).ToList();
                }
                if (roleFilter.filter.ContainsKey("email") && !string.IsNullOrEmpty(roleFilter.filter["email"]))
                {
                    var value = roleFilter.filter["email"].ToString().ToUpper();
                    users = (from c in users
                                 where RegularExpression.RemoveAccents(c.Email).ToUpper().Contains(RegularExpression.RemoveAccents(value))
                                 select c).ToList();
                }
                if (roleFilter.filter.ContainsKey("phone") && !string.IsNullOrEmpty(roleFilter.filter["phone"]))
                {
                    var value = roleFilter.filter["phone"].ToString();
                    users = (from c in users
                             where RegularExpression.RemoveAccents(c.Phone).ToUpper().Contains(RegularExpression.RemoveAccents(value))
                             select c).ToList();
                }
            }

            //Se cambio de los datos a las propiedades de respuesta
            var userResponses = new List<UserResponseGetDto>();
            foreach (var user in users)
            {
                userResponses.Add(SetUserResponse(user));
            }

            //pagination And ordering
            var pagedUsers = PagedList<UserResponseGetDto>.Create(userResponses, roleFilter.page, roleFilter.order);
            return pagedUsers;
        }


        private UserResponseGetDto SetUserResponse(Domain.Models.User user)
        {
            var userResponse =  new UserResponseGetDto()
            {
                id = user.Id,
                firstName = user.FirstName,
                lastName = user.LastName,
                email = user.Email,
                phone = String.IsNullOrEmpty(user.Phone) ? string.Empty : user.Phone,
                status = (int)user.Status,
                //roleId = user.RoleId,
                /*allowCreateCampaigns = (bool)user.AllowCreateCampaigns,
                allowCreateCustomers = (bool)user.AllowCreateCustomers,
                allowManageWorkflows = (bool)user.AllowManageWorkflows,
                targetType = user.TargetType,
                customerId = user.CustomerId==null? null : (Guid?)user.CustomerId,
                avatarUrl = String.IsNullOrEmpty(user.AvatarUrl) ? string.Empty : _helpers.GeneralPathWeb(user.AvatarUrl),
                accountId = user.AccountId
                */
            };
            /*if(user.TargetId!=null)
            {
                if (user.TargetId.Contains(","))
                {
                    userResponse.targetId = user.TargetId.Split(",").ToList();
                }
                else
                {
                    userResponse.targetId = user.TargetId;
                }
            }
           */
            return userResponse;

        }

        private UserResponseToAssignGetDto SetUserResponseToAssign(Domain.Models.User user)
        {
            var userResponse = new UserResponseToAssignGetDto()
            {
                id = user.Id,
                firstName = user.FirstName,
                lastName = user.LastName,
                //roleId = user.RoleId,
                /*allowCreateCampaigns = (bool)user.AllowCreateCampaigns,
                allowCreateCustomers = (bool)user.AllowCreateCustomers,
                allowManageWorkflows = (bool)user.AllowManageWorkflows,
                targetType = user.TargetType
                */
            };
            
            /*if (user.TargetId != null)
            {
                if (user.TargetId.Contains(","))
                {
                    userResponse.targetId = user.TargetId.Split(",").ToList();
                }
                else
                {
                    userResponse.targetId = user.TargetId;
                }
            }*/

            return userResponse;

        }

    }
}
