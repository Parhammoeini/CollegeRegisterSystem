using System;

namespace assignment2
{
    internal class Program
    {
        static Student[] students = new Student[100];
        static int studentCount = 0;

        static Professor[] professors = new Professor[100];
        static int professorCount = 0;

        static Student[,] classEnrollment = new Student[10, 10];
        static int[] classEnrollmentCount = new int[10];

        static int GenerateRandom5DigitNumber()
        {
            Random random = new Random();
            return random.Next(10000, 100000);
        }

        class Person
        {
            public int Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }

            public Person(int id, string firstName, string lastName)
            {
                Id = id;
                FirstName = firstName;
                LastName = lastName;
            }

            public virtual void DisplayInfo()
            {
                Console.WriteLine($"ID: {Id}, Name: {FirstName} {LastName}");
            }
        }

        class Student : Person
        {
            public int ClassId { get; set; }

            public Student(int id, string firstName, string lastName, int classId) : base(id, firstName, lastName)
            {
                ClassId = classId;
            }

            public void EnrollInClass(int classId)
            {
                if (classEnrollmentCount[classId] < 10)
                {
                    classEnrollment[classId, classEnrollmentCount[classId]] = this;
                    classEnrollmentCount[classId]++;
                    Console.WriteLine($"Student {FirstName} {LastName} enrolled in Class {classId + 1}");
                }
                else
                {
                    Console.WriteLine($"Class {classId + 1} is full. Cannot enroll {FirstName} {LastName}");
                }
            }

            public override void DisplayInfo()
            {
                base.DisplayInfo();
                Console.WriteLine($"Class ID: {ClassId}");
            }
        }

        class Professor : Person
        {
            public int ClassId { get; set; }

            public Professor(int id, string firstName, string lastName, int classId) : base(id, firstName, lastName)
            {
                ClassId = classId;
            }

            public override void DisplayInfo()
            {
                base.DisplayInfo();
                Console.WriteLine($"Teaching Class ID: {ClassId}");
            }
        }

        static public void DisplayMenu()
        {
            while (true)
            {
                Console.WriteLine("1) Add New Student\n2) Add New Professor\n3) View All Students\n" +
                                  "4) View All Professors\n5) Enroll a Student in a Class\n" +
                                  "6) View Students in a Class\n7) Exit The Program\n\nPlease select an option:");

                int option = Convert.ToInt32(Console.ReadLine());

                if (option > 7 || option < 1)
                {
                    Console.WriteLine("Please choose between 1 to 7\n\n-----------------------------------");
                    continue;
                }

                switch (option)
                {
                    case 1:
                        AddStudent();
                        break;
                    case 2:
                        AddProfessor();
                        break;
                    case 3:
                        ShowStudents();
                        break;
                    case 4:
                        ShowProfessors();
                        break;
                    case 5:
                        EnrollStudent();
                        break;
                    case 6:
                        ViewStudentsInClass();
                        break;
                    case 7:
                        return;
                }
            }
        }

        static public void AddStudent()
        {
            int id_random = GenerateRandom5DigitNumber();
            Console.WriteLine("Enter Your First Name:");
            string fname = Console.ReadLine();
            Console.WriteLine("Enter Your Last Name:");
            string lname = Console.ReadLine();
            Console.WriteLine("Enter Your class number: ");
            int c = Convert.ToInt32(Console.ReadLine());

            Student newStudent = new Student(id_random, fname, lname, c);
            students[studentCount] = newStudent;
            studentCount++;

            Console.WriteLine($"Your Student id is : {id_random}");
            Console.WriteLine("If You want to go back to menu please press 0 and If you want to add another student press 1");
            int a = Convert.ToInt32(Console.ReadLine());

            if (a == 0)
                DisplayMenu();
            else if (a == 1)
                AddStudent();
        }

        static public void AddProfessor()
        {
            int id_random = GenerateRandom5DigitNumber();
            Console.WriteLine("Enter Professor's First Name:");
            string fname = Console.ReadLine();
            Console.WriteLine("Enter Professor's Last Name:");
            string lname = Console.ReadLine();
            Console.WriteLine("Enter Professor's class number: ");
            int c = Convert.ToInt32(Console.ReadLine());

            Professor newProfessor = new Professor(id_random, fname, lname, c);
            professors[professorCount] = newProfessor;
            professorCount++;

            Console.WriteLine($"Professor's ID is : {id_random}");
            Console.WriteLine("If you want to go back to menu please press 0 and If you want to add another professor press 1");
            int a = Convert.ToInt32(Console.ReadLine());

            if (a == 0)
                DisplayMenu();
            else if (a == 1)
                AddProfessor();
        }

        static public void ShowStudents()
        {
            foreach (var student in students)
            {
                if (student != null)
                {
                    student.DisplayInfo();
                }
            }
        }

        static public void ShowProfessors()
        {
            foreach (var professor in professors)
            {
                if (professor != null)
                {
                    professor.DisplayInfo();
                }
            }
        }

        static public void EnrollStudent()
        {
            Console.WriteLine("Enter Student ID to enroll:");
            int id = Convert.ToInt32(Console.ReadLine());

            Student studentToEnroll = null;
            foreach (var student in students)
            {
                if (student != null && student.Id == id)
                {
                    studentToEnroll = student;
                    break;
                }
            }

            if (studentToEnroll != null)
            {
                Console.WriteLine("Enter Class ID to enroll:");
                int classId = Convert.ToInt32(Console.ReadLine()) - 1;

                studentToEnroll.EnrollInClass(classId);
            }
            else
            {
                Console.WriteLine("Student not found.");
            }
        }

        static public void ViewStudentsInClass()
        {
            Console.WriteLine("Enter Class ID to view enrolled students:");
            int classId = Convert.ToInt32(Console.ReadLine()) - 1;

            Console.WriteLine($"Students enrolled in Class {classId + 1}:");
            for (int i = 0; i < classEnrollmentCount[classId]; i++)
            {
                if (classEnrollment[classId, i] != null)
                {
                    classEnrollment[classId, i].DisplayInfo();
                }
            }
        }

        static void Main(string[] args)
        {
            DisplayMenu();
        }
    }
}

