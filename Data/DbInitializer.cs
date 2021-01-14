using APIstuff.DataAccess;
using APIstuff.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIstuff.Data
{
    public class DbInitializer
    {
        public static void Initialize(ApiDbContext context)
        {
            context.Database.EnsureCreated();

            //Look for students, if any DB has been seeded
            if (context.Students.Any())
            {
                return;
            }

            var students = new Student[]
            {
                new Student{FirstName="Carson",LastName="Alexander",EnrollmentDate=DateTime.Parse("2019-09-01")},
                new Student{FirstName="Meredith",LastName="Alonso",EnrollmentDate=DateTime.Parse("2017-09-01")},
                new Student{FirstName="Arturo",LastName="Anand",EnrollmentDate=DateTime.Parse("2018-09-01")},
                new Student{FirstName="Gytis",LastName="Barzdukas",EnrollmentDate=DateTime.Parse("2017-09-01")},
                new Student{FirstName="Yan",LastName="Li",EnrollmentDate=DateTime.Parse("2017-09-01")},
                new Student{FirstName="Peggy",LastName="Justice",EnrollmentDate=DateTime.Parse("2016-09-01")},
                new Student{FirstName="Laura",LastName="Norman",EnrollmentDate=DateTime.Parse("2018-09-01")},
                new Student{FirstName="Nino",LastName="Olivetto",EnrollmentDate=DateTime.Parse("2019-09-01")}
            };

            context.Students.AddRange(students);
            context.SaveChanges();

            var courses = new Course[]
            {
                new Course{Title="Chemistry",Credits=3},
                new Course{Title="Microeconomics",Credits=3},
                new Course{Title="Macroeconomics",Credits=3},
                new Course{Title="Calculus",Credits=4},
                new Course{Title="Trigonometry",Credits=4},
                new Course{Title="Composition",Credits=3},
                new Course{Title="Literature",Credits=4}
            };

            context.Courses.AddRange(courses);
            context.SaveChanges();

            var enrollments = new Enrollment[]
            {
                new Enrollment{StudentId=1,CourseId=1,Grade=Grade.A},
                new Enrollment{StudentId=1,CourseId=2,Grade=Grade.C},
                new Enrollment{StudentId=1,CourseId=3,Grade=Grade.B},
                new Enrollment{StudentId=2,CourseId=4,Grade=Grade.B},
                new Enrollment{StudentId=2,CourseId=5,Grade=Grade.F},
                new Enrollment{StudentId=2,CourseId=6,Grade=Grade.F},
                new Enrollment{StudentId=3,CourseId=7},
                new Enrollment{StudentId=4,CourseId=1},
                new Enrollment{StudentId=4,CourseId=2,Grade=Grade.F},
                new Enrollment{StudentId=5,CourseId=3,Grade=Grade.C},
                new Enrollment{StudentId=6,CourseId=4},
                new Enrollment{StudentId=7,CourseId=5,Grade=Grade.A},
            };

            context.Enrollments.AddRange(enrollments);
            context.SaveChanges();
        }
    }
}
