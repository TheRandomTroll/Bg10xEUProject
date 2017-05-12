using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectMilky.Models
{
    public class School
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<ApplicationUser> Students { get; set; }

        public virtual ICollection<ApplicationUser> Teachers { get; set; }

        public virtual ICollection<Subject> Subjects { get; set; }

    }
}