using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using RepositoryLayer.Context;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;
using RepositoryLayer.Entity;

namespace FundooWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly INotesBL iNotesBL;

        private readonly IMemoryCache memoryCache;

        private readonly IDistributedCache distributedCache;

        private readonly FundooContext fundooContext;
        public NotesController(INotesBL iNotesBL, IMemoryCache memoryCache, IDistributedCache distributedCache, FundooContext fundooContext)
        {
            this.iNotesBL = iNotesBL;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
            this.fundooContext = fundooContext;
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
        [Authorize]
        [HttpGet]
        [Route("Retrieve by ")]

        public IActionResult RetrieveNotesbyUserID(long UserId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = iNotesBL.RetrieveNotesbyUserID(userId);
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
        [Authorize]
        [HttpGet]
        [Route("Retrieve")]
        public IActionResult RetrieveNotesbyNoteID(long NoteId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = iNotesBL.RetrieveNotesbyNoteID(userId, NoteId);
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
        [Authorize]
        [HttpDelete]
        [Route("Delete")]
        public ActionResult DeleteNotes(int noteId)
        {
            try
            {
                if (iNotesBL.DeleteNotes(noteId))
                {
                    return this.Ok(new { success = true, message = "Note Deleted Successfully" });
                }
                return this.BadRequest(new { success = true, message = "Note Deletion Failed" });
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        [Authorize]
        [HttpPut]
        [Route("Update")]
        public IActionResult UpdateNotesData(long noteId, NotesModel notesModel)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = iNotesBL.UpdateNotesData(userId, noteId, notesModel);

                if (result != null)
                {
                    return Ok(new { success = true, message = "Update data Successful ", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Update data UnSuccessful" });
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        [Authorize]
        [HttpPut]
        [Route("Pin")]
        public IActionResult PinNotes(long noteId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = iNotesBL.PinNotes(userId, noteId);

                if (result != null)
                {
                    return Ok(new { success = true, message = "Pin Successful " });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Pin UnSuccessful" });
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        [Authorize]
        [HttpPut]
        [Route("Archive")]
        public IActionResult ArchiveNotes(long noteId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = iNotesBL.ArchiveNotes(userId, noteId);

                if (result != null)
                {
                    return Ok(new { success = true, message = "Archive Successful " });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Archive UnSuccessful" });
                }
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        [Authorize]
        [HttpPut]
        [Route("Trash")]
        public IActionResult TrashNote(long noteId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = iNotesBL.TrashNotes(userId, noteId);

                if (result != null)
                {
                    return Ok(new { success = true, message = "Trash Successful " });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Trash Unsuccessful" });
                }
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        [Authorize]
        [HttpPut]
        [Route("BackgroundColor")]
        public IActionResult BackgroundColor(long noteId, string Backgroundcolor)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = iNotesBL.ChangeBackgroundColor(userId, noteId, Backgroundcolor);

                if (result != null)
                {
                    return Ok(new { success = true, message = "Bg color data Successful ", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Bg color data UnSuccessful" });
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        [Authorize]
        [HttpPut]
        [Route("Image")]

        public IActionResult NotesImage(IFormFile Image, long noteId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = iNotesBL.Image(Image, noteId, userId);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Image Added Successfully " });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Image Addition UnSuccessful" });
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        [Authorize]
        [HttpGet]
        [Route("redis")]
        public async Task<IActionResult> GetAllNotesUsingRedisCache()
        {
            long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
            var cacheKey = "NotesList";
            string serializedNotesList;
            var NotesList = new List<NotesEntity>();
            var redisNotesList = await distributedCache.GetAsync(cacheKey);
            if (redisNotesList != null)
            {
                serializedNotesList = Encoding.UTF8.GetString(redisNotesList);
                NotesList = JsonConvert.DeserializeObject<List<NotesEntity>>(serializedNotesList);
            }
            else
            {
                NotesList = fundooContext.NotesTable.ToList();
                serializedNotesList = JsonConvert.SerializeObject(NotesList);
                redisNotesList = Encoding.UTF8.GetBytes(serializedNotesList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(cacheKey, redisNotesList, options);
            }
            return Ok(NotesList);
        }
    }
}
