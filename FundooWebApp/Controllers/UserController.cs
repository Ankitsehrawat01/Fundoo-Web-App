using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Security.Claims;

namespace FundooWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IuserBL iuserBL;

        private readonly ILogger<UserController> logger;

        public UserController(IuserBL iuserBL, ILogger<UserController> logger)
        {
            this.iuserBL = iuserBL;
            this.logger = logger;
        }
        [HttpPost]
        [Route("Register")]
        public IActionResult RegisterUser(UserRegistrationModel userRegistrationModel)
        {
            try
            {
                var result = iuserBL.Registration(userRegistrationModel);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Registration successful", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Registration unsuccessful" });

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        [Route("Login")]

        public IActionResult LoginUser(UserLoginModel userLoginModel)
        {
            try
            {
                var resultLog = iuserBL.Login(userLoginModel);

                if (resultLog != null)
                {
                    return Ok(new { success = true, message = "Login Successful", data = resultLog });

                    throw new Exception("Error Occured");
                }
                else
                {
                    return BadRequest(new{success = false, message ="Login UnSuccessful"});
                }

            }
            catch (Exception ex )
            {
                logger.LogError(ex.ToString());
                throw;
            }
        }
        [HttpPost]
        [Route("ForgetPassword")]

        public IActionResult ForgetPassword(string email)
        {
            try
            {
                var resultLog = iuserBL.ForgetPassword(email);

                if (resultLog != null)
                {
                    return Ok(new { success = true, message = "Reset Email Send" });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Reset UnSuccessful" });
                }

            }
            catch (System.Exception)
            {
                throw;
            }
        }
        [Authorize]
        [HttpPut]
        [Route("ResetPassword")]

        public IActionResult ResetPassword(string newPassword, string confirmPassword)
        {
            try
            {
                var email = User.FindFirst(ClaimTypes.Email).Value.ToString();
                var resultLog = iuserBL.ResetPassword(email, newPassword, confirmPassword);

                if (resultLog != null)
                {
                    return Ok(new { success = true, message = "Password Reset Successful" });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Password Reset UnSuccessful" });
                }

            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
