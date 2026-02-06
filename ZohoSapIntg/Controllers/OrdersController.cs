using Microsoft.AspNetCore.Mvc;

namespace ZohoSapIntg.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll([FromQuery] string cardCode)
        {
            return Ok(new { message = "Orders API - SAP deshabilitado temporalmente" });
        }

        [HttpGet("{docEntry}")]
        public IActionResult Get(int docEntry)
        {
            return Ok(new { message = "Orders API - SAP deshabilitado temporalmente" });
        }

        [HttpPost]
        public IActionResult Create([FromBody] object order)
        {
            return Ok(new { message = "Orders API - SAP deshabilitado temporalmente" });
        }

        [HttpPut("{docEntry}")]
        public IActionResult Update(int docEntry, [FromBody] object order)
        {
            return Ok(new { message = "Orders API - SAP deshabilitado temporalmente" });
        }
    }
}
