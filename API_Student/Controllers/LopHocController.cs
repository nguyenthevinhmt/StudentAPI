using API_Student.Models;
using API_Student.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Student.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LopHocController : ControllerBase
    {
        private readonly ClassroomService _classRoomService;

        public LopHocController(ClassroomService classRoomService)
        {
            _classRoomService= classRoomService;
        }
        [HttpGet]
        public IActionResult GetAllClassRoom()
        {
            try
            {
                return Ok(_classRoomService.GetAllClassRoomsAsync());
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetClassRoomById(int id)
        {
            var clr = await _classRoomService.GetClassRoomAsync(id);
            return clr == null ? NotFound() : Ok(clr);
        }
        [HttpPost]
        public async Task<IActionResult> AddNewClassRoom(Classroom clr)
        {
            try
            {
                var _clr = new Classroom
                {
                    ClassRoomId = clr.ClassRoomId,
                    ClassName = clr.ClassName
                };
                var newClassRoomId = await _classRoomService.AddClassRoomAsync(_clr);
                var student = await _classRoomService.AddClassRoomAsync(_clr);
                return clr == null ? NotFound() : Ok(_clr);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPut("{id}")]
        public IActionResult UpdateClassRoom(int id, [FromBody] Classroom clr)
        {
            if (id != clr.ClassRoomId)
            {
                return NotFound();
            }
            _classRoomService.UpdateClassRoomAsync(clr);
            return Ok();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteStudent([FromRoute] int id)
        {
            _classRoomService.DeleteClassRoomAsync(id);
            return Ok();
        }
        
    }
}
