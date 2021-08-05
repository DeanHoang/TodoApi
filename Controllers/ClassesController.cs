using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models1;
using TodoApi.Repositories;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassesController : ControllerBase
    {
        private readonly IClassRepository _classRepository; 


        public ClassesController(IClassRepository classRepository)
        {
            _classRepository = classRepository;
        }

        // GET: api/Classes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Class>>> GetClasses()
        {
            return Ok(await _classRepository.GetClasses()); 
        }

        [HttpGet("Name/{className}")]
        public async Task<ActionResult<IEnumerable<Class>>> GetClassByName(string className)
        {
            var @class = await _classRepository.GetClassesByName(className);

            if (@class == null)
            {
                return NotFound();
            }

            return Ok(@class);
        }

        // GET: api/Classes/5
        [HttpGet("{classId}")]
        public async Task<ActionResult<Class>> GetClass(int classId)
        {
            var @class = await _classRepository.GetClass(classId);

            if (@class == null)
            {
                return NotFound();
            }

            return @class;
        }
        [HttpGet("{classId}/Students")]
        public async Task<ActionResult<Class>> GetStudentofClass(int classId)
        {
            var @class = await _classRepository.GetStudentsofClass(classId);
            if (@class == null)
            {
                return NotFound();
            }

            return Ok(@class);
        }

        // PUT: api/Classes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{classId}")]
        public async Task<IActionResult> PutClass(int classId, Class @class)
        {
            await _classRepository.UpdateClass(@class);
            return Ok(@class);
        }

        // POST: api/Classes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Class>> PostClass(Class @class)
        {
            await _classRepository.AddClass(@class);
            return Ok(@class);
        }

        //[HttpPost("{classId}/Students")]
        //public async Task<ActionResult<Class>> PostStudenttoClass(int classId, Student student)
        //{
        //    if (!_context.Students.Contains(student)) return NotFound("Student does not exist");
        //    ClassErollment classErollment = new ClassErollment()
        //    {
        //        ClassId = classId,
        //        StudentId = student.StudentId
        //    };
        //    _context.ClassErollments.Add(classErollment);
        //    await _context.SaveChangesAsync();

        //    return Ok();
        //}
        [HttpPost("{classId}/Students/{studentId}")]
        public async Task<ActionResult<Class>> PostStudenttoClass(int classId,int studentId)
        {
            return await _classRepository.PostStudenttoClass(classId, studentId);
        }


        // DELETE: api/Classes/5
        [HttpDelete("{classId}")]
        public async Task<IActionResult> DeleteClass(int classId)
        {
            await _classRepository.DeleteClass(classId);
              return NoContent();
        }


        [HttpDelete("{classId}/Students/{studentId}")]
        public async Task<IActionResult> DeleteStudentfromClass(int classId, int studentId)
        {
             await _classRepository.DeleteStudentfromClass(classId, studentId);
            return Ok();
        }

        //        private bool ClassExists(int id)
        //        {
        //            return _context.Classes.Any(e => e.ClassId == id);
        //        }
    }
}
