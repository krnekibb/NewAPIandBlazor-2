using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace APIstuff.Models
{
    public class Course
    {
        public int CourseId { get; set; }

        public string Title { get; set; }

        public int Credits { get; set; }

        [JsonIgnore]
        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}
