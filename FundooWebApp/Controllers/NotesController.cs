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
    public class NotesController : ControllerBase
    {
        private readonly INotesBL iNotesBL;
        public NotesController(INotesBL iNotesBL)
        {
            this.iNotesBL = iNotesBL;
        }
        [Authorize]
        [HttpPost]
        [Route("Create")]
        public IActionResult CreateNotes(NotesModel notesModel)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);

                var result = iNotesBL.createNotes(notesModel, userId);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Notes Created", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Notes Not Created" });

                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
