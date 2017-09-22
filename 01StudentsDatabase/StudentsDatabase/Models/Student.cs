using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StudentsDatabase.Models
{
    public class Student
    {
        public Student()
        {
            this.Courses = new List<StudentCourse>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public ICollection<StudentCourse> Courses { get; set; }
    }
}
