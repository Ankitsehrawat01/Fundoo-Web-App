using BusinessLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace FundooWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabelController : ControllerBase
    {
        private readonly ILabelBL iLabelBL;

        private readonly IMemoryCache memoryCache;

        private readonly IDistributedCache distributedCache;

        private readonly FundooContext fundooContext;
        public LabelController(ILabelBL iLabelBL, IMemoryCache memoryCache, IDistributedCache distributedCache, FundooContext fundooContext)
        {
            this.iLabelBL = iLabelBL;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
            this.fundooContext = fundooContext;
        }
        [Authorize]
        [HttpPost]
        [Route("Create")]
        public IActionResult LabelCreation(long noteId, string label_Name)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);

                var result = iLabelBL.CreateLabel(label_Name, noteId, userId);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Label Created", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Label not Created", data = result });

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

        public IActionResult LabelDelete(long labelId)
        {
            try
            {
                var result = iLabelBL.DeleteLabel(labelId);

                if (result != null)
                {

                    return Ok(new { success = true, message = "Label Deleted", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Label not Deleted", data = result });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        [Authorize]
        [HttpGet]
        [Route("Retrive")]

        public IActionResult LabelRetrive(long labelId)
        {
            try
            {
                var result = iLabelBL.RetrieveLabel(labelId);

                if (result != null)
                {

                    return Ok(new { success = true, message = "Label Retrived", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Label not Retrived", data = result });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        [Authorize]
        [HttpPut]
        [Route("Update")]

        public IActionResult LabelUpdate(long labelId, string label_Name)
        {
            try
            {
                var result = iLabelBL.UpdateLabel(label_Name, labelId );

                if (result != null)
                {

                    return Ok(new { success = true, message = "Label Updated", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Label not Updated", data = result });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        [Authorize]
        [HttpGet]
        [Route("redis")]
        public async Task<IActionResult> GetAllLabelUsingRedisCache()
        {
            long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
            var cacheKey = "LabelList";
            string serializedLabelList;
            var LabelList = new List<LabelEntity>();
            var redisLabelList = await distributedCache.GetAsync(cacheKey);
            if (redisLabelList != null)
            {
                serializedLabelList = Encoding.UTF8.GetString(redisLabelList);
                LabelList = JsonConvert.DeserializeObject<List<LabelEntity>>(serializedLabelList);
            }
            else
            {
                LabelList = fundooContext.LabelTable.ToList();
                serializedLabelList = JsonConvert.SerializeObject(LabelList);
                redisLabelList = Encoding.UTF8.GetBytes(serializedLabelList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(cacheKey, redisLabelList, options);
            }
            return Ok(LabelList);
        }
    }
}
