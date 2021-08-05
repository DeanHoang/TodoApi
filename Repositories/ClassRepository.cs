using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Models1;

namespace TodoApi.Repositories
{
    public class ClassRepository : IClassRepository
    {
        private readonly SchoolDBContext _context;
        public ClassRepository(SchoolDBContext schoolDBContext)
        {
            _context = schoolDBContext;
        }
        public async Task<Class> AddClass(Class @class)
        {
            try
            {
                if (!_context.Classes.Contains(@class))
                {
                    _context.Classes.Add(@class);
                    await _context.SaveChangesAsync();
                }
                return @class;
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
            return null;
        }

        public async Task DeleteClass(int classId)
        {
            try
            {
                var @class = await _context.Classes.FindAsync(classId);
                var _list = _context.ClassErollments.Where(clr => clr.ClassId == classId)
                                                        .AsNoTracking()
                                                        .ToList();
                foreach(var item in _list)
                {
                    _context.ClassErollments.Remove(item);
                   // _context.SaveChanges();
                }
                _context.Classes.Remove(@class);
                _context.SaveChanges();
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message); 
            }
        }

        public async Task<Class> DeleteStudentfromClass(int classId, int studentId)
        {
            try
            {
                var erollment = await _context.ClassErollments.FindAsync(classId, studentId);
                if (erollment != null)
                {
                    _context.ClassErollments.Remove(erollment);
                    _context.SaveChanges();
                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
            return null; 
        }

        public async Task<Class> GetClass(int classId)
        {
            try
            {
                return await _context.Classes.Include(enrol => enrol.ClassErollments)
                                                   .ThenInclude(s => s.Student)
                                                   .AsNoTracking()
                                                   .FirstOrDefaultAsync(c => c.ClassId == classId);
            }
            catch (Exception e )
            {

                Console.WriteLine(e.Message);
            }
            return null;
        }

        public async Task<IEnumerable<Class>> GetClasses()
        {
            try
            {
                return await _context.Classes.AsNoTracking()
                                              .ToListAsync();
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message); 
            }
            return null;
        }

        public async Task<IEnumerable<Class>> GetClassesByName(string name)
        {
            try
            {
                return await _context.Classes.Include(enrol => enrol.ClassErollments)
                                                .ThenInclude(stu => stu.Student)
                                                .Where(cs => cs.ClassName.Contains(name))
                                                .AsNoTracking()
                                                .ToListAsync();
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message); ;
            }
            return null;
        }

        public async Task<IEnumerable<Class>> GetStudentsofClass(int classId)
        {
            try
            {
                return await _context.Classes.Include(enrol => enrol.ClassErollments)
                                                .ThenInclude(s => s.Student)
                                                .Where(c => c.ClassId == classId)
                                                .ToListAsync();
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
            return null;
            
           
        }

        public async Task<Class> PostStudenttoClass(int classId, int studentId)
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

        public async Task UpdateClass(Class @class)
        {
            try
            {
                var _class = await _context.Classes.FirstOrDefaultAsync(c => c.ClassId == @class.ClassId);
                _class.ClassName = @class.ClassName;
                _context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
