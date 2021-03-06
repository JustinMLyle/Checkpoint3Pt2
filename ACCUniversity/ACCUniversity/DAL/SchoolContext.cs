﻿using ACCUniversity.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace ACCUniversity.DAL
{
    public class SchoolContext : DbContext
    {
        //name of connection string
        public SchoolContext() : base("SchoolContext")
        {
        }

        //create the database objects
        public DbSet<Course> Courses { get; set; }        
        public DbSet<Department> Departments { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Student> Students { get; set; }        
        public DbSet<OfficeAssignment> OfficeAssignments { get; set; }
        


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            //modelBuilder.Entity<Course>()
            //    .HasMany(c => c.Instructors).WithMany(i => i.Courses)
            //    .Map(t => t.MapLeftKey("CourseID")
            //    .MapRightKey("InstructorID")
            //    .ToTable("CourseInstructor"));
        }

        public System.Data.Entity.DbSet<ACCUniversity.Models.CampusLocations> CampusLocations { get; set; }
    }
}