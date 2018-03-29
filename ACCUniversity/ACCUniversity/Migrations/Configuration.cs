namespace ACCUniversity.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Data.Entity;
    using ACCUniversity.Models;
    using ACCUniversity.DAL;

    using System.Data.Entity.Migrations;
    

    internal sealed class Configuration : DbMigrationsConfiguration<SchoolContext>
    {
       public void AddOrUpdateInstructor(SchoolContext schoolContext, string courseTitle, string instructorName)
        {
            var crs = schoolContext.Courses.SingleOrDefault(c => c.Title == courseTitle);
            var inst = crs.Instructors.SingleOrDefault(i => i.LastName == instructorName);
            if (inst == null)
                crs.Instructors.Add(schoolContext.Instructors.Single(i => i.LastName == instructorName));
        }

        public void Seeder(SchoolContext context)
        {
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


            students.ForEach(s => context.Students.Add(s));
            context.SaveChanges();

            var instructors = new List<Instructor>
            {
                new Instructor { FirstMidName = "ShaneTwo",LastName = "Whiter", HireDate = DateTime.Parse("1995-03-11") },
                new Instructor { FirstMidName = "Electric",LastName = "Boogaloo", HireDate = DateTime.Parse("2002-07-06") },
                new Instructor { FirstMidName = "Ivana",   LastName = "KandyVar", HireDate = DateTime.Parse("1998-07-01")},
            };

            instructors.ForEach(s => context.Instructors.Add(s));
            context.SaveChanges();

            var departments = new List<Department>
            {
                new Department { Name = "Web", Budget = 350000, StartDate = DateTime.Parse("2007-09-01"), InstructorID  = instructors.Single( i => i.LastName == "Whiter").ID },
                new Department { Name = "C#", Budget = 100000, StartDate = DateTime.Parse("2007-09-01"), InstructorID  = instructors.Single( i => i.LastName == "Boogaloo").ID },
                new Department { Name = "C++", Budget = 350000, StartDate = DateTime.Parse("2007-09-01"), InstructorID  = instructors.Single( i => i.LastName == "KandyVar").ID },
            };

            departments.ForEach(s => context.Departments.AddOrUpdate(p => p.Name, s));
            context.SaveChanges();

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
            courses.ForEach(s => context.Courses.Add(s));
            context.SaveChanges();

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
            officeAssignments.ForEach(s => context.OfficeAssignments.AddOrUpdate(p => p.InstructorID, s));
            context.SaveChanges();

            AddOrUpdateInstructor(context, "Intro to Web", "Whiter");
            AddOrUpdateInstructor(context, "Intro to C#", "Boogaloo");
            AddOrUpdateInstructor(context, "C# Advance", "KandyVar");

            context.SaveChanges();


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
                var enrollmentInDataBase = context.Enrollments.Where(
                    s =>
                         s.Student.ID == e.StudentID &&
                         s.Course.CourseID == e.CourseID).SingleOrDefault();
                if (enrollmentInDataBase == null)
                {
                    context.Enrollments.Add(e);
                }
            }
            context.SaveChanges();


        }

        public Configuration()
        {
            AutomaticMigrationsEnabled = false;            
        }

        protected override void Seed(SchoolContext context)
        {
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


            students.ForEach(s => context.Students.Add(s));
            context.SaveChanges();

            var instructors = new List<Instructor>
            {
                new Instructor { FirstMidName = "ShaneTwo",LastName = "Whiter", HireDate = DateTime.Parse("1995-03-11") },
                new Instructor { FirstMidName = "Electric",LastName = "Boogaloo", HireDate = DateTime.Parse("2002-07-06") },
                new Instructor { FirstMidName = "Ivana",   LastName = "KandyVar", HireDate = DateTime.Parse("1998-07-01")},
            };

            instructors.ForEach(s => context.Instructors.Add(s));
            context.SaveChanges();

            var departments = new List<Department>
            {
                new Department { Name = "Web", Budget = 350000, StartDate = DateTime.Parse("2007-09-01"), InstructorID  = instructors.Single( i => i.LastName == "Whiter").ID },
                new Department { Name = "C#", Budget = 100000, StartDate = DateTime.Parse("2007-09-01"), InstructorID  = instructors.Single( i => i.LastName == "Boogaloo").ID },
                new Department { Name = "C++", Budget = 350000, StartDate = DateTime.Parse("2007-09-01"), InstructorID  = instructors.Single( i => i.LastName == "KandyVar").ID },
            };

            departments.ForEach(s => context.Departments.AddOrUpdate(p => p.Name, s));
            context.SaveChanges();

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
            courses.ForEach(s => context.Courses.Add(s));
            context.SaveChanges();

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
            officeAssignments.ForEach(s => context.OfficeAssignments.AddOrUpdate(p => p.InstructorID, s));
            context.SaveChanges();

            AddOrUpdateInstructor(context, "Intro to Web", "Whiter");
            AddOrUpdateInstructor(context, "Intro to C#", "Boogaloo");
            AddOrUpdateInstructor(context, "C# Advance", "KandyVar");

            context.SaveChanges();


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
                var enrollmentInDataBase = context.Enrollments.Where(
                    s =>
                         s.Student.ID == e.StudentID &&
                         s.Course.CourseID == e.CourseID).SingleOrDefault();
                if (enrollmentInDataBase == null)
                {
                    context.Enrollments.Add(e);
                }
            }
            context.SaveChanges();
  
            
        }
    }
}
