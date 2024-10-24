using WebRoster.Models;
using Microsoft.EntityFrameworkCore;
namespace WebRoster.Data;
public class RosterContext : DbContext{
    public RosterContext() : base(){}
    public RosterContext(DbContextOptions<RosterContext> options) : base(options){}
    public DbSet<Role> Roles { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserDetail> UserDetails { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<CourseInstructor> CourseInstructors { get; set; }
    public DbSet<CourseStudent> CourseStudents { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // CourseInstructor relationship
        modelBuilder.Entity<CourseInstructor>()
            .HasKey(ci => ci.ID);

        modelBuilder.Entity<CourseInstructor>()
            .HasOne(ci => ci.Course)
            .WithMany(c => c.CourseInstructors)
            .HasForeignKey(ci => ci.CourseID)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<CourseInstructor>()
            .HasOne(ci => ci.Instructor)
            .WithMany(u => u.CourseInstructors)
            .HasForeignKey(ci => ci.InstructorID)
            .OnDelete(DeleteBehavior.Cascade);

        // CourseStudent relationship
        modelBuilder.Entity<CourseStudent>()
            .HasKey(ce => ce.ID);

        modelBuilder.Entity<CourseStudent>()
            .HasOne(ce => ce.Course)
            .WithMany(c => c.CourseStudents)
            .HasForeignKey(ce => ce.CourseID)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<CourseStudent>()
            .HasOne(ce => ce.Student)
            .WithMany(u => u.CourseStudents)
            .HasForeignKey(ce => ce.StudentID)
            .OnDelete(DeleteBehavior.Cascade);

        // UserDetail relationship
        modelBuilder.Entity<UserDetail>()
            .HasOne(ud => ud.User)
            .WithOne(u => u.UserDetail)
            .HasForeignKey<UserDetail>(ud => ud.UserID)
            .OnDelete(DeleteBehavior.Cascade);
        
        // Seeding data
        // Seed Roles
        modelBuilder.Entity<Role>().HasData(
            new Role { ID = 1, RoleName = "Teacher"},
            new Role { ID = 2, RoleName = "Student"}
        );
        // Seed Users
        modelBuilder.Entity<User>().HasData(
            new User { ID = 1, UserName = "teacher1", Password = "hashedpassword1", RoleID = 1, FirstName = "teacher1", LastName = "teacher1"},
            new User { ID = 2, UserName = "teacher2", Password = "hashedpassword2", RoleID = 1, FirstName = "teacher2", LastName = "teacher2"},
            new User { ID = 3, UserName = "student1", Password = "hashedpassword3", RoleID = 2, FirstName = "student1", LastName = "student1"}
        );

        // Seed Courses
        modelBuilder.Entity<Course>().HasData(
            new Course { ID = 1, CourseName = "Math" },
            new Course { ID = 2, CourseName = "Science" }
        );

        // Seed Course Instructors
        modelBuilder.Entity<CourseInstructor>().HasData(
            new CourseInstructor { ID = 1, CourseID = 1, InstructorID = 2 },
            new CourseInstructor { ID = 2, CourseID = 2, InstructorID = 2 }
        );

        // Seed Course Enrollments
        modelBuilder.Entity<CourseStudent>().HasData(
            new CourseStudent { ID = 1, CourseID = 1, StudentID = 3 }
        );
    }
}