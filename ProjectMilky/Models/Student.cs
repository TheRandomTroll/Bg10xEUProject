using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectMilky.Models
{
    using System.ComponentModel.DataAnnotations.Schema;
    public class Student : ApplicationUser
    {
        public Student()
        {
            this.Grades = new List<Grade>();
        }
        public ICollection<Grade> Grades { get; set; }
    }
}