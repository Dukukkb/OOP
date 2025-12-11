using System;
using Xunit;
using CourseManagement.Models;
using CourseManagement.Services;

namespace Lab1.Tests
{
    public class CourseManagerTests
    {
        [Fact]
        public void AddCourse_ShouldAddCourseToList()
        {
            var manager = new CourseManager();
            var course = new OnlineCourse("C001", "Тест", 30, "Zoom");

            manager.AddCourse(course);

            Assert.Single(manager.GetAllCourses());
            Assert.Equal("C001", manager.GetAllCourses()[0].Id);
        }

        [Fact]
        public void RemoveCourse_ShouldRemoveCourseFromList()
        {
            var manager = new CourseManager();
            var course = new OnlineCourse("C001", "Тест", 30, "Zoom");
            manager.AddCourse(course);

            manager.RemoveCourse("C001");

            Assert.Empty(manager.GetAllCourses());
        }

        [Fact]
        public void AssignTeacher_ShouldAssignTeacherToCourse()
        {
            var manager = new CourseManager();
            var course = new OnlineCourse("C001", "Тест", 30, "Zoom");
            var teacher = new Teacher("T001", "Иванов", "IT");
            manager.AddCourse(course);
            manager.AddTeacher(teacher);

            manager.AssignTeacher("C001", "T001");

            Assert.NotNull(manager.GetCourse("C001").Teacher);
            Assert.Equal("T001", manager.GetCourse("C001").Teacher.Id);
        }

        [Fact]
        public void EnrollStudent_ShouldAddStudentToCourse()
        {
            var manager = new CourseManager();
            var course = new OnlineCourse("C001", "Тест", 30, "Zoom");
            var student = new Student("S001", "Петров", "petrov@mail.ru");
            manager.AddCourse(course);

            manager.EnrollStudent("C001", student);

            Assert.Equal(1, manager.GetCourse("C001").Students.Count);
        }

        [Fact]
        public void EnrollStudent_WhenCourseFull_ShouldThrowException()
        {
            var manager = new CourseManager();
            var course = new OnlineCourse("C001", "Тест", 1, "Zoom"); // Макс 1 студент
            var student1 = new Student("S001", "Петров", "petrov@mail.ru");
            var student2 = new Student("S002", "Сидоров", "sidorov@mail.ru");
            manager.AddCourse(course);
            manager.EnrollStudent("C001", student1);

            Assert.Throws<InvalidOperationException>(() => 
                manager.EnrollStudent("C001", student2));
        }

        [Fact]
        public void GetCoursesByTeacher_ShouldReturnOnlyTeacherCourses()
        {
            var manager = new CourseManager();
            var course1 = new OnlineCourse("C001", "Курс 1", 30, "Zoom");
            var course2 = new OnlineCourse("C002", "Курс 2", 30, "Teams");
            var teacher1 = new Teacher("T001", "Иванов", "IT");
            var teacher2 = new Teacher("T002", "Петров", "Math");
            
            manager.AddCourse(course1);
            manager.AddCourse(course2);
            manager.AddTeacher(teacher1);
            manager.AddTeacher(teacher2);
            manager.AssignTeacher("C001", "T001");
            manager.AssignTeacher("C002", "T002");

            var result = manager.GetCoursesByTeacher("T001");

            Assert.Single(result);
            Assert.Equal("C001", result[0].Id);
        }

        [Fact]
        public void GetCourseInfo_OnlineAndOffline_ShouldReturnDifferentInfo()
        {
            var online = new OnlineCourse("C001", "Онлайн", 30, "Zoom");
            var offline = new OfflineCourse("C002", "Офлайн", 30, "305");

            var onlineInfo = online.GetCourseInfo();
            var offlineInfo = offline.GetCourseInfo();

            Assert.Contains("Платформа", onlineInfo);
            Assert.Contains("Аудитория", offlineInfo);
        }
    }
}
