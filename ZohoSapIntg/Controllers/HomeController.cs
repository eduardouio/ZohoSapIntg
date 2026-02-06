using Microsoft.AspNetCore.Mvc;

namespace ZohoSapIntg.Controllers
{
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet("/")]
        public IActionResult Index()
        {
            return Ok("System UP Success");
        }
    }
}
