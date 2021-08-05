using System;
using System.Collections.Generic;

#nullable disable

namespace TodoApi.Models1
{
    public partial class Student
    {
        //private static Student instance = null;
        //public static Student GetInstance
        //{
        //    get
        //    {
        //        if (instance == null)
        //            instance = new Student();
        //        return instance;
        //    }
        //}
        public Student()
        {
            ClassErollments = new HashSet<ClassErollment>();
        }

        public int StudentId { get; set; }
        public string StudentName { get; set; }

        public virtual ICollection<ClassErollment> ClassErollments { get; set; }
    }
}
