using Microsoft.AspNetCore.Mvc;
using ZohoSapIntg.Common;
using ZohoSapIntg.Models;

namespace ZohoSapIntg.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly OrderService _service = new();

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_service.ListOrders());
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("{docEntry}")]
        public IActionResult Get(int docEntry)
        {
            try
            {
                return Ok(_service.GetOrder(docEntry));
            }
            catch (Exception ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody] OrderDto order)
        {
            try
            {
                var docEntry = _service.CreateOrder(order);
                return CreatedAtAction(nameof(Get), new { docEntry }, new { docEntry });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPut("{docEntry}")]
        public IActionResult Update(int docEntry, [FromBody] OrderDto order)
        {
            try
            {
                _service.UpdateOrder(docEntry, order);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
