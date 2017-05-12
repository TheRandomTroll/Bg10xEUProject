using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectMilky.Models
{
    public class Teacher : ApplicationUser
    {
        public virtual Subject SubjectTaught { get; set; }
    }
}