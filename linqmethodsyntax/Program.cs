using System;
using System.Collections.Generic;
using System.Linq;

public class Student
{
    public int StudentID { get; set; }
    public string StudentName { get; set; }
    public int Age { get; set; }
    public string Major { get; set; }
    public double Tuition { get; set; }
}
public class StudentClubs
{
    public int StudentID { get; set; }
    public string ClubName { get; set; }
}
public class StudentGPA
{
    public int StudentID { get; set; }
    public double GPA { get; set; }
}

class Program
{
    static void Main(string[] args)
    {
        IList<Student> studentList = new List<Student>() {
            new Student() { StudentID = 1, StudentName = "Frank Furter", Age = 55, Major = "Hospitality", Tuition = 3500.00 },
            new Student() { StudentID = 2, StudentName = "Gina Host", Age = 21, Major = "Hospitality", Tuition = 4500.00 },
            new Student() { StudentID = 3, StudentName = "Cookie Crumb", Age = 21, Major = "CIT", Tuition = 2500.00 },
            new Student() { StudentID = 4, StudentName = "Ima Script", Age = 48, Major = "CIT", Tuition = 5500.00 },
            new Student() { StudentID = 5, StudentName = "Cora Coder", Age = 35, Major = "CIT", Tuition = 1500.00 },
            new Student() { StudentID = 6, StudentName = "Ura Goodchild", Age = 40, Major = "Marketing", Tuition = 500.00 },
            new Student() { StudentID = 7, StudentName = "Take Mewith", Age = 29, Major = "Aerospace Engineering", Tuition = 5500.00 }
        };
        IList<StudentGPA> studentGPAList = new List<StudentGPA>() {
            new StudentGPA() { StudentID = 1, GPA = 4.0 },
            new StudentGPA() { StudentID = 2, GPA = 3.5 },
            new StudentGPA() { StudentID = 3, GPA = 2.0 },
            new StudentGPA() { StudentID = 4, GPA = 1.5 },
            new StudentGPA() { StudentID = 5, GPA = 4.0 },
            new StudentGPA() { StudentID = 6, GPA = 2.5 },
            new StudentGPA() { StudentID = 7, GPA = 1.0 }
        };
        IList<StudentClubs> studentClubList = new List<StudentClubs>() {
            new StudentClubs() {StudentID=1, ClubName="Photography" },
            new StudentClubs() {StudentID=1, ClubName="Game" },
            new StudentClubs() {StudentID=2, ClubName="Game" },
            new StudentClubs() {StudentID=5, ClubName="Photography" },
            new StudentClubs() {StudentID=6, ClubName="Game" },
            new StudentClubs() {StudentID=7, ClubName="Photography" },
            new StudentClubs() {StudentID=3, ClubName="PTK" },
        };

        var groupByGPA = studentGPAList.GroupBy(s => s.GPA);
        Console.WriteLine("Students Grouped by GPA:");
        foreach (var group in groupByGPA)
        {
            Console.WriteLine($"GPA: {group.Key}");
            foreach (var student in group)
            {
                Console.WriteLine($"Student ID: {student.StudentID}");
            }
        }

        var sortByClub = studentClubList.OrderBy(s => s.ClubName).GroupBy(s => s.ClubName);
        Console.WriteLine("\nStudents Sorted and Grouped by Club:");
        foreach (var group in sortByClub)
        {
            Console.WriteLine($"Club: {group.Key}");
            foreach (var student in group)
            {
                Console.WriteLine($"Student ID: {student.StudentID}");
            }
        }

        var countGPA = studentGPAList.Count(s => s.GPA >= 2.5 && s.GPA <= 4.0);
        Console.WriteLine($"\nNumber of Students with GPA between 2.5 and 4.0: {countGPA}");

        var averageTuition = studentList.Average(s => s.Tuition);
        Console.WriteLine($"\nAverage Tuition: {averageTuition}");

        var maxTuition = studentList.Max(s => s.Tuition);
        var highestPayingStudent = studentList.FirstOrDefault(s => s.Tuition == maxTuition);
        if (highestPayingStudent != null)
        {
            Console.WriteLine($"\nStudent Paying the Most Tuition:");
            Console.WriteLine($"Name: {highestPayingStudent.StudentName}");
            Console.WriteLine($"Major: {highestPayingStudent.Major}");
            Console.WriteLine($"Tuition: {highestPayingStudent.Tuition}");
        }

        var joinedData = studentList.Join(studentGPAList,
                                           s => s.StudentID,
                                           g => g.StudentID,
                                           (s, g) => new { s.StudentName, s.Major, g.GPA });
        Console.WriteLine("\nJoined Student List and GPA List:");
        foreach (var data in joinedData)
        {
            Console.WriteLine($"Name: {data.StudentName}, Major: {data.Major}, GPA: {data.GPA}");
        }

        var gameClubMembers = studentList.Join(studentClubList.Where(s => s.ClubName == "Game"),
                                               s => s.StudentID,
                                               c => c.StudentID,
                                               (s, c) => s.StudentName);
        Console.WriteLine("\nStudents in the Game Club:");
        foreach (var student in gameClubMembers)
        {
            Console.WriteLine(student);
        }
    }
}

