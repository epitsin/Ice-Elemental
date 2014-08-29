namespace FurnitureSystem.Data.Migrations
{
    using System.Data.Entity.Migrations;
    using System.Linq;

    using FurnitureSystem.Data;

    public sealed class Configuration : DbMigrationsConfiguration<FurnitureSystemDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
            this.ContextKey = "StudentSystem.Data.StudentSystemDbContext";
        }

        //protected override void Seed(FurnitureSystemDbContext context)
        //{
        //    this.SeedCourses(context);
        //    this.SeedStudents(context);
        //}

        //private void SeedStudents(FurnitureSystemDbContext context)
        //{
            //if (context.Students.Any())
            //{
            //    return;
            //}

            //context.Students.Add(new Student
            //{
            //    Name = "Evlogi Hristov",
            //    Number = "11112222"
            //});

            //context.Students.Add(new Student
            //{
            //    Name = "Ivaylo Kenov",
            //    Number = "11113333"
            //});

            //context.Students.Add(new Student
            //{
            //    Name = "Doncho Minkov",
            //    Number = "11114444"
            //});

            //context.Students.Add(new Student
            //{
            //    Name = "Nikolay Kostov",
            //    Number = "11115555"
            //});
        //}

        //private void SeedCourses(FurnitureSystemDbContext context)
        //{
            //if (context.Courses.Any())
            //{
            //    return;
            //}

            //context.Courses.Add(new Course
            //{
            //    Name = "Seeded course",
            //    Description = "Initial course for testing"
            //});
        //}
    }
}
