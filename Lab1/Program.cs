using System;
using System.Collections.Generic;
using CourseManagement.Models;
using CourseManagement.Services;

namespace CourseManagement;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Система управления курсами\n");


        var manager = new CourseManager();


        var teacher1 = new Teacher("K4110с", " Кочубеев Николай Сергеевич", "Объекто ориентированное программирование");
        var teacher2 = new Teacher("K4112с", "Слюсаренко Сергей Владиморвич", "Объекто ориентированное программирование");
        manager.AddTeacher(teacher1);
        manager.AddTeacher(teacher2);

        var onlineCourse = new OnlineCourse(
            id: null,
            name: "Объектно ориентированное программирование",
            maxStudents: 25,
            platform: "Zoom"
        );

        var offlineCourse = new OfflineCourse(
            id: null,
            name: "Объектно ориентированное программирование",
            maxStudents: 20,
            classroom: "Аудитория 305"
        );

        manager.AddCourse(onlineCourse);
        manager.AddCourse(offlineCourse);

        manager.AssignTeacher(onlineCourse.Id, teacher1.Id);
        manager.AssignTeacher(offlineCourse.Id, teacher2.Id);

        var student1 = new Student("K3239", " Бандурин Егор Сергеевич", "egor@mail.ru");
        var student2 = new Student("K3242", "Веселков Матвей Евгеньевич", "matvey@mail.ru");

        manager.EnrollStudent(onlineCourse.Id, student1);
        manager.EnrollStudent(onlineCourse.Id, student2);
        manager.EnrollStudent(offlineCourse.Id, student1);

        Console.WriteLine("Все курсы:");
        foreach (var course in manager.GetAllCourses())
            Console.WriteLine("  " + course.GetCourseInfo());

        Console.WriteLine("\nВсе студенты:");
        var printedStudentIds = new HashSet<string>();
        foreach (var course in manager.GetAllCourses())
        {
            foreach (var student in course.Students)
            {
                if (printedStudentIds.Add(student.Id))
                    Console.WriteLine($"  {student.Name} ({student.Id}), {student.Email}");
            }
        }

        Console.WriteLine($"\nКурсы преподавателя {teacher1.Name}:");
        foreach (var course in manager.GetCoursesByTeacher(teacher1.Id))
            Console.WriteLine("  " + course.Name);

        Console.WriteLine("\nНажмите Enter...");
        Console.ReadLine();
    }
}
