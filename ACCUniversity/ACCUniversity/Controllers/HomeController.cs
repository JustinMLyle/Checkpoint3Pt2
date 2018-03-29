using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ACCUniversity.Migrations;
using ACCUniversity.DAL;
using ACCUniversity.ViewModels;
using ACCUniversity.Models;
using System.Data.Entity.Migrations;

namespace ACCUniversity.Controllers
{
    public class HomeController : Controller
    {
        private SchoolContext db = new SchoolContext();
        public ActionResult Index()
        {
            Configuration newSeed = new Configuration();



            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            var students = new List<Student> //initialize student db
            {
            new Student{FirstMidName="Shane",LastName="White",EnrollmentDate=DateTime.Parse("2005-09-01")},
            new Student{FirstMidName="Hector",LastName="Garcia",EnrollmentDate=DateTime.Parse("2002-09-01")},
            new Student{FirstMidName="Israel",LastName="Hernandez",EnrollmentDate=DateTime.Parse("2003-09-01")},
            new Student{FirstMidName="Jackson",LastName="Five",EnrollmentDate=DateTime.Parse("2002-09-01")},
            new Student{FirstMidName="Justin",LastName="Lyle",EnrollmentDate=DateTime.Parse("2002-09-01")},
            };


            students.ForEach(s => db.Students.Add(s));
            db.SaveChanges();

            var instructors = new List<Instructor>
            {
                new Instructor { FirstMidName = "ShaneTwo",LastName = "Whiter", HireDate = DateTime.Parse("1995-03-11") },
                new Instructor { FirstMidName = "Electric",LastName = "Boogaloo", HireDate = DateTime.Parse("2002-07-06") },
                new Instructor { FirstMidName = "Ivana",   LastName = "KandyVar", HireDate = DateTime.Parse("1998-07-01")},
            };

            instructors.ForEach(s => db.Instructors.Add(s));
            db.SaveChanges();

            var departments = new List<Department>
            {
                new Department { Name = "Web", Budget = 350000, StartDate = DateTime.Parse("2007-09-01"), InstructorID  = instructors.Single( i => i.LastName == "Whiter").ID },
                new Department { Name = "C#", Budget = 100000, StartDate = DateTime.Parse("2007-09-01"), InstructorID  = instructors.Single( i => i.LastName == "Boogaloo").ID },
                new Department { Name = "C++", Budget = 350000, StartDate = DateTime.Parse("2007-09-01"), InstructorID  = instructors.Single( i => i.LastName == "KandyVar").ID },
            };

            foreach (var department in departments){
                Dep
                db.Entry(department).State = System.Data.Entity.EntityState.Added;
                db.SaveChanges();
            }
            //departments.ForEach(s => db.Departments.AddOrUpdate(p => p.Name, s));
            

            var courses = new List<Course> //initialize courses db
            {
            new Course{CourseID=1,Title="Intro to Web",Credits=3, DepartmentID = departments.Single( s => s.Name == "Web").DepartmentID,
                  Instructors = new List<Instructor>()
            },
            new Course{CourseID=2,Title="C# Intro",Credits=3,  DepartmentID = departments.Single( s => s.Name == "C#").DepartmentID,
                  Instructors = new List<Instructor>()
            },
            new Course{CourseID=3,Title="C# Advanced",Credits=3,  DepartmentID = departments.Single( s => s.Name == "c++").DepartmentID,
                  Instructors = new List<Instructor>()
            },

            };
            courses.ForEach(s => db.Courses.Add(s));
            db.SaveChanges();

            var officeAssignments = new List<OfficeAssignment>
            {
                new OfficeAssignment {
                    InstructorID = instructors.Single( i => i.LastName == "Whiter").ID,
                    Location = "Building A" },
                new OfficeAssignment {
                    InstructorID = instructors.Single( i => i.LastName == "Boogaloo").ID,
                    Location = "Building B" },
                new OfficeAssignment {
                    InstructorID = instructors.Single( i => i.LastName == "KandyVar").ID,
                    Location = "Building C" },
            };
            officeAssignments.ForEach(s => db.OfficeAssignments.AddOrUpdate(p => p.InstructorID, s));
            db.SaveChanges();

            newSeed.AddOrUpdateInstructor(db, "Intro to Web", "Whiter");
            newSeed.AddOrUpdateInstructor(db, "Intro to C#", "Boogaloo");
            newSeed.AddOrUpdateInstructor(db, "C# Advance", "KandyVar");

            db.SaveChanges();


            var enrollments = new List<Enrollment> //initialize enrollments
            {
           new Enrollment {
                    StudentID = students.Single(s => s.LastName == "Whiter").ID,
                    CourseID = courses.Single(c => c.Title == "Intro to Web" ).CourseID,
                    Grade = Grade.A
                },
                 new Enrollment {
                    StudentID = students.Single(s => s.LastName == "Lyle").ID,
                    CourseID = courses.Single(c => c.Title == "Intro to C#" ).CourseID,
                    Grade = Grade.C
                 },
                 new Enrollment {
                    StudentID = students.Single(s => s.LastName == "Garcia").ID,
                    CourseID = courses.Single(c => c.Title == "C# Advance" ).CourseID,
                    Grade = Grade.B
                 },
                 new Enrollment {
                     StudentID = students.Single(s => s.LastName == "Five").ID,
                    CourseID = courses.Single(c => c.Title == "Intro to C#" ).CourseID,
                    Grade = Grade.B
                 },
                 new Enrollment {
                     StudentID = students.Single(s => s.LastName == "Hernandez").ID,
                    CourseID = courses.Single(c => c.Title == "intro to web" ).CourseID,
                    Grade = Grade.B
                 },
            };

            foreach (Enrollment e in enrollments)
            {
                var enrollmentInDataBase = db.Enrollments.Where(
                    s =>
                         s.Student.ID == e.StudentID &&
                         s.Course.CourseID == e.CourseID).SingleOrDefault();
                if (enrollmentInDataBase == null)
                {
                    db.Enrollments.Add(e);
                }
            }
            db.SaveChanges();

        
        
            return View();
        }

        public ActionResult About()
        {
            IQueryable<EnrollmentDateGroup> data = from student in db.Students
                                                   group student by student.EnrollmentDate into dateGroup
                                                   select new EnrollmentDateGroup()
                                                   {
                                                       EnrollmentDate = dateGroup.Key,
                                                       StudentCount = dateGroup.Count()
                                                   };
            return View(data.ToList());
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}