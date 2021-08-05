using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Models1;

namespace TodoApi.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly SchoolDBContext _context;
        public StudentRepository(SchoolDBContext schoolDBContext)
        {
            _context = schoolDBContext;
        }
        public async Task<Student> AddStudent(Student student)
        {
            try
            {
                if (!_context.Students.Contains(student))
                {
                    _context.Students.Add(student);
                    await _context.SaveChangesAsync();
                }
                return student;
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
            return null;
        }

        public async Task DeleteClassforStudent(int studentId, int classId)
        {
            var erollment = await _context.ClassErollments.FindAsync(classId, studentId);
            if (erollment != null)
            {
                _context.ClassErollments.Remove(erollment);
                _context.SaveChanges();
            }
        }

        public async Task DeleteStudent(int studentId)
        {
            try
            {
                var @student = _context.Students.Find(studentId);
                var _list = _context.ClassErollments.Where(enl => enl.StudentId == studentId)
                                                             .AsNoTracking()
                    .                                        ToList();
                foreach (var item in _list) _context.ClassErollments.Remove(item);
                _context.Students.Remove(@student);
                await _context.SaveChangesAsync();                                                 
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }        }

        public async Task<ICollection<Student>> GetClassesofStudent(int studentId)
        {
            try
            {
                return await _context.Students.Include(enr => enr.ClassErollments)
                                           .ThenInclude(cl => cl.Class)
                                           .AsNoTracking()
                                           .Where(stu => stu.StudentId == studentId)
                                           .ToListAsync();
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
            return null;
        }

        public async Task<Student> GetStudent(int studentId)
        {
            try
            {
                return await _context.Students.Include(enr => enr.ClassErollments)
                                                .ThenInclude(cl => cl.Class)
                                                .AsNoTracking()
                                                .FirstOrDefaultAsync(stu => stu.StudentId == studentId);
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);  
            }
            return null;
        }

        public async Task<ICollection<Student>> GetStudents()
        {
            try
            {
                return await _context.Students.AsNoTracking()
                                            .ToListAsync();
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
            return null;
        }

        public async Task<ICollection<Student>> GetStudentsByName(string name)
        {
            try
            {
                return await _context.Students.Include(enr => enr.ClassErollments)
                                           .ThenInclude(cl => cl.Class)
                                           .AsNoTracking()
                                           .Where(stu => stu.StudentName.Contains(name))
                                           .ToListAsync();
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message); 
            }
            return null;
        }

        public async Task<Student> PostClassforStudent(int studentId, int classId)
        {
            try
            {
                if (_context.Students.Contains(_context.Students.Where(s => s.StudentId == studentId).FirstOrDefault()))
                    if (_context.Classes.Contains(_context.Classes.Where(cl => cl.ClassId == classId).FirstOrDefault()))
                    {
                        var @erollment = await _context.ClassErollments.FindAsync(classId, studentId);
                        if (@erollment == null)
                        {
                            ClassErollment newEnroll = new ClassErollment();
                            newEnroll.ClassId = classId;
                            newEnroll.StudentId = studentId;
                           _context.ClassErollments.Add(newEnroll);
                            _context.SaveChanges();
                        }
                    }

            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
            return null;
        }

        public async Task UpdateStudent(Student student)
        {
            var _student = _context.Students.FirstOrDefault(stu => stu.StudentId == student.StudentId);
            if (_student != null) {
                _student.StudentName = student.StudentName;
            }
            await _context.SaveChangesAsync();
        }
    }
}
