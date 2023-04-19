using API_Student.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Student.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LINQController : ControllerBase
    {
        private readonly ClassroomService _service;

        public LINQController(ClassroomService service) {
            _service = service;
        }
        [HttpGet]
        public IActionResult Join() {
            var result = _service.Join();
            return Ok(result);
        }
        [HttpGet("result")]
        public IActionResult LeftJoin()
        {
            var result = _service.LeftJoin();
            return Ok(result);
        }
    }
}
