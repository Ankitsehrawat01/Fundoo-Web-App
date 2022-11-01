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
        public IActionResult CreateCollab(CollabratorModel collabratorModel, long noteId)
        {
            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);

                var result = iCollabratorBL.CreateCollabrator(collabratorModel, userId, noteId);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Collabrator Created", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "colabrator Not Created" });

                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
