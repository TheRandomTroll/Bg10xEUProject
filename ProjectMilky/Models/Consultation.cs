using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectMilky.Models
{
    public class Consultation
    {
        public int Id { get; set; }

        public string Subject { get; set; }

        public string Description { get; set; }

        public string YoutubeUrl { get; set; }

        public virtual ApplicationUser Teacher { get; set; }
    }
}