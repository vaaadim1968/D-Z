using System;
using System.Collections.Generic;

class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }

    public override string ToString()
    {
        return $"ID: {Id}, Name: {Name}, Age: {Age}";
    }
}

class UserManager
{
    private List<User> users = new List<User>();
    private int nextId = 1;

    public void AddUser(string name, int age)
    {
        User user = new User
        {
            Id = nextId++,
            Name = name,
            Age = age
        };
        users.Add(user);
        Console.WriteLine("Пользователь добавлен.");
    }

    public void RemoveUser(int id)
    {
        User user = users.Find(u => u.Id == id);
        if (user != null)
        {
            users.Remove(user);
            Console.WriteLine("Пользователь удален.");
        }
        else
        {
            Console.WriteLine("Пользователь не найден.");
        }
    }

    public void ListUsers()
    {
        if (users.Count == 0)
        {
            Console.WriteLine("Нет пользователей для отображения.");
            return;
        }

        Console.WriteLine("Список пользователей:");
        foreach (var user in users)
        {
            Console.WriteLine(user);
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        UserManager userManager = new UserManager();
        bool running = true;

        while (running)
        {
            Console.WriteLine("\nМеню:");
            Console.WriteLine("1. Добавить пользователя");
            Console.WriteLine("2. Удалить пользователя");
            Console.WriteLine("3. Показать всех пользователей");
            Console.WriteLine("4. Выход");
            Console.Write("Выберите действие: ");

            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    try
                    {
                        Console.Write("Введите имя: ");
                        string name = Console.ReadLine();
                        Console.Write("Введите возраст: ");
                        int age = int.Parse(Console.ReadLine());
                        userManager.AddUser(name, age);
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Некорректный ввод возраста.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Произошла ошибка: {ex.Message}");
                    }
                    break;

                case "2":
                    try
                    {
                        Console.Write("Введите ID пользователя для удаления: ");
                        int id = int.Parse(Console.ReadLine());
                        userManager.RemoveUser(id);
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Некорректный ввод ID.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Произошла ошибка: {ex.Message}");
                    }
                    break;

                case "3":
                    userManager.ListUsers();
                    break;

                case "4":
                    running = false;
                    break;

                default:
                    Console.WriteLine("Некорректный выбор. Попробуйте снова.");
                    break;
            }
        }
    }
}