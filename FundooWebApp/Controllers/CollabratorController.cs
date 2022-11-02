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
                    return Ok(new { success = true, message = "Collabrator Created", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Collabrator not Created", data = result });

                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        [Authorize]
        [HttpDelete]
        [Route("Delete")]

        public IActionResult DeleteCollab(long collabratorId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);

                var result = iCollabratorBL.DeleteCollabrator(collabratorId, userId);

                if (result != null)
                {

                    return Ok(new { success = true, message = "Collabrator Deleted" });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Collabrator not Deleted" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        [Authorize]
        [HttpGet]
        [Route("Retrieve")]
        public IActionResult Retrievecollab(long NoteId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = iCollabratorBL.RetrieveCollabrator(NoteId);
                if (result != null)
                {

                    return Ok(new { success = true, message = "Retrieve Successful ", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Retrieve UnSuccessful" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
