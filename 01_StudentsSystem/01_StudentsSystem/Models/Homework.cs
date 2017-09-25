using StudentsSystem.Enums;
using System;

namespace StudentsSystem.Models
{
    public class Homework
    {
        public int Id { get; set; }

        //public byte[] Content { get; set; }
        public string Content { get; set; }

        public DateTime SubmissionDate { get; set; }

        public HomeworkType Type { get; set; }

        public int StudentId { get; set; }
        public Student Student { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}