using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using ACCUniversity.Models;

namespace ACCUniversity.DAL
{
    public class SchoolInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<SchoolContext>
    {
        protected override void Seed(SchoolContext context)
        {
            var students = new List<Student> //initialize student db
            {
            new Student{FirstMidName="Shane",LastName="White",EnrollmentDate=DateTime.Parse("2005-09-01")},
            new Student{FirstMidName="Hector",LastName="Garcia",EnrollmentDate=DateTime.Parse("2002-09-01")},
            new Student{FirstMidName="Israel",LastName="Hernandez",EnrollmentDate=DateTime.Parse("2003-09-01")},
            new Student{FirstMidName="Jackson",LastName="Five",EnrollmentDate=DateTime.Parse("2002-09-01")},
            new Student{FirstMidName="Justin",LastName="Lyle",EnrollmentDate=DateTime.Parse("2002-09-01")},            
            };

            students.ForEach(s => context.Students.Add(s));
            context.SaveChanges();
            var courses = new List<Course> //initialize courses db
            {
            new Course{CourseID=1,Title="Intro to Web",Credits=3,},
            new Course{CourseID=2,Title="C# Intro",Credits=3,},
            new Course{CourseID=3,Title="C# Advanced",Credits=3,},            
            };
            courses.ForEach(s => context.Courses.Add(s));
            context.SaveChanges();
            var enrollments = new List<Enrollment> //initialize enrollments
            {
            new Enrollment{StudentID=1,CourseID=1,Grade=Grade.A},
            new Enrollment{StudentID=1,CourseID=2,Grade=Grade.C},
            new Enrollment{StudentID=1,CourseID=3,Grade=Grade.B},
            new Enrollment{StudentID=2,CourseID=1,Grade=Grade.B},
            new Enrollment{StudentID=2,CourseID=2,Grade=Grade.F},
            new Enrollment{StudentID=2,CourseID=3,Grade=Grade.F},
            new Enrollment{StudentID=3,CourseID=1},
            new Enrollment{StudentID=4,CourseID=2,},
            new Enrollment{StudentID=4,CourseID=3,Grade=Grade.F},
            new Enrollment{StudentID=5,CourseID=1,Grade=Grade.C},
            new Enrollment{StudentID=5,CourseID=2},
            new Enrollment{StudentID=5,CourseID=3,Grade=Grade.A},
            };
            enrollments.ForEach(s => context.Enrollments.Add(s));
            context.SaveChanges();
        }
    }
}