namespace CourseManagement.Models;

public class Teacher
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Department { get; set; }

    public Teacher(string id, string name, string department)
    {
        Id = id;
        Name = name;
        Department = department;
    }
}

