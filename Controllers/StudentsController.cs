using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models1;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly SchoolDBContext _context;

        public StudentsController(SchoolDBContext context)
        {
            _context = context;
        }

        // GET: api/Students
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            return await _context.Students.ToListAsync();
        }

        // GET: api/Students/5
        [HttpGet("{studentId}")]
        public async Task<ActionResult<Student>> GetStudent(int studentId)
        {
            var student = await _context.Students.FindAsync(studentId);

            if (student == null)
            {
                return NotFound();
            }

            return student;
        }

        //Get:api/Students/2/Classes
        [HttpGet("{studentId}/Classes")]
        public async Task<ActionResult<Student>> GetClassesofStudent(int studentId)
        {
            var student = await _context.Students.Include(s => s.ClassErollments)
                                                    .ThenInclude(cs => cs.Class)
                                                     .Where(stu => stu.StudentId == studentId)
                                                     .FirstOrDefaultAsync();
            if (student == null)
            {
                return NotFound();
            }

            return student;
        }

        // PUT: api/Students/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{studentId}")]
        public async Task<IActionResult> PutStudent(int studentId, Student student)
        {
            if (studentId != student.StudentId)
            {
                return BadRequest();
            }

            _context.Entry(student).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(studentId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Students
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Student>> PostStudent(Student student)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return Ok();
        }

        //POST: api/Students/2/Classes
        [HttpPost("{studentId}/Classes")]
        public async Task<ActionResult<Student>> PostClassesforStudent(int studentId, Class @class)
        {
          if(!_context.Classes.Contains(@class)) return NotFound("Class does not exist");
          ClassErollment classErollment = new ClassErollment()
          {
              ClassId = @class.ClassId,
              StudentId = studentId
          };
            _context.ClassErollments.Add(classErollment);
            await _context.SaveChangesAsync();

            return Ok();
        }
        // DELETE: api/Students/5
        [HttpDelete("{studentId}")]
        public async Task<IActionResult> DeleteStudent(int studentId)
        {
            var student = await _context.Students.FindAsync(studentId);
            if (student == null)
            {
                return NotFound("Student does not exist");
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        [HttpDelete("{studentId}/Classes/{classId}")]
        public async Task<IActionResult> DeleteClassforStudent(int studentId,int classId)
        {
            var @classStudents = await _context.ClassErollments.FindAsync(classId, studentId);
            if (@classStudents == null)
            {
                return NotFound("Student is not in this class");
            }

            _context.ClassErollments.Remove(@classStudents);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StudentExists(int studentId)
        {
            return _context.Students.Any(e => e.StudentId == studentId);
        }
    }
}
