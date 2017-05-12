using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectMilky.Models
{
    public class School
    {
        public School()
        {
            this.Students = new HashSet<Student>();
            this.Teachers = new HashSet<Teacher>();
            this.Subjects = new HashSet<Subject>();
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Student> Students { get; set; }

        public virtual ICollection<Teacher> Teachers { get; set; }

        public virtual ICollection<Subject> Subjects { get; set; }

    }
}