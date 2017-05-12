using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectMilky.Models
{
    using System.ComponentModel.DataAnnotations.Schema;
    public class Teacher : ApplicationUser
    {
        public virtual Subject SubjectTaught { get; set; }
    }
}