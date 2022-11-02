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

                    return Ok(new { success = true, message = "Label Deleted" });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Label not Deleted" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

    }
}
