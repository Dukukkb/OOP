using System;
using System.Collections.Generic;
using System.Linq;

namespace CourseManagement.Models;

public abstract class Course
{
    private List<Student> _students = new List<Student>();

    public string Id { get; set; }
    public string Name { get; set; }
    public Teacher Teacher { get; set; }
    public int MaxStudents { get; set; }

    public IReadOnlyList<Student> Students => _students;

    protected Course(string id, string name, int maxStudents)
    {
        Id = string.IsNullOrEmpty(id) ? Guid.NewGuid().ToString() : id;
        Name = name;
        MaxStudents = maxStudents;
    }

    public void EnrollStudent(Student student)
    {
        if (student == null)
            throw new ArgumentNullException(nameof(student), "Студент не может быть null");

        if (_students.Count >= MaxStudents)
            throw new InvalidOperationException("Курс заполнен");

        if (_students.Any(s => s.Id == student.Id))
            throw new InvalidOperationException("Студент уже записан на курс");

        _students.Add(student);
    }

    public void RemoveStudent(string studentId)
    {
        int idx = _students.FindIndex(s => s.Id == studentId);
        if (idx >= 0)
            _students.RemoveAt(idx);
    }

    public abstract string GetCourseInfo();
}
