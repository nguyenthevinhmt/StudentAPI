using API_Student.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace API_Student.Services
{
    public class StudentService
    {
        private readonly AppDbContext _context;

        public StudentService(AppDbContext context) { 
            _context = context;
        }
        public List<Student> GetAllStudentsAsync()
        {
            var std = _context.Student!.Select(c => new Student
            {
               Id = c.Id,
               MSSV = c.MSSV,
               TenSinhVien = c.TenSinhVien,
               ClassRoomId = c.ClassRoomId,
               GhiChu  = c.GhiChu,
               IsDeleted = c.IsDeleted
            });
            return std.ToList();
        }
        public Task<Student> GetStudentAsync(int id)
        {
            var std = _context.Student!.FirstOrDefaultAsync(x => x.Id == id);
            return std;
        }
        public async Task<int> AddStudentAsync(Student std)
        {      
            _context.Student!.Add(std);
            await _context.SaveChangesAsync();
            return std.Id;
        }
        public void UpdateStudentAsync(Student student)
        {
            var _student = _context.Student.SingleOrDefault(c => c.Id == student.Id);
            student = _student;
            _context.SaveChanges();
        }   
    
        public void DeleteStudentAsync(int id)
        {
            var std = _context.Student.SingleOrDefault(lo => lo.Id == id);
            if (std != null)
            {
                _context.Remove(std);
                _context.SaveChanges();
            }
        }
        //public IQueryable<Student> SortByMSSV()
        //{
        //    var result = _context.Student!.OrderBy(c => c.MSSV);
        //    return result;               
        //}
        //public IQueryable<Student> SortByName()
        //{
        //    var result = _context.Student.OrderBy(c => c.TenSinhVien);
        //    return result;
        //}
        public IQueryable<Student> SortByCondition(string condition)
        {
            var result = from s in _context.Student select s;
            if(condition != null)
            {
                switch (condition)
                {
                    case "MSSV": result = result.OrderBy(c => c.MSSV); break;
                    case "Name": result = result.OrderBy(c => c.TenSinhVien); break;
                } 
            }
            return result;
        }
        public IQueryable<Student> SortByManyCondition()
        {
            var result = _context.Student.OrderBy(c => c.MSSV).ThenByDescending(c => c.ClassRoomId);
            return result;
        }
        public List<Student> Paging(int pageNumber, int pageSize)
        {
          
            var students = _context.Student
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            return students;
        }
        public List<Student> Filtering(int id)
        {
            var result = _context.Student.Where(c => c.ClassRoomId == id);
            return result.ToList();
        }
    }
}
