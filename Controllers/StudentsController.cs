using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        List<Student> Students = new List<Student>()
        {
            new Student(){Id=1,Name="Abc",Grade=12 },
            new Student(){Id=2,Name="ABB",Grade=12 },
            new Student(){Id=3,Name="HHHH",Grade=12 },
            new Student(){Id=4,Name="UUASD",Grade=12 },
            new Student(){Id=5,Name="NNN",Grade=12 },
            new Student(){Id=6,Name="THANH",Grade=12 },
        };


        [HttpGet]
        public IActionResult Get()
        {
           return Ok(Students);
        }
        
        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var _stu = Students.SingleOrDefault(s => s.Id == id);
            if (_stu == null) return NotFound();
            return Ok(_stu);
        }


        [HttpPost]
        public IActionResult Add(Student stu)
        {
            Students.Add(stu);
            return Ok(Students);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, Student stu)
        {
            var _stu = Students.SingleOrDefault(s => s.Id == id);
            if (_stu == null) return NotFound("Not student found");
            _stu.Id = stu.Id;
            _stu.Name = stu.Name;
            _stu.Grade = stu.Grade;
            return Ok(Students);
        }

        [HttpDelete("{id}")] 
        public IActionResult Delete(long id)
        {
            var _stu = Students.SingleOrDefault(s => s.Id == id);
            if (_stu == null) return NotFound("Not student found");
            Students.Remove(_stu);  
            return Ok(Students);
        }


    }
}
