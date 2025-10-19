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
    public class Professor : Person, IIdentifiable
    {
        private static int nextId = 1;

        public int Id { get; }
        private List<Course> courses;

        public IReadOnlyList<Course> Courses => courses.AsReadOnly();
        public string Department { get; set; }
        public decimal Salary { get; set; }

        public Professor(string name, int age, string contactInfo, string department, decimal salary)
            : base(name, age, contactInfo)
        {
            Id = nextId++;
            Department = department;
            Salary = salary;
            courses = new List<Course>();
        }

        public void AssignToCourse(Course course)
        {
            if (course == null)
                throw new ArgumentNullException(nameof(course));

            if (!courses.Contains(course))
            {
                courses.Add(course);
                course.AssignProfessor(this);
            }
        }

        public override string GetInfo()
        {
            return $"{base.GetInfo()}, Кафедра: {Department}, Зарплата: {Salary:C}";
        }

        public override string GetRole()
        {
            return "Преподаватель";
        }

        public string GetTeachingCoursesInfo()
        {
            if (courses.Count == 0)
                return "Преподаватель не ведет курсы";

            return string.Join("\n", courses.Select(c => $" - {c.Name}"));
        }
    }
    public class Course : IIdentifiable
    {
        private static int nextId = 1;

        public int Id { get; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Credits { get; set; }

        private Professor professor;
        private List<Student> students;

        public Professor Professor
        {
            get => professor;
            set
            {
                professor = value;
                value?.AssignToCourse(this);
            }
        }

        public IReadOnlyList<Student> Students => students.AsReadOnly();

        public Course(string name, string description, int credits)
        {
            Id = nextId++;
            Name = name;
            Description = description;
            Credits = credits;
            students = new List<Student>();
        }

        public void AssignProfessor(Professor professor)
        {
            Professor = professor;
        }

        public void AddStudent(Student student)
        {
            if (student != null && !students.Contains(student))
            {
                students.Add(student);
            }
        }

        public void RemoveStudent(Student student)
        {
            if (student != null)
            {
                students.Remove(student);
            }
        }

        public string GetInfo()
        {
            string professorInfo = Professor != null ? Professor.Name : "Не назначен";
            return $"Курс: {Name}\nОписание: {Description}\nКредиты: {Credits}\nПреподаватель: {professorInfo}\nКоличество студентов: {students.Count}";
        }

        public string GetStudentsInfo()
        {
            if (students.Count == 0)
                return "На курс не записаны студенты";

            return string.Join("\n", students.Select(s => $" - {s.Name} (ID: {s.StudentId})"));
        }
    }
    // Основной класс системы управления университетом
    public class UniversityManagementSystem
    {
        // Инкапсуляция: приватные коллекции с публичными методами для управления
        private List<Student> students;
        private List<Professor> professors;
        private List<Course> courses;

        public UniversityManagementSystem()
        {
            students = new List<Student>();
            professors = new List<Professor>();
            courses = new List<Course>();
            InitializeSampleData();
        }

        private void InitializeSampleData()
        {
       
            var prof1 = new Professor("Мария Сокольническая", 45, "maria@university.ru", "Компьютерные науки", 80000);
            var prof2 = new Professor("Максим Олегович", 38, "maxon@university.ru", "Математика", 75000);

            var student1 = new Student("Витек Помогайкин", 20, "vitek@student.ru", "S2024001");
            var student2 = new Student("Ликер кофейный", 21, "cofee@student.ru", "S2024002");

            var course1 = new Course("Программирование на C#", "Основы программирования на языке C#", 4);
            var course2 = new Course("Алгебра", "Высшая математика", 3);

            AddProfessor(prof1);
            AddProfessor(prof2);
            AddStudent(student1);
            AddStudent(student2);
            AddCourse(course1);
            AddCourse(course2);

            course1.AssignProfessor(prof1);
            course2.AssignProfessor(prof2);

            student1.EnrollInCourse(course1);
            student1.EnrollInCourse(course2);
            student2.EnrollInCourse(course1);
        }
        public void AddStudent(Student student)
        {
            if (student != null && !students.Contains(student))
                students.Add(student);
        }

        public Student FindStudentById(string studentId)
        {
            return students.FirstOrDefault(s => s.StudentId == studentId);
        }

        public void DisplayAllStudents()
        {
            Console.WriteLine("ВСЕ СТУДЕНТЫ");
            foreach (var student in students)
            {
                Console.WriteLine(student.GetInfo());
                Console.WriteLine();
            }
        }

        public void AddProfessor(Professor professor)
        {
            if (professor != null && !professors.Contains(professor))
                professors.Add(professor);
        }

        public Professor FindProfessorById(int id)
        {
            return professors.FirstOrDefault(p => p.Id == id);
        }

        public void DisplayAllProfessors()
        {
            Console.WriteLine("ВСЕ ПРЕПОДАВАТЕЛИ");
            foreach (var professor in professors)
            {
                Console.WriteLine(professor.GetInfo());
                Console.WriteLine();
            }
        }
        public Student FindStudentByName(string name)
        {
            return students.FirstOrDefault(s => s.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public Professor FindProfessorByName(string name)
        {
            return professors.FirstOrDefault(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public Course FindCourseByName(string name)
        {
            return courses.FirstOrDefault(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }
        public void AddNewStudent()
        {
            try
            {
                Console.Write("Введите имя студента: ");
                string name = Console.ReadLine();

                Console.Write("Введите возраст студента: ");
                int age = int.Parse(Console.ReadLine());

                Console.Write("Введите контактную информацию: ");
                string contactInfo = Console.ReadLine();

                Console.Write("Введите ID студента: ");
                string studentId = Console.ReadLine();

                var student = new Student(name, age, contactInfo, studentId);
                AddStudent(student);
                Console.WriteLine("Студент успешно добавлен!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }
        public void AddNewProfessor()
        {
            try
            {
                Console.Write("Введите имя преподавателя: ");
                string name = Console.ReadLine();

                Console.Write("Введите возраст преподавателя: ");
                int age = int.Parse(Console.ReadLine());

                Console.Write("Введите контактную информацию: ");
                string contactInfo = Console.ReadLine();

                Console.Write("Введите кафедру: ");
                string department = Console.ReadLine();

                Console.Write("Введите зарплату: ");
                decimal salary = decimal.Parse(Console.ReadLine());

                var professor = new Professor(name, age, contactInfo, department, salary);
                AddProfessor(professor);
                Console.WriteLine("Преподаватель успешно добавлен!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }
        public void AddNewCourse()
        {
            try
            {
                Console.Write("Введите название курса: ");
                string name = Console.ReadLine();

                Console.Write("Введите описание курса: ");
                string description = Console.ReadLine();

                Console.Write("Введите количество кредитов: ");
                int credits = int.Parse(Console.ReadLine());

                var course = new Course(name, description, credits);
                AddCourse(course);
                Console.WriteLine("Курс успешно добавлен!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }
        public void EnrollStudentInCourse()
        {
            Console.Write("Введите ID студента: ");
            string studentId = Console.ReadLine();
            var student = FindStudentById(studentId);

            if (student == null)
            {
                Console.WriteLine("Студент не найден!");
                return;
            }
            Console.Write("Введите название курса: ");
            string courseName = Console.ReadLine();
            var course = FindCourseByName(courseName);

            if (course == null)
            {
                Console.WriteLine("Курс не найден!");
                return;
            }

            student.EnrollInCourse(course);
            Console.WriteLine("Студент успешно записан на курс!");
        }
        public void AssignProfessorToCourse()
        {
            Console.Write("Введите имя преподавателя: ");
            string professorName = Console.ReadLine();
            var professor = FindProfessorByName(professorName);

            if (professor == null)
            {
                Console.WriteLine("Преподаватель не найден!");
                return;
            }

            Console.Write("Введите название курса: ");
            string courseName = Console.ReadLine();
            var course = FindCourseByName(courseName);

            if (course == null)
            {
                Console.WriteLine("Курс не найден!");
                return;
            }

            course.AssignProfessor(professor);
            Console.WriteLine("Преподаватель успешно назначен на курс!");
        }




