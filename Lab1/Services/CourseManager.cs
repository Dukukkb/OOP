using System;
using System.Collections.Generic;
using System.Linq;
using CourseManagement.Models;

namespace CourseManagement.Services;

public class CourseManager
{
    private List<Course> _courses = new();
    private List<Teacher> _teachers = new();

    public CourseManager()
    {
    }

    public void AddCourse(Course course)
    {
        if (course == null)
            throw new ArgumentNullException(nameof(course), "Курс не может быть null");

        if (_courses.Any(c => c.Id == course.Id))
            throw new InvalidOperationException("Курс с таким Id уже существует");

        _courses.Add(course);
    }

    public void RemoveCourse(string courseId)
    {
        Course course = _courses.FirstOrDefault(course => course.Id == courseId);
        if (course != null)
            _courses.Remove(course);
    }

    public Course GetCourse(string courseId)
    {
        return _courses.FirstOrDefault(course => course.Id == courseId);
    }

    public List<Course> GetAllCourses()
    {
        return new List<Course>(_courses);
    }

    public void AddTeacher(Teacher teacher)
    {
        if (teacher == null)
            throw new ArgumentNullException(nameof(teacher), "Преподаватель не может быть null");

        _teachers.Add(teacher);
    }

    public Teacher GetTeacher(string teacherId)
    {
        return _teachers.FirstOrDefault(t => t.Id == teacherId);
    }

    public void AssignTeacher(string courseId, string teacherId)
    {
        Course course = GetCourse(courseId);
        Teacher teacher = GetTeacher(teacherId);

        if (course == null)
            throw new InvalidOperationException("Курс не найден");
        if (teacher == null)
            throw new InvalidOperationException("Преподаватель не найден");

        course.Teacher = teacher;
    }

    public List<Course> GetCoursesByTeacher(string teacherId)
    {
        return _courses
            .Where(course => course.Teacher != null && course.Teacher.Id == teacherId)
            .ToList();
    }

    public void EnrollStudent(string courseId, Student student)
    {
        Course course = GetCourse(courseId);

        if (course == null)
            throw new InvalidOperationException("Курс не найден");

        course.EnrollStudent(student);
    }
}
