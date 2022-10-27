using CommonLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IuserRL
    {
        public UserEntity Registration(UserRegistrationModel userRegistrationModel);
        //Added For Login 
        //Here UserLoginModel class is taken to get the email and Password Property
        public string Login(UserLoginModel userLoginModel);

        public string ForgetPassword(string email);

        public string ResetPassword(string email, string newPassword, string confirmPassword);
    }
}
