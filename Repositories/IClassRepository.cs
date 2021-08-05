using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Models1;

namespace TodoApi.Repositories
{
    public interface IClassRepository
    {
        Task<IEnumerable<Class>> GetClasses();
        Task<Class> GetClass(int classId);
        Task<Class> AddClass(Class @class);
        Task UpdateClass(Class @class);
        Task DeleteClass(int classId);
        Task<IEnumerable<Class>> GetStudentsofClass(int classId);
        Task<IEnumerable<Class>> GetClassesByName(string name);

        Task<Class> PostStudenttoClass(int classId, int studentId);
        Task<Class> DeleteStudentfromClass(int classId, int studentId);
    }
}
