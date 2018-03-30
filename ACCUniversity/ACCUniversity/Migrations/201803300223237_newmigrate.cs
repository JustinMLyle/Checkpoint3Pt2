using System;
using System.Data.Entity.Migrations;

public partial class newmigrate : DbMigration
{
    public override void Up()
    {
        CreateTable(
            "dbo.Courses",
            c => new
                {
                    CourseID = c.Int(nullable: false),
                    Title = c.String(maxLength: 50),
                    Credits = c.Int(nullable: false),
                    DepartmentID = c.Int(nullable: false),
                })
            .PrimaryKey(t => t.CourseID)
            .ForeignKey("dbo.Departments", t => t.DepartmentID, cascadeDelete: true)
            .Index(t => t.DepartmentID);
        
        CreateTable(
            "dbo.Departments",
            c => new
                {
                    DepartmentID = c.Int(nullable: false, identity: true),
                    Name = c.String(maxLength: 50),
                    Budget = c.Decimal(nullable: false, storeType: "money"),
                    StartDate = c.DateTime(nullable: false),
                    InstructorID = c.Int(),
                })
            .PrimaryKey(t => t.DepartmentID)
            .ForeignKey("dbo.Instructors", t => t.InstructorID)
            .Index(t => t.InstructorID);
        
        CreateTable(
            "dbo.Instructors",
            c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    LastName = c.String(nullable: false, maxLength: 50),
                    FirstName = c.String(nullable: false, maxLength: 50),
                    HireDate = c.DateTime(nullable: false),
                })
            .PrimaryKey(t => t.ID);
        
        CreateTable(
            "dbo.OfficeAssignments",
            c => new
                {
                    InstructorID = c.Int(nullable: false),
                    Location = c.String(maxLength: 50),
                })
            .PrimaryKey(t => t.InstructorID)
            .ForeignKey("dbo.Instructors", t => t.InstructorID)
            .Index(t => t.InstructorID);
        
        CreateTable(
            "dbo.Enrollments",
            c => new
                {
                    EnrollmentID = c.Int(nullable: false, identity: true),
                    CourseID = c.Int(nullable: false),
                    StudentID = c.Int(nullable: false),
                    Grade = c.Int(),
                })
            .PrimaryKey(t => t.EnrollmentID)
            .ForeignKey("dbo.Courses", t => t.CourseID, cascadeDelete: true)
            .ForeignKey("dbo.Students", t => t.StudentID, cascadeDelete: true)
            .Index(t => t.CourseID)
            .Index(t => t.StudentID);
        
        CreateTable(
            "dbo.Students",
            c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    LastName = c.String(nullable: false, maxLength: 50),
                    FirstName = c.String(nullable: false, maxLength: 50),
                    EnrollmentDate = c.DateTime(nullable: false),
                })
            .PrimaryKey(t => t.ID);
        
        CreateTable(
            "dbo.InstructorCourses",
            c => new
                {
                    Instructor_ID = c.Int(nullable: false),
                    Course_CourseID = c.Int(nullable: false),
                })
            .PrimaryKey(t => new { t.Instructor_ID, t.Course_CourseID })
            .ForeignKey("dbo.Instructors", t => t.Instructor_ID, cascadeDelete: true)
            .ForeignKey("dbo.Courses", t => t.Course_CourseID, cascadeDelete: true)
            .Index(t => t.Instructor_ID)
            .Index(t => t.Course_CourseID);
        
    }
    
    public override void Down()
    {
        DropForeignKey("dbo.Enrollments", "StudentID", "dbo.Students");
        DropForeignKey("dbo.Enrollments", "CourseID", "dbo.Courses");
        DropForeignKey("dbo.Courses", "DepartmentID", "dbo.Departments");
        DropForeignKey("dbo.Departments", "InstructorID", "dbo.Instructors");
        DropForeignKey("dbo.OfficeAssignments", "InstructorID", "dbo.Instructors");
        DropForeignKey("dbo.InstructorCourses", "Course_CourseID", "dbo.Courses");
        DropForeignKey("dbo.InstructorCourses", "Instructor_ID", "dbo.Instructors");
        DropIndex("dbo.InstructorCourses", new[] { "Course_CourseID" });
        DropIndex("dbo.InstructorCourses", new[] { "Instructor_ID" });
        DropIndex("dbo.Enrollments", new[] { "StudentID" });
        DropIndex("dbo.Enrollments", new[] { "CourseID" });
        DropIndex("dbo.OfficeAssignments", new[] { "InstructorID" });
        DropIndex("dbo.Departments", new[] { "InstructorID" });
        DropIndex("dbo.Courses", new[] { "DepartmentID" });
        DropTable("dbo.InstructorCourses");
        DropTable("dbo.Students");
        DropTable("dbo.Enrollments");
        DropTable("dbo.OfficeAssignments");
        DropTable("dbo.Instructors");
        DropTable("dbo.Departments");
        DropTable("dbo.Courses");
    }
}
