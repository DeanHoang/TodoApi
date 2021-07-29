using System;
using System.Collections.Generic;

#nullable disable

namespace TodoApi.Models1
{
    public partial class Student
    {
        public Student()
        {
            ClassErollments = new HashSet<ClassErollment>();
        }

        public int StudentId { get; set; }
        public string StudentName { get; set; }

        public virtual ICollection<ClassErollment> ClassErollments { get; set; }
    }
}
