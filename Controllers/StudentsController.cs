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
    public class StudentsController : ControllerBase
    {
        private readonly IStudentRepository _studentRepository;

        public StudentsController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        // GET: api/Students
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {            
            return Ok(await _studentRepository.GetStudents());
        }

        // GET: api/Students/5
        [HttpGet("{studentId}")]
        public async Task<ActionResult<Student>> GetStudent(int studentId)
        {
            var student = await _studentRepository.GetStudent(studentId);

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
            return Ok(await _studentRepository.GetClassesofStudent(studentId));
        }
        [HttpGet("Name/{studentName}")]
        public async Task<ActionResult<Student>> GetStudentByName(string studentName)
        {
            //return Ok(await _studentRepository.GetStudentsByName(studentName));
            var _student = await _studentRepository.GetStudentsByName(studentName);

            if (_student == null)
            {
                return NotFound();
            }

            return Ok(_student);
        }

        // PUT: api/Students/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{studentId}")]
        public async Task<IActionResult> PutStudent(int studentId, Student student)
        {
            await _studentRepository.UpdateStudent(student);
            return Ok();
        }

        // POST: api/Students
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Student>> PostStudent(Student student)
        {
           await _studentRepository.AddStudent(student);

            return Ok();
        }

        //POST: api/Students/2/Classes
        [HttpPost("{studentId}/Classes/{classId}")]
        public async Task<ActionResult<Student>> PostClassesforStudent(int studentId, int classId)
        {
            await _studentRepository.PostClassforStudent(studentId, classId);
            return Ok();
        }
        // DELETE: api/Students/5
        [HttpDelete("{studentId}")]
        public async Task<IActionResult> DeleteStudent(int studentId)
        {
            await _studentRepository.DeleteStudent(studentId);
            return Ok();
        }


        [HttpDelete("{studentId}/Classes/{classId}")]
        public async Task<IActionResult> DeleteClassforStudent(int studentId,int classId)
        {
            await _studentRepository.DeleteClassforStudent(studentId, classId);

            return NoContent();
        }

        //private bool StudentExists(int studentId)
        //{
        //    return _context.Students.Any(e => e.StudentId == studentId);
        //}
    }
}
