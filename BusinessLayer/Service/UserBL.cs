using BusinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class UserBL : IuserBL
    {
        private readonly IuserRL iuserRL;
        public UserBL(IuserRL iuserRL)
        {
            this.iuserRL = iuserRL;
        }
        public UserEntity Registration(UserRegistrationModel userRegistrationModel)
        {
            try
            {
                return iuserRL.Registration(userRegistrationModel);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string Login(UserLoginModel userLoginModel)
        {
            try
            {
                return iuserRL.Login(userLoginModel);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string ForgetPassword(string email)
        {
            try
            {
                return iuserRL.ForgetPassword(email);
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
                return iuserRL.ResetPassword(email, newPassword, confirmPassword);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
