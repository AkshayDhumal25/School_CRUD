using Microsoft.EntityFrameworkCore;
using School_CRUD.Model;

namespace School_CRUD.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
            
        }

        public DbSet<Student> students { get; set; }
        public DbSet<Teacher> teachers { get; set; }

        public DbSet<ClassRoom> ClassRooms { get; set; }

        public DbSet<Attendance> Attendance { get; set; }

        public DbSet<Subject> Subjects { get; set; }
    }
}
