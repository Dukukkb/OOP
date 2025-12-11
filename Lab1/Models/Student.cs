namespace CourseManagement.Models;

public class Student
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }

    public Student(string id, string name, string email)
    {
        Id = id;
        Name = name;
        Email = email;
    }
}

