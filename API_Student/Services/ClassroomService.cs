using API_Student.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace API_Student.Services
{
    public class ClassroomService
    {
        private readonly AppDbContext _context;

        public ClassroomService(AppDbContext context)
        {
            _context = context;
        }
        public List<Classroom> GetAllClassRoomsAsync()
        {
            var clrs = _context.Classroom!.Select(x => new Classroom
            {
                ClassRoomId = x.ClassRoomId,
                ClassName = x.ClassName
            });
            return clrs.ToList();
        }
        public Task<Classroom> GetClassRoomAsync(int id)
        {
            var clr = _context.Classroom!.FirstOrDefaultAsync(x => x.ClassRoomId == id);
            return clr;
        }
        public async Task<int> AddClassRoomAsync(Classroom clr)
        {
            _context.Classroom!.Add(clr);
            await _context.SaveChangesAsync();
            return clr.ClassRoomId;
        }
        public void UpdateClassRoomAsync(Classroom classroom)
        {
            var _classroom = _context.Classroom.SingleOrDefault(c => c.ClassRoomId == classroom.ClassRoomId);
            classroom = _classroom;
            _context.SaveChanges();
        }
        private bool ClassroomExists(int id)
        {
            return _context.Classroom!.Any(e => e.ClassRoomId == id);
        }
        
        public void DeleteClassRoomAsync(int id)
        {
            var clr = _context.Classroom.SingleOrDefault(lo => lo.ClassRoomId == id);
            if (clr != null)
            {
                _context.Remove(clr);
                _context.SaveChanges();
            }
        }
        public JsonResult Join()
        {
            var result = from student in _context.Student
                         join classroom in _context.Classroom 
                         on student.ClassRoomId equals classroom.ClassRoomId
                         select new { student, classroom };
            return new JsonResult(result);
        }
        public JsonResult LeftJoin()
        {
            var result = from student in _context.Student
                         join classroom in _context.Classroom
                             on student.ClassRoomId equals classroom.ClassRoomId into r
                         from clr in r.DefaultIfEmpty()
                         select new
                         {
                             StudentName = student.TenSinhVien,
                             ClassName= clr != null ? clr.ClassName : ""
                         };
            return new JsonResult(result);
        }
    }
}
