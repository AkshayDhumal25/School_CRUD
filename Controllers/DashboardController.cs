using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using School_CRUD.Data;
using School_CRUD.Model;

namespace School_CRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DashboardController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<object>> GetDashboard()
        {
            var totalStudents = await _context.students.CountAsync();
            var totalTeachers = await _context.teachers.CountAsync();
            var totalClasses = await _context.ClassRooms.CountAsync();

            // Attendance percentage per class
            var classAttendances = await _context.ClassRooms
                .Select(c => new
                {
                    ClassRoomId = c.Id,
                    ClassRoomName = c.Name,
                    AttendancePercentage = _context.Attendance
                        .Where(a => a.Student.Id == c.Id)
                        .Average(a => a.IsPresent ? 100 : 0)
                }).ToListAsync();

            // Exam average per class
            var classExamAverages = await _context.ClassRooms
                .Select(c => new
                {
                    ClassRoomId = c.Id,
                    ClassRoomName = c.Name,
                    AverageScore = _context.Marks
                        .Where(m => m.Student.Id == c.Id)
                        .Average(m => (double?)m.Score) ?? 0
                }).ToListAsync();

            return Ok(new
            {
                TotalStudents = totalStudents,
                TotalTeachers = totalTeachers,
                TotalClasses = totalClasses,
                ClassAttendances = classAttendances,
                ClassExamAverages = classExamAverages
            });
        }
    }
}
