using System;
using System.Collections.Generic;

#nullable disable

namespace TodoApi.Models1
{
    public partial class ClassErollment
    {
        public int ClassId { get; set; }
        public int StudentId { get; set; }

        public virtual Class Class { get; set; }
        public virtual Student Student { get; set; }
    }
}
