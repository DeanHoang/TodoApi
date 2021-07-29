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
    public class ClassesController : ControllerBase
    {
        private readonly SchoolDBContext _context;

        public ClassesController(SchoolDBContext context)
        {
            _context = context;
        }

        // GET: api/Classes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Class>>> GetClasses()
        {
            return await _context.Classes.ToListAsync();
        }

        // GET: api/Classes/5
        [HttpGet("{classId}")]
        public async Task<ActionResult<Class>> GetClass(int classId)
        {
            var @class = await _context.Classes.FindAsync(classId);

            if (@class == null)
            {
                return NotFound();
            }

            return @class;
        }
        [HttpGet("{classId}/Students")]
        public async Task<ActionResult<Class>> GetStudentofClass(int classId)
        {
            var @class = await _context.Classes.Include(s => s.ClassErollments)
                                                    .ThenInclude(st => st.Student)
                                                    .Where(cl => cl.ClassId == classId)
                                                    .FirstOrDefaultAsync();
            if (@class == null)
            {
                return NotFound();
            }

            return @class;
        }

        // PUT: api/Classes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{classId}")]
        public async Task<IActionResult> PutClass(int classId, Class @class)
        {
            if (classId != @class.ClassId)
            {
                return BadRequest();
            }

            _context.Entry(@class).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClassExists(classId))
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

        // POST: api/Classes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Class>> PostClass(Class @class)
        {
            _context.Classes.Add(@class);
            await _context.SaveChangesAsync();

            return Ok(_context.Classes.ToList());
        }

        [HttpPost("{classId}/Students")]
        public async Task<ActionResult<Class>> PostStudenttoClass(int classId, Student student)
        {
            if (!_context.Students.Contains(student)) return NotFound("Student does not exist");
            ClassErollment classErollment = new ClassErollment()
            {
                ClassId = classId,
                StudentId = student.StudentId
            };
            _context.ClassErollments.Add(classErollment);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // DELETE: api/Classes/5
        [HttpDelete("{classId}")]
        public async Task<IActionResult> DeleteClass(int classId)
        {
            var @class = await _context.Classes.FindAsync(classId);
            if (@class == null)
            {
                return NotFound("Class does not exist");
            }

            _context.Classes.Remove(@class);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpDelete("{classId}/Students/{studentId}")]
        public async Task<IActionResult> DeleteClass(int classId, int studentId)
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

        private bool ClassExists(int id)
        {
            return _context.Classes.Any(e => e.ClassId == id);
        }
    }
}
