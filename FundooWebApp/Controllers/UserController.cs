using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FundooWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IuserBL iuserBL;
        public UserController(IuserBL iuserBL)
        {
            this.iuserBL = iuserBL;
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
            catch (System.Exception)
            {
                throw;
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
                }
                else
                {
                    return BadRequest(new{success = false, message ="Login UnSuccessful"});
                }

            }
            catch (System.Exception)
            {
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
    }
}
