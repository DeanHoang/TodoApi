using System;
using System.Collections.Generic;

#nullable disable

namespace TodoApi.Models1
{
    public partial class ClassErollment
    {
        //private static ClassErollment instance = null;
        //public static ClassErollment GetInstance
        //{
        //    get
        //    {
        //        if (instance == null)
        //            instance = new ClassErollment();
        //        return instance;
        //    }
        //}
       // private ClassErollment() { }
        public int ClassId { get; set; }
        public int StudentId { get; set; }

        public virtual Class Class { get; set; }
        public virtual Student Student { get; set; }
    }
}
