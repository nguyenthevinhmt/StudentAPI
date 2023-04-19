namespace API_Student.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string MSSV { get; set; }
        public String TenSinhVien { get; set; }
        public int ClassRoomId { get; set; }
        public string GhiChu { get; set; }
        public bool IsDeleted { get; set; }
    }
}
