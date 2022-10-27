using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace RepositoryLayer.Service
{
    public class UserRL : IuserRL
    {
        private readonly FundooContext fundooContext;

        private readonly IConfiguration iconfiguration;
        public UserRL(FundooContext fundooContext, IConfiguration iconfiguration)
        {
            this.fundooContext = fundooContext;
            this.iconfiguration = iconfiguration;
        }
        public UserEntity Registration(UserRegistrationModel userRegistrationModel)
        {
            try
            {
                UserEntity userEntityobj = new UserEntity();
                userEntityobj.FirstName = userRegistrationModel.FirstName;
                userEntityobj.LastName = userRegistrationModel.LastName;
                userEntityobj.Email = userRegistrationModel.Email;
                userEntityobj.Password = userRegistrationModel.Password;
                fundooContext.UserTable.Add(userEntityobj);
                int result = fundooContext.SaveChanges();
                if(result != 0)
                {
                    return userEntityobj;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        //For login
        public string Login(UserLoginModel userLoginModel)
        {
            try
            {
                //query to check only for email and password
                var resultLog = fundooContext.UserTable.Where(x => x.Email == userLoginModel.Email && x.Password == userLoginModel.Password).FirstOrDefault();


                if (resultLog != null)
                {
                    //taken userLoginModel to get the stored data used for login
                    //userLoginModel.Email = resultLog.Email;
                    //userLoginModel.Password = resultLog.Password;
                    //return userLoginModel.Email;
                    var token = GenerateSecurityToken(resultLog.Email, resultLog.UserId);
                    return token;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception)
            {
                throw;
            }
        }
        public string GenerateSecurityToken(string email, long UserId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(this.iconfiguration[("JWT:Key")]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email, email),
                    new Claim("UserId",UserId.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);

        }
        public string ForgetPassword(string email)
        {
            try
            {
                var emailcheck = fundooContext.UserTable.FirstOrDefault(x => x.Email == email);
                if (emailcheck != null)
                {
                    var token = GenerateSecurityToken(emailcheck.Email, emailcheck.UserId);
                    MSMQ msmq = new MSMQ();
                    msmq.sendData2Queue(token);
                    return token;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string ResetPassword(string email, string newPassword, string confirmPassword)
        {
            try
            {
                if (newPassword.Equals(confirmPassword))
                {
                    var user = fundooContext.UserTable.FirstOrDefault(x => x.Email == email);
                    user.Password = newPassword;
                    fundooContext.SaveChanges();
                    return user.Password;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
