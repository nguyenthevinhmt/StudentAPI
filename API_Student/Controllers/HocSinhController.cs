using API_Student.Models;
using API_Student.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Student.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HocSinhController : ControllerBase
    {
        private readonly StudentService _studentService;

        public HocSinhController(StudentService studentService) {
            _studentService = studentService;
        }
        [HttpGet]
        public IActionResult GetAllStudents()
        {
            try
            {
                return Ok(_studentService.GetAllStudentsAsync());
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudentById(int id)
        {
            var book = await _studentService.GetStudentAsync(id);
            return book == null ? NotFound() : Ok(book);
        }
        [HttpPost]
        public async Task<IActionResult> AddNewStudent(Student std)
        {
            try
            {
                var _std = new Student
                {
                    Id = std.Id,
                    MSSV = std.MSSV,
                    TenSinhVien = std.TenSinhVien,
                    ClassRoomId = std.ClassRoomId,
                    GhiChu = std.GhiChu,
                    IsDeleted = std.IsDeleted
                };
                var newStudentId = await _studentService.AddStudentAsync(_std);
                var student = await _studentService.GetStudentAsync(newStudentId);
                return std == null ? NotFound() : Ok(_std);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPut("{id}")]
        public IActionResult UpdateStudent(int id, [FromBody] Student std)
        {
            if (id != std.Id)
            {
                return NotFound();
            }
            _studentService.UpdateStudentAsync(std);
            return Ok();
        }
        [HttpDelete("{id}")]

        public IActionResult DeleteStudent([FromRoute] int id)
        {
            _studentService.DeleteStudentAsync(id);
            return Ok();
        }
        //[HttpGet("sortByMSSV")]
        //public JsonResult SortByMSSV()
        //{
        //    var result = _studentService.SortByMSSV();
        //    return new JsonResult(result);
        //}
        //[HttpGet("sortByName")]
        //public JsonResult SortByName()
        //{
        //    var result = _studentService.SortByName();
        //    return new JsonResult(result);
        //}
        [HttpGet("sortByManyCondition")]    
        public JsonResult SortByManyCondition()
        {
            var result = _studentService.SortByManyCondition();
            return new JsonResult(result);
        }
        [HttpGet("Paging")]
        public JsonResult Paging(int pageNumber,int pageSize)
        {
            var result  = _studentService.Paging(pageNumber, pageSize);
            return new JsonResult(result);
        }
        [HttpGet("SortWithCondition")]
        public JsonResult SWC(string str)
        {
            var result = _studentService.SortByCondition(str);
            return new JsonResult(result);
        }
        [HttpGet("Filter")]
        public JsonResult Filtering(int id)
        {
            var result = _studentService.Filtering(id);
            return new JsonResult(result);
        }
    }
}
