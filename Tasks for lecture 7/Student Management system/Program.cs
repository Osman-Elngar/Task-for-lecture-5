using System;
using System.Collections.Generic;
using System.Linq;

#region Student Class
public class Student
{
    public string Name { get; set; }
    public int ID { get; set; }
    public List<string> Courses { get; set; }

    // Constructor
    public Student(string name, int id)
    {
        Name = name;
        ID = id;
        Courses = new List<string>();
    }

    // Copy Constructor (Courses list is empty)
    public Student(Student other)
    {
        Name = other.Name;
        ID = other.ID;
        Courses = new List<string>();
    }

    public void EnrollCourse(string courseName)
    {
        if (!Courses.Contains(courseName))
        {
            Courses.Add(courseName);
        }
    }

    public void DisplayDetails()
    {
        Console.WriteLine($"Student Name : {Name}");
        Console.WriteLine($"Student ID   : {ID}");
        Console.WriteLine("Courses:");

        if (Courses.Count == 0)
            Console.WriteLine("  None");
        else
            foreach (string course in Courses)
                Console.WriteLine($"  - {course}");

        Console.WriteLine("--------------------------------");
    }
}
#endregion

#region Course Class
public class Course
{
    public string Name { get; set; }
    public string Instructor { get; set; }

    public Course(string name, string instructor)
    {
        Name = name;
        Instructor = instructor;
    }

    public void DisplayDetails()
    {
        Console.WriteLine($"Course Name : {Name}");
        Console.WriteLine($"Instructor  : {Instructor}");
        Console.WriteLine("--------------------------------");
    }
}
#endregion

#region School Class (With Indexer - Bonus)
public class School
{
    private List<Student> students = new List<Student>();
    private List<Course> courses = new List<Course>();

    // Indexer (Bonus)
    public Student this[int studentID]
    {
        get
        {
            return students.FirstOrDefault(s => s.ID == studentID);
        }
    }

    public void AddStudent(Student student)
    {
        if (students.Any(s => s.ID == student.ID))
        {
            Console.WriteLine("❌ Error: Student ID already exists.");
            return;
        }

        students.Add(student);
    }

    public void AddCourse(Course course)
    {
        courses.Add(course);
    }

    public void EnrollStudentInCourse(int studentID, string courseName)
    {
        Student student = this[studentID];
        Course course = courses.FirstOrDefault(c => c.Name == courseName);

        if (student == null)
        {
            Console.WriteLine("❌ Error: Student not found.");
            return;
        }

        if (course == null)
        {
            Console.WriteLine("❌ Error: Course not found.");
            return;
        }

        student.EnrollCourse(courseName);
    }

    public void DisplayAllStudents()
    {
        Console.WriteLine("\n===== All Students =====");
        foreach (Student student in students)
        {
            student.DisplayDetails();
        }
    }

    public void DisplayAllCourses()
    {
        Console.WriteLine("\n===== All Courses =====");
        foreach (Course course in courses)
        {
            course.DisplayDetails();
        }
    }
}
#endregion

#region Main Program
class Program
{
    static void Main()
    {
        School school = new School();

        // Add Courses
        school.AddCourse(new Course("C#", "Dr. Ahmed"));
        school.AddCourse(new Course("OOP", "Dr. Ali"));
        school.AddCourse(new Course("Database", "Dr. Mona"));

        // Add Students
        Student s1 = new Student("Osman", 1);
        Student s2 = new Student("Mohamed", 2);

        school.AddStudent(s1);
        school.AddStudent(s2);

        // Enroll Students in Courses
        school.EnrollStudentInCourse(1, "C#");
        school.EnrollStudentInCourse(1, "OOP");
        school.EnrollStudentInCourse(2, "Database");

        // Display All Data
        school.DisplayAllStudents();
        school.DisplayAllCourses();

        // Copy Constructor Test
        Student copiedStudent = new Student(s1);
        Console.WriteLine("\n===== Copied Student (Empty Courses) =====");
        copiedStudent.DisplayDetails();

        Console.WriteLine("Press Enter to exit...");
        Console.ReadLine();
    }
}
#endregion
