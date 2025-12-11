namespace CourseManagement.Models;

public class OfflineCourse : Course
{
    public string Classroom { get; set; }

    public OfflineCourse(string id, string name, int maxStudents, string classroom)
        : base(id, name, maxStudents)
    {
        Classroom = classroom;
    }

    public override string GetCourseInfo()
    {
        string teacherName = Teacher != null ? Teacher.Name : "Не назначен";
        return
            $"Курс: {Name}, Аудитория: {Classroom}, Преподаватель: {teacherName}, Студентов: {Students.Count}/{MaxStudents}";
    }
}
