using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee
{
    public interface IEmployee
    {
        string FirstName { get; set; }
        string LastName { get; set; }
        DateTime DateOfHire { get; set; }
        string Position { get; set; }
        decimal Salary { get; set; }
        char Gender { get; set; }
    }
    public struct Employee : IEmployee
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfHire { get; set; }
        public string Position { get; set; }
        public decimal Salary { get; set; }
        public char Gender { get; set; }

        /// <summary>
        /// a.	вывести полную информацию обо всех сотрудниках; (желательно использовать перегрузку функции ToString())
        /// </summary>
        public override string ToString()
        {
            return $"Имя и фамилия: {FirstName} {LastName}, Дата принятия на работу: {DateOfHire} , Занимаемая должность: {Position}, Получаемая зарплата: {Salary}, Пол: {Gender}";
        }
    }

    
    public class Program
    {
        /// <summary>
        /// Метод для чтения информации о сотруднике от пользователя
        /// </summary>
        /// <returns></returns>
        static Employee ReadEmployeeInfo()
        {
            Employee employee = new Employee();
            
            Console.WriteLine("Введите имя Вашего сотрудника: ");
            employee.FirstName = Console.ReadLine();

            Console.WriteLine("Введите фамилию Вашего сотрудника: ");
            employee.LastName = Console.ReadLine();
            
            Console.WriteLine("Укажите занимаемую должность/позицию Вашего сотрудника: ");
            employee.Position = Console.ReadLine();

            Console.WriteLine("Введите дату вступления на работу Вашего сотрудника (гггг/мм/дд): ");
            employee.DateOfHire = DateTime.Parse(Console.ReadLine());

            Console.WriteLine("Укажите зарплату Вашего сотрудника: ");
            employee.Salary = decimal.Parse(Console.ReadLine());

            Console.WriteLine("Укажите гендер Вашего сотрудника (ну, или как он(-а/-о) себя идентифицирует): ");
            employee.Gender = char.ToUpper(Console.ReadKey().KeyChar);

            Console.WriteLine();
            return employee;
        }
        /// <summary>
        /// a.вывести полную информацию обо всех сотрудниках;
        /// </summary>
        static void PrintAllEmployeesInfo(Employee[] employees)
        {
            Console.WriteLine("\nВот, полная информация обо всех сотрудниках вашей компании «ТОО Reich»:");
            foreach(var employee in employees)
            {
                Console.WriteLine(employee);
            }
            Console.WriteLine();
        }
        /// <summary>
        /// b.	вывести полную информацию о сотрудниках, выбранной должности
        /// </summary>
        static void PrintEmployeesByPosition(Employee[] employees, string position)
        {
            foreach(var employee in employees)
            {
                if(employee.Position.ToLower() == position.ToLower())
                {
                    Console.WriteLine(employee);
                }
            }
            Console.WriteLine();
        }
        /// <summary>
        /// c.	найти в массиве всех менеджеров, 
        ///     зарплата которых больше средней зарплаты всех клерков, 
        ///     вывести на экран полную информацию о таких менеджерах отсортированной по их фамилии.
        /// </summary>
        static void PrintAllManagaresAboveClerksAverageSalary(Employee[] employees)
        {
            decimal clerksAverageSalary = CalculateAverageSalaryByPosition(employees, "э-Клерк");

            //Linq запрос, ыы
            var managers = from employee in employees
                           where employee.Position.ToLower() == "manager"
                           &&
                           employee.Salary > clerksAverageSalary
                           orderby employee.LastName
                           select employee;
            Console.WriteLine("\nМанагеры, зарплаты которых больше средней зарплаты клерков: ");

            foreach(var manager in managers)
            {
                Console.WriteLine(manager);
            }
            Console.WriteLine();
        }
        /// <summary>
        /// Метод для расчета средней з/п по выбранной должности
        /// </summary>
        static decimal CalculateAverageSalaryByPosition(Employee[] employees, string position)
        {
            var averageSalary = employees.Where(e => e.Position.ToLower() == position.ToLower())
                .Select(e => e.Salary)
                .DefaultIfEmpty(0)
                .Average();
            return averageSalary;
        }
        /// <summary>
        /// d.	распечатать информацию обо всех сотрудниках, 
        ///     принятых на работу позже определенной даты 
        ///     (дата передается пользователем), 
        ///     отсортированную в алфавитном порядке по фамилии сотрудника.
        /// </summary>
        static void PrintEmployeesHiredAfterDate(Employee[] employees, DateTime DateOfHireFilter)
        {
            var selectedEmployees = from employee in employees
                                    where employee.DateOfHire > DateOfHireFilter
                                    orderby employee.LastName
                                    select employee;
            Console.WriteLine("\nИнформация о сотрудниках, принятых на работу «определенной даты»: ");

            foreach(var employee in selectedEmployees)
            {
                Console.WriteLine(employee);
            }
            Console.WriteLine();
        }
        /// <summary>
        /// e.	Вывести информацию о всех мужчинах, 
        ///     женщинах в зависимости от того что передаст пользователь. 
        ///     Предусмотреть случай, 
        ///     когда, 
        ///     пользователь не выбирает конкретный пол, 
        ///     т.е. просто хочет получить информацию о всех.
        /// </summary>
        static void PrintEmployeesByGender(Employee[] employees, char genderFilter)
        {
            string genderDescription = GetGenderDescription(genderFilter);

            var selectedEmployees = from employee in employees
                                    where employee.Gender == genderFilter || genderFilter == 'Л'
                                    orderby employee.LastName
                                    select employee;
            Console.WriteLine($"\nИнформация о {genderDescription}: ");

            foreach (var employee in selectedEmployees)
            {
                Console.WriteLine(employee);
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Ввод гендеров для получения описания пола: 
        /// </summary>
        static string GetGenderDescription(char genderFilter)
        {
            switch (char.ToUpper(genderFilter))
            {
                case 'М':
                    return "мужчинах";
                case 'Ж':
                    return "женщинах";
                case 'В':
                    return "вертолетах";
                case 'Л':
                    return "всех сотрудниках";
                default:
                    return "сотрудниках";
            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Добро пожаловать в компанию «ТОО Reich»!");
            Console.WriteLine("Введите количество сотрудников, работающих в Вашей компании: ");

            int employeeCount = int.Parse(Console.ReadLine());
            Employee[] employees = new Employee[employeeCount];

            for (int i = 0; i < employees.Length; i++)
            {
                employees[i] = ReadEmployeeInfo();
            }
            ///a.вывести полную информацию обо всех сотрудниках;
            PrintAllEmployeesInfo(employees);

            Console.WriteLine("Введите должности для вывода информации: ");
            string positionToPrint = Console.ReadLine();
            PrintEmployeesByPosition(employees, positionToPrint);

            Console.WriteLine("Менеджера с зарплатой по выше средней зарплаты клерков");
            PrintAllManagaresAboveClerksAverageSalary(employees);

            Console.WriteLine("Введите дату для фильтрации (гггг|мм|дд):");
            DateTime DateOfHireFilter = DateTime.Parse(Console.ReadLine());
            PrintEmployeesHiredAfterDate(employees, DateOfHireFilter);

            Console.WriteLine("Укажите пол для вывода информации (М/Ж/В/Л. М-Мужчина, Ж - Женщина, В - Вертолет, Л - Любой): ");
            char genderFilter = char.ToUpper(Console.ReadKey().KeyChar);
            PrintEmployeesByGender(employees, genderFilter);

            Console.ReadLine();

        }
    }
}
