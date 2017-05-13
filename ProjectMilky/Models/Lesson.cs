using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectMilky.Models
{
    public class Lesson
    {
        public Lesson()
        {
            this.Resources = new HashSet<File>();
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ApplicationUser Teacher { get; set; }

        public virtual ICollection<File> Resources { get; set; }
    }
}