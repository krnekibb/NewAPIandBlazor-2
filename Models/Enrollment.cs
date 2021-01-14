﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIstuff.Models
{
    public enum Grade
    {
        A,
        B,
        C,
        D,
        F,
    }

    public class Enrollment
    {
        public int EnrollmentId { get; set; }

        public Grade? Grade { get; set; }

        public int StudentId { get; set; }
        public Student Student { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}