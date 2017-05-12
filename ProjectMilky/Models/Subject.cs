using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectMilky.Models
{
    public class Subject
    {
        public Subject()
        {
            this.Students = new List<ApplicationUser>();
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ApplicationUser Teacher { get; set; }

        public virtual ICollection<ApplicationUser> Students { get; set; }
    }
}