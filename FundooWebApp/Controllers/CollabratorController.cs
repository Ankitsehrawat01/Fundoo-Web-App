using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using RepositoryLayer.Context;
using Newtonsoft.Json;
using RepositoryLayer.Entity;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundooWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollabratorController : ControllerBase
    {
        private readonly ICollabratorBL iCollabratorBL;

        private readonly IMemoryCache memoryCache;

        private readonly IDistributedCache distributedCache;

        private readonly FundooContext fundooContext;
        public CollabratorController(ICollabratorBL iCollabratorBL, IMemoryCache memoryCache, IDistributedCache distributedCache, FundooContext fundooContext)
        {
            this.iCollabratorBL = iCollabratorBL;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
            this.fundooContext = fundooContext;
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
        [Authorize]
        [HttpGet]
        [Route("redis")]
        public async Task<IActionResult> GetAllCollabratorUsingRedisCache()
        {
            long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
            var cacheKey = "CollabratorList";
            string serializedCollabratorList;
            var CollabratorList = new List<CollabratorEntity>();
            var redisCollabratorList = await distributedCache.GetAsync(cacheKey);
            if (redisCollabratorList != null)
            {
                serializedCollabratorList = Encoding.UTF8.GetString(redisCollabratorList);
                CollabratorList = JsonConvert.DeserializeObject<List<CollabratorEntity>>(serializedCollabratorList);
            }
            else
            {
                CollabratorList = fundooContext.collabratorTable.ToList();
                serializedCollabratorList = JsonConvert.SerializeObject(CollabratorList);
                redisCollabratorList = Encoding.UTF8.GetBytes(serializedCollabratorList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(cacheKey, redisCollabratorList, options);
            }
            return Ok(CollabratorList);
        }
    }
}
