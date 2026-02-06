using Microsoft.AspNetCore.Mvc;

namespace ZohoSapIntg.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SapController : ControllerBase
    {
        [HttpGet("connect")]
        public IActionResult Connect()
        {
            return Ok(new { message = "SAP deshabilitado temporalmente" });
        }
    }
}
