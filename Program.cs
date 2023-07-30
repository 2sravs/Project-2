using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.AccessControl;

namespace ConAppProject2
{

   public class Teacher
    {
        public string Name { get; set; }
        public string Subject { get; set; }
        public string Class { get; set; }
       
        public string Section { get; set; }

        public Teacher(string name, string subject,string number, string section) 
        { 
           Name = name;
           Subject = subject;
            Class = number;
           Section = section;
        }

        public override string ToString()
        {
            return $"Name: {Name}, Subject: {Subject},Class:{Class},Section:{Section}";
        }
    }

    internal class Program
    {
        static List<Teacher> teachers = new List<Teacher>();
        static string dataFilePath = "teachers.txt";

        

        static void Main()
        {
            LoadDataFromFile();

            while (true)
            {
                Console.WriteLine("1. Add Teacher");
                Console.WriteLine("2. Update Teacher");
                Console.WriteLine("3. Delete Teachers");
                Console.WriteLine("4. Save and Exit");
                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddTeacher();
                        break;
                    case "2":
                        UpdateTeacher();
                        break;
                    case "3":
                        deleteTeachers();
                        break;
                   

                    case "4":
                        SaveDataToFile();
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Try again.");
                        break;
                }
            }
        }

        private static void deleteTeachers()
        {
            throw new NotImplementedException();
        }

        static void AddTeacher()
        {
            Console.Write("Enter teacher's name: ");
            string name = Console.ReadLine();

            Console.Write("Enter subject: ");
            string subject = Console.ReadLine();

            Console.Write("Enter class: ");
            string number = Console.ReadLine();

            Console.Write("Enter Section: ");
            string section = Console.ReadLine();

            Teacher newTeacher = new Teacher(name, subject, number, section);
            teachers.Add(newTeacher);
            Console.WriteLine("Teacher added successfully!\n");
        }

        static void UpdateTeacher()
        {
            Console.Write("Enter teacher's name to update: ");
            string nameToUpdate = Console.ReadLine();

            Teacher teacherToUpdate = teachers.Find(t => t.Name.Equals(nameToUpdate, StringComparison.OrdinalIgnoreCase));
            if (teacherToUpdate != null)
            {
                Console.Write("Enter new subject: ");
                string newSubject = Console.ReadLine();

                Console.Write("Enter new Class: ");
                string newNumber = Console.ReadLine();

                Console.Write("Enter new Section: ");
                string newSection = Console.ReadLine();

                teacherToUpdate.Subject = newSubject;
                teacherToUpdate.Subject = newNumber;
                teacherToUpdate.Section = newSection;


                Console.WriteLine("Teacher updated successfully!\n");
            }
            else
            {
                Console.WriteLine($"Teacher with name '{nameToUpdate}' not found.\n");
            }
        }

        static void ViewTeachers()
        {
            if (teachers.Count == 0)
            {
                Console.WriteLine("No teachers found.\n");
            }
            else
            {
                foreach (Teacher teacher in teachers)
                {
                    Console.WriteLine(teacher);
                }
                Console.WriteLine();
            }
        }

        static void SaveDataToFile()
        {
            using (StreamWriter writer = new StreamWriter(dataFilePath))
            {
                foreach (Teacher teacher in teachers)
                {
                    writer.WriteLine($"{teacher.Name},{teacher.Subject},{teacher.Class}{teacher.Section}");
                }
            }
            Console.WriteLine("Data saved to file.\n");
        }

        static void LoadDataFromFile()
        {
            if (File.Exists(dataFilePath))
            {
                teachers.Clear();
                using (StreamReader reader = new StreamReader(dataFilePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] data = line.Split(',');
                        if (data.Length == 4)
                        {
                            string name = data[0].Trim();
                            string subject = data[1].Trim();
                            string number = data[2].Trim();
                            string section = data[3].Trim();
                            teachers.Add(new Teacher(name, subject,number,section));
                        }
                    }
                }
                Console.WriteLine("Data loaded from file.\n");
            }
            else
            {
                Console.WriteLine("No existing data file found. Starting with an empty list.\n");
            }
            Console.ReadKey();
        }
    }
}

