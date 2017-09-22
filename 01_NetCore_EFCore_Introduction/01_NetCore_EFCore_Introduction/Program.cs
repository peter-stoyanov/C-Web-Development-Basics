using System;
using System.Collections.Generic;
using System.Linq;

namespace NetCore_EFCore_Introduction
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var studentMarks = new Dictionary<string, int>();
            var studentSubjects = new Dictionary<string, HashSet<string>>();

            while (true)
            {
                var input = Console.ReadLine();

                if (input == "END") { break; }

                var tokens = input.Split().ToArray();

                string name = tokens[0];
                string subject = tokens[1];
                int mark = int.Parse(tokens[2]);

                ReadMarks(studentMarks, name, mark);
                ReadSubjects(studentSubjects, name, subject);

                var orderedStudents = studentMarks
                    .OrderByDescending(kvp => kvp.Value)
                    .ThenBy(kvp => kvp.Key)
                    .Select(kvp => new
                        {
                            Name = kvp.Key,
                            Points = kvp.Value,
                            Subjects = String.Join(", ", studentSubjects[kvp.Key].OrderBy(s => s))
                        })
                    .ToList();

                foreach (var student in orderedStudents)
                {
                    Console.WriteLine($"{student.Name}: {student.Points} [{student.Subjects}]");
                }
            }
        }

        private static void ReadSubjects(Dictionary<string, HashSet<string>> studentSubjects, string name, string subject)
        {
            if (!studentSubjects.ContainsKey(name))
            {
                studentSubjects.Add(name, new HashSet<string>() { subject });
            }
            else
            {
                studentSubjects[name].Add(subject);
            }
        }

        private static void ReadMarks(Dictionary<string, int> studentMarks, string name, int mark)
        {
            if (!studentMarks.ContainsKey(name))
            {
                studentMarks.Add(name, mark);
            }
            else
            {
                studentMarks[name] += mark;
            }
        }
    }
}
