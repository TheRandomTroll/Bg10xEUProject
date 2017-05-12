using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectMilky.Models
{
    public class Student : ApplicationUser
    {
        public Student()
        {
            this.Grades = new List<Grade>();
        }
        public ICollection<Grade> Grades { get; set; }

        public virtual School School { get; set; }
    }
}