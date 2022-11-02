using BusinessLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;

namespace FundooWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabelController : ControllerBase
    {
        private readonly ILabelBL iLabelBL;
        public LabelController(ILabelBL iLabelBL)
        {
            this.iLabelBL = iLabelBL;
        }
        [Authorize]
        [HttpPost]
        [Route("Create")]
        public IActionResult CreateCollab(long noteId, long userId, string label_Name)
        {
            try
            {
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
    }
}
