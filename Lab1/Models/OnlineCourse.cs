namespace CourseManagement.Models;

public class OnlineCourse : Course
{
    public string Platform { get; set; }

    public OnlineCourse(string id, string name, int maxStudents, string platform)
        : base(id, name, maxStudents)
    {
        Platform = platform;
    }

    public override string GetCourseInfo()
    {
        string teacherName = Teacher != null ? Teacher.Name : "Не назначен";
        return
            $"Курс: {Name}, Платформа: {Platform}, Преподаватель: {teacherName}, Студентов: {Students.Count}/{MaxStudents}";
    }
}
