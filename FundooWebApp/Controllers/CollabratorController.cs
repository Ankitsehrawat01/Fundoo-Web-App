using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;

namespace FundooWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollabratorController : ControllerBase
    {
        private readonly ICollabratorBL iCollabratorBL;
        public CollabratorController(ICollabratorBL iCollabratorBL)
        {
            this.iCollabratorBL = iCollabratorBL;
        }
        [Authorize]
        [HttpPost]
        [Route("Create")]
        public IActionResult CreateCollab(string Email, long noteId)
        {
            try
            {
                var result = iCollabratorBL.CreateCollabrator(Email, noteId);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Created notes successful", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Created notes unsuccessful", data = result });

                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
