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

        public DbSet<Exam> Exams { get; set; }
        public DbSet<Mark> Marks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Marks -> Student (disable cascade delete)
            modelBuilder.Entity<Mark>()
                .HasOne(m => m.Student)
                .WithMany()
                .HasForeignKey(m => m.StudentId)
                .OnDelete(DeleteBehavior.NoAction); // <-- Important

            // Marks -> Exam (cascade OK)
            modelBuilder.Entity<Mark>()
                .HasOne(m => m.Exam)
                .WithMany()
                .HasForeignKey(m => m.ExamId)
                .OnDelete(DeleteBehavior.Cascade);

            // Attendance -> Student (disable cascade delete)
            modelBuilder.Entity<Attendance>()
                .HasOne(a => a.Student)
                .WithMany()
                .HasForeignKey(a => a.StudentId)
                .OnDelete(DeleteBehavior.NoAction);

            // Exam -> ClassRoom (cascade OK)
            modelBuilder.Entity<Exam>()
                .HasOne(e => e.ClassRoom)
                .WithMany()
                .HasForeignKey(e => e.ClassRoomId)
                .OnDelete(DeleteBehavior.Cascade);
        }


    }
}
