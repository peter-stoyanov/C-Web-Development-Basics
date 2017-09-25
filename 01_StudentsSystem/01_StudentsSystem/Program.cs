using Microsoft.EntityFrameworkCore;
using StudentsSystem.DAL;
using StudentsSystem.Enums;
using StudentsSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StudentsSystem
{
    public class Program
    {
        private static Random random = new Random();

        private static void Main(string[] args)
        {
            using (var db = new StudentSystemDbContext())
            {
                //DeletePreviosData(db);

                db.Database.Migrate();

                //SeedStudentAndCourses(db);
                //SeedLicenses(db);

                //Task01_PrintStudentHomeworks(db);
                //Task02_PrintCoursesResources(db);
                //Task03_PrintCoursesWithMoreResources(db);
                //Task04_PrintCoursesOnAGivenDay(db, DateTime.Now.AddDays(20));
                //Task05_PrintStudentsWithCoursePrices(db);
                //Task06_PrintCoursesResourcesLicenses(db);
                //Task07_PrintStudentCoursesResourcesLicenses(db);
            }
        }

        private static void Task07_PrintStudentCoursesResourcesLicenses(StudentSystemDbContext db)
        {
            var reportData = db
                .Students
                .Select(s => new
                {
                    s.Name,
                    CoursesCount = s.Courses.Count,
                    ResoursesCount = s.Courses.Select(sc => sc.Course.Resources.Count()).Sum(),
                    LicensesCount = s.Courses.Select(sc => sc.Course.Resources.Select(r => r.Licenses.Count()).Sum()).Sum()
                })
                .OrderByDescending(entry => entry.CoursesCount)
                .OrderByDescending(entry => entry.ResoursesCount)
                .ThenBy(entry => entry.Name)
                .ToList();

            foreach (var entry in reportData)
            {
                Console.WriteLine($"Student name: {entry.Name}");
                Console.WriteLine($"## Courses Count: {entry.CoursesCount}");
                Console.WriteLine($"## Resources Count: {entry.ResoursesCount}");
                Console.WriteLine($"## Licenses Count: {entry.LicensesCount}");
            }
        }

        private static void Task06_PrintCoursesResourcesLicenses(StudentSystemDbContext db)
        {
            var reportData = db
                .Courses
                .Select(c => new
                {
                    CourseName = c.Name,
                    Resources = c.Resources.Select(r => new { r.Name, Licenses = r.Licenses })
                })
                .OrderByDescending(entry => entry.Resources.Count())
                .ThenBy(entry => entry.CourseName)
                .ToList();

            foreach (var entry in reportData)
            {
                Console.WriteLine($"Course name: {entry.CourseName}");
                var resources = entry.Resources.Select(r => new
                {
                    ResourceName = r.Name,
                    Licenses = r.Licenses.ToList()
                })
                .OrderByDescending(sub => sub.Licenses.Count)
                .ThenBy(sub => sub.ResourceName)
                .ToList();

                foreach (var resource in resources)
                {
                    Console.WriteLine($"## Resource: {resource.ResourceName}");
                    foreach (var license in resource.Licenses)
                    {
                        Console.WriteLine($"--- License: {license.Name}");
                    }
                }
            }
        }

        private static void Task04_PrintCoursesOnAGivenDay(StudentSystemDbContext db, DateTime date)
        {
            var reportData = db
               .Courses
               .Where(c => date > c.StartDate && date < c.EndDate)
               .Select(c => new
               {
                   CourseName = c.Name,
                   StartDate = c.StartDate,
                   EndDate = c.EndDate,
                   StudentsEnrolled = c.Students.Count
               })
               .OrderByDescending(entry => entry.StudentsEnrolled)
               .ThenByDescending(entry => entry.EndDate.Subtract(entry.StartDate))
               .ToList();

            foreach (var entry in reportData)
            {
                Console.WriteLine($"Course name: {entry.CourseName}");
                Console.WriteLine($"## Start date: {entry.StartDate: dd MMM yyyy}");
                Console.WriteLine($"## End date: {entry.EndDate: dd MMM yyyy}");
                Console.WriteLine($"## Duration: {(entry.EndDate - entry.StartDate).TotalDays:f0}");
                Console.WriteLine($"## Students count: {entry.StudentsEnrolled}");
            }
        }

        private static void Task05_PrintStudentsWithCoursePrices(StudentSystemDbContext db)
        {
            var reportData = db
                .Students
                //.Include(s => s.Courses)
                //.ThenInclude(c => c.)
                .Select(s => new
                {
                    StudentName = s.Name,
                    CoursesCount = s.Courses.Count,
                    TotalPrice = s.Courses.Select(sc => sc.Course.Price).Sum()
                })
                .OrderByDescending(entry => entry.TotalPrice)
                .ThenByDescending(entry => entry.CoursesCount)
                .ThenBy(entry => entry.StudentName)
                .ToList();

            foreach (var entry in reportData)
            {
                Console.WriteLine($"Student name: {entry.StudentName}");
                Console.WriteLine($"## Total price: {entry.TotalPrice}");
                Console.WriteLine($"## Average price: {(entry.CoursesCount == 0 ? 0 : entry.TotalPrice / entry.CoursesCount):F2}");
            }
        }

        private static void Task03_PrintCoursesWithMoreResources(StudentSystemDbContext db)
        {
            var reportData = db
                .Courses
                .Where(c => c.Resources.Count() > 5)
                .Select(c => new
                {
                    CourseName = c.Name,
                    StartDate = c.StartDate,
                    ResourcesCount = c.Resources.Count
                })
                .OrderByDescending(entry => entry.ResourcesCount)
                .ThenByDescending(entry => entry.StartDate)
                .ToList();

            foreach (var entry in reportData)
            {
                Console.WriteLine($"Course name: {entry.CourseName}");
                Console.WriteLine($"Resources count: {entry.ResourcesCount}");
            }
        }

        private static void Task02_PrintCoursesResources(StudentSystemDbContext db)
        {
            var reportData = db
                .Courses
                .OrderBy(c => c.StartDate)
                .ThenByDescending(c => c.EndDate)
                .Select(c => new
                {
                    CourseName = c.Name,
                    CourseDesciption = c.Description,
                    Resources = c.Resources.ToArray()
                })
                .ToList();

            foreach (var entry in reportData)
            {
                Console.WriteLine($"Course name: {entry.CourseName}");
                Console.WriteLine($"Course desciption: {entry.CourseDesciption}");
                Console.WriteLine("## Resources:");

                if (entry.Resources.Count() == 0)
                {
                    Console.WriteLine("N/A");
                }
                else
                {
                    foreach (var resource in entry.Resources)
                    {
                        Console.WriteLine($"- id: {resource.Id}");
                        Console.WriteLine($"- name: {resource.Name}");
                        Console.WriteLine($"- type: {resource.Type}");
                        Console.WriteLine($"- url: {resource.Url}");
                    }
                }
            }
        }

        private static void Task01_PrintStudentHomeworks(StudentSystemDbContext db)
        {
            var reportData = db
                .Students
                .Select(s => new
                {
                    StudentName = s.Name,
                    Homeworks = s.Homeworks.Select(h => new { ContentType = h.Type, Content = h.Content }).ToArray()
                })
                .OrderBy(entry => entry.StudentName)
                .ToList();

            foreach (var entry in reportData)
            {
                Console.WriteLine($"Student: {entry.StudentName}");
                Console.WriteLine("## Homeworks:");

                if (entry.Homeworks.Count() == 0)
                {
                    Console.WriteLine("N/A");
                }
                else
                {
                    foreach (var homework in entry.Homeworks)
                    {
                        Console.WriteLine($"- type: {homework.ContentType}");
                        Console.WriteLine($"- content: {homework.Content}");
                    }
                }
            }
        }

        private static void DeletePreviosData(StudentSystemDbContext db)
        {
            //db.Database.ExecuteSqlCommand("USE master; DROP DATABASE StudentSystem;");
            db.Database.EnsureDeleted();

            //foreach (var entity in db.Model.GetEntityTypes())
            //{
            //    string tableName = "dbo." + entity.Relational().TableName;
            //    var projectParam = new SqlParameter("@tableName", System.Data.SqlDbType.NVarChar);
            //    projectParam.Value = tableName;
            //    db.Database.ExecuteSqlCommand("USE StudentSystem; TRUNCATE TABLE @tableName;", new object[] { projectParam });
            //}
        }

        private static void SeedStudentAndCourses(StudentSystemDbContext db)
        {
            int totalCoursesCount = 25;
            var coursesAdded = new List<Course>();
            for (int i = 0; i < totalCoursesCount; i++)
            {
                coursesAdded.Add(new Course()
                {
                    Name = "Course " + i,
                    Price = 100 + i,
                    StartDate = DateTime.Now.AddDays(10 + i * 3),
                    EndDate = DateTime.Now.AddDays(100 + 2 * i)
                });
            }
            db.Courses.AddRange(coursesAdded);

            int totalStudentsCount = 100;
            var studentsAdded = new List<Student>();
            for (int i = 0; i < totalStudentsCount; i++)
            {
                studentsAdded.Add(new Student()
                {
                    Name = "Student " + i,
                    Birthday = DateTime.Now.AddYears(-20 - i).AddDays(13 + 6 * i),
                    RegistrationDate = DateTime.Now.AddDays(i)
                });
            }
            db.Students.AddRange(studentsAdded);

            var homeworkTypes = new int[] { 0, 1, 2 };
            foreach (var course in coursesAdded)
            {
                for (int i = 0; i < 4; i++)
                {
                    var randomStudent = studentsAdded[random.Next(0, totalStudentsCount)];
                    if (course.Students.Any(s => s.Student.Name == randomStudent.Name))
                    {
                        i--;
                        continue;
                    }
                    randomStudent.Homeworks.Add(new Homework()
                    {
                        Course = course,
                        Student = randomStudent,
                        SubmissionDate = DateTime.Now.AddDays(3 + 7 * i),
                        Type = Enum.Parse<HomeworkType>(homeworkTypes[random.Next(0, maxValue: homeworkTypes.Length)].ToString()),
                        Content = "Content of Homework : " + i * 1005
                    });
                    course.Students.Add(new StudentCourse() { Course = course, Student = randomStudent });
                }

                var resourcesTypes = new int[] { 0, 1, 2, 999 };
                int resourceCount = random.Next(2, 11);
                for (int i = 0; i < resourceCount; i++)
                {
                    course.Resources.Add(new Resource()
                    {
                        Course = course,
                        Name = "Resource_" + i + "_" + course.Name,
                        Type = Enum.Parse<ResourceType>(resourcesTypes[random.Next(0, maxValue: resourcesTypes.Length)].ToString()),
                        Url = "www.resources.get/" + 3 * i + 8
                    });
                }
            }

            foreach (var course in coursesAdded)
            {
                for (int i = 0; i < 4; i++)
                {
                    var randomStudent = studentsAdded[random.Next(0, totalStudentsCount)];
                    if (course.Students.Any(s => s.Student.Name == randomStudent.Name))
                    {
                        i--;
                        continue;
                    }
                    course.Students.Add(new StudentCourse() { Course = course, Student = randomStudent });
                }
            }

            db.SaveChanges();
        }

        private static void SeedLicenses(StudentSystemDbContext db)
        {
            int totalResourcesCount = 68;
            var resources = db.Resources.ToList();
            var licensesAdded = new List<License>();
            for (int i = 0; i < totalResourcesCount; i++)
            {
                int index = random.Next(0, resources.Count);
                if (resources[index].Licenses.Count != 0)
                {
                    i--;
                    continue;
                }
                int licensesCounter = random.Next(1, 5);
                for (int j = 0; j < licensesCounter; j++)
                {
                    resources[index].Licenses.Add(new License()
                    {
                        Name = "License#_0" + j * 6,
                        Resource = resources[index]
                    });
                }
            }

            db.Licenses.AddRange(licensesAdded);
            db.SaveChanges();
        }
    }
}