//Zhttps://github.com/lub1veee/pomogite_423.git

using System.Runtime.CompilerServices;
using System;
using System.Linq;
/*internal class Dog
{
    public void Move()
    {
        Console.WriteLine("Вариант 1");
    }
    public void Move(bool v)
    {
        Console.WriteLine("Вариант 2");
    }
    public void Move(int a)
    {
        Console.WriteLine("Вариант 3");
    }
}
class Program
{
    void Main(string[] args)
    {
        private Dog dog = new Dog();
        dog.Move();
        dog.Move(false);
        dog.Move(5);
    }
}
*/

namespace UniversityManagementSystem
{
    public abstract class Person
    {
        private string name;
        private int age;
        private string contactInfo;

        public string Name
        {
            get => name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Имя не может быть пустым");
                name = value;
            }
        }

        public int Age
        {
            get => age;
            set
            {
                if (value < 16 || value > 100)
                    throw new ArgumentException("Возраст должен быть от 16 до 100 лет");
                age = value;
            }
        }

        public string ContactInfo
        {
            get => contactInfo;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Контактная информация не может быть пустой");
                contactInfo = value;
            }
        }

        protected Person(string name, int age, string contactInfo)
        {
            Name = name;
            Age = age;
            ContactInfo = contactInfo;
        }

        public virtual string GetInfo()
        {
            return $"Имя: {Name}, Возраст: {Age}, Контакты: {ContactInfo}";
        }

        public abstract string GetRole();
    }
    public interface IIdentifiable
    {
        int Id { get; }
    }

    public class Student : Person, IIdentifiable
    {
        private static int nextId = 1;

        public int Id { get; }
        private List<Course> courses;

        public IReadOnlyList<Course> Courses => courses.AsReadOnly();
        public string StudentId { get; }

        public Student(string name, int age, string contactInfo, string studentId)
            : base(name, age, contactInfo)
        {
            Id = nextId++;
            StudentId = studentId;
            courses = new List<Course>();
        }

        public void EnrollInCourse(Course course)
        {
            if (course == null)
                throw new ArgumentNullException(nameof(course));

            if (!courses.Contains(course))
            {
                courses.Add(course);
                course.AddStudent(this);
            }
        }

        public void DropCourse(Course course)
        {
            if (course != null && courses.Contains(course))
            {
                courses.Remove(course);
                course.RemoveStudent(this);
            }
        }

        public override string GetInfo()
        {
            return $"{base.GetInfo()}, ID студента: {StudentId}";
        }

        public override string GetRole()
        {
            return "Студент";
        }

        public string GetCoursesInfo()
        {
            if (courses.Count == 0)
                return "Студент не записан на курсы";

            return string.Join("\n", courses.Select(c => $" - {c.Name}"));
        }
    }