using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Models1;

namespace TodoApi.Repositories
{
    public interface IStudentRepository
    {
        Task<ICollection<Student>> GetStudents();
        Task<Student> GetStudent(int studentId);
        Task<Student> AddStudent(Student @student);
        Task UpdateStudent(Student @student);
        Task DeleteStudent(int studentId);
        Task<ICollection<Student>> GetClassesofStudent(int studentId);
        Task<ICollection<Student>> GetStudentsByName(string name);

        Task<Student> PostClassforStudent(int studentId, int classId);
        Task DeleteClassforStudent(int studentId, int classId);
    }
}
