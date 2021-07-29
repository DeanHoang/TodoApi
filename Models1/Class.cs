using System;
using System.Collections.Generic;

#nullable disable

namespace TodoApi.Models1
{
    public partial class Class
    {
        public Class()
        {
            ClassErollments = new HashSet<ClassErollment>();
        }

        public int ClassId { get; set; }
        public string ClassName { get; set; }

        public virtual ICollection<ClassErollment> ClassErollments { get; set; }
    }
}
