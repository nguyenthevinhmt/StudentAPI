using Microsoft.EntityFrameworkCore;

namespace API_Student.Models
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {

        }
        override protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Student>().HasKey(entity => entity.Id); //primary key
            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("Student"); //nếu thay đổi tên bảng thì dùng
                entity.Property(e => e.Id)
                    .HasColumnName("Id") //nếu thay đổi tên thì dùng
                    .ValueGeneratedOnAdd() //identity(1,1)
                    .IsRequired(); //những trường có kiểu dữ liệu cho phép null vd string nếu không dùng IsRequired thì khi tạo sẽ ra trường allow null
                entity.Property(e => e.MSSV)
                    .HasColumnType("nvarchar(50)")
                    .IsRequired();
                entity.Property(e => e.TenSinhVien)
                    .HasColumnType("nvarchar(100)") //nvarchar thì không cần dùng IsUnicode() nữa
                    .IsRequired();
                entity.Property(e => e.ClassRoomId)
                    .HasColumnType("int")
                    .IsRequired();
                entity.Property(e => e.GhiChu)
                    .HasColumnType("nvarchar(500)")
                    .IsRequired(false); //trường allow null
                entity.Property(e => e.IsDeleted)
                    .HasColumnName("IsDeleted")
                    .IsRequired();
            });
            modelBuilder.Entity<Student>().HasIndex(entity => entity.ClassRoomId); //index cho trường là khóa ngoại
            modelBuilder.Entity<Student>()
                .HasIndex(entity => new { entity.MSSV, entity.IsDeleted })
                .HasFilter("IsDeleted <> 1") //bỏ qua những dòng đánh dấu đã xóa
                .IsUnique(); //unique

            //Classroom setup
            modelBuilder.Entity<Classroom>().HasKey(entity => entity.ClassRoomId);
            modelBuilder.Entity<Classroom>(entity =>
            {
                entity.ToTable("ClassRoom"); //nếu thay đổi tên bảng thì dùng
                entity.Property(e => e.ClassRoomId)
                    .HasColumnName("Id") //nếu thay đổi tên thì dùng
                    .ValueGeneratedOnAdd() //identity(1,1)
                    .IsRequired(); //những trường có kiểu dữ liệu cho phép null vd string nếu không dùng IsRequired thì khi tạo sẽ ra trường allow null
                entity.Property(e => e.ClassName)
                    .HasColumnName("ClassName")
                    .IsRequired();
            });
        }
        public DbSet<Student>? Student { get; set; }
        public DbSet<Classroom>? Classroom { get; set; }
    }
}
