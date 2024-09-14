using System;
using System.Collections.Generic;

class Employee
{
    public int Id { get; }
    public string Name { get; set; }
    public string Position { get; set; }
    public decimal Salary { get; set; }

    public Employee(int id, string name, string position, decimal salary)
    {
        Id = id;
        Name = name;
        Position = position;
        Salary = salary;
    }

    public override string ToString() => $"ID: {Id}, Name: {Name}, Position: {Position}, Salary: {Salary:C}";
}

class EmployeeManager
{
    private readonly List<Employee> employees = new();
    private int nextId = 1;

    public void AddEmployee(string name, string position, decimal salary)
    {
        employees.Add(new Employee(nextId++, name, position, salary));
        Console.WriteLine("Сотрудник добавлен.");
    }

    public void UpdateEmployee(int id, string name, string position, decimal salary)
    {
        var employee = employees.Find(e => e.Id == id);
        if (employee != null)
        {
            employee.Name = name;
            employee.Position = position;
            employee.Salary = salary;
            Console.WriteLine("Сотрудник обновлен.");
        }
        else
        {
            Console.WriteLine("Сотрудник не найден.");
        }
    }

    public void GetEmployee(int id)
    {
        var employee = employees.Find(e => e.Id == id);
        if (employee != null)
        {
            Console.WriteLine(employee);
        }
        else
        {
            Console.WriteLine("Сотрудник не найден.");
        }
    }

    public void CalculateTotalSalaries()
    {
        decimal totalSalary = 0;
        employees.ForEach(e => totalSalary += e.Salary);
        Console.WriteLine($"Общая зарплата сотрудников: {totalSalary:C}");
    }

    public void ListEmployees()
    {
        if (employees.Count == 0)
        {
            Console.WriteLine("Нет сотрудников для отображения.");
            return;
        }

        Console.WriteLine("Список сотрудников:");
        employees.ForEach(e => Console.WriteLine(e));
    }
}

class Program
{
    static void Main()
    {
        var employeeManager = new EmployeeManager();
        bool running = true;

        while (running)
        {
            Console.WriteLine("\nМеню:\n1. Добавить сотрудника\n2. Обновить информацию о сотруднике\n3. Получить информацию о сотруднике\n4. Рассчитать общую зарплату\n5. Показать всех сотрудников\n6. Выход");
            Console.Write("Выберите действие: ");

            switch (Console.ReadLine())
            {
                case "1":
                    Console.Write("Введите имя: ");
                    string name = Console.ReadLine();
                    Console.Write("Введите должность: ");
                    string position = Console.ReadLine();
                    Console.Write("Введите зарплату: ");
                    decimal salary = decimal.Parse(Console.ReadLine());
                    employeeManager.AddEmployee(name, position, salary);
                    break;

                case "2":
                    Console.Write("Введите ID сотрудника для обновления: ");
                    int updateId = int.Parse(Console.ReadLine());
                    Console.Write("Введите новое имя: ");
                    string newName = Console.ReadLine();
                    Console.Write("Введите новую должность: ");
                    string newPosition = Console.ReadLine();
                    Console.Write("Введите новую зарплату: ");
                    decimal newSalary = decimal.Parse(Console.ReadLine());
                    employeeManager.UpdateEmployee(updateId, newName, newPosition, newSalary);
                    break;

                case "3":
                    Console.Write("Введите ID сотрудника для получения информации: ");
                    int getId = int.Parse(Console.ReadLine());
                    employeeManager.GetEmployee(getId);
                    break;

                case "4":
                    employeeManager.CalculateTotalSalaries();
                    break;

                case "5":
                    employeeManager.ListEmployees();
                    break;

                case "6":
                    running = false;
                    break;

                default:
                    Console.WriteLine("Некорректный выбор. Попробуйте снова.");
                    break;
            }
        }
    }
}