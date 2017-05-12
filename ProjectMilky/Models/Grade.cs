using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectMilky.Models
{
    public class Grade
    {
        public int Id { get; set; }

        public virtual Subject Subject { get; set; }

        public double Value { get; set; }
    }
}