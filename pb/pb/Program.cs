using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Abonent
{
    public string Name { get; set; }
    public string PhoneNumber { get; set; }

    public Abonent(string name, string phoneNumber)
    {
        Name = name;
        PhoneNumber = phoneNumber;
    }
}

class Phonebook
{
    private static Phonebook _instance;
    private List<Abonent> _abonents;
    private const string FilePath = "phonebook.txt";

    private Phonebook()
    {
        _abonents = new List<Abonent>();
        LoadFromFile();
    }

    public static Phonebook Instance => _instance ??= new Phonebook();

    public void AddAbonent(string name, string phoneNumber)
    {
        if (_abonents.Any(a => a.PhoneNumber == phoneNumber))
        {
            Console.WriteLine("Абонент с таким номером уже существует.");
            return;
        }

        var abonent = new Abonent(name, phoneNumber);
        _abonents.Add(abonent);
        SaveToFile();
        Console.WriteLine("Абонент добавлен.");
    }

    public void RemoveAbonent(string phoneNumber)
    {
        var abonent = _abonents.FirstOrDefault(a => a.PhoneNumber == phoneNumber);
        if (abonent != null)
        {
            _abonents.Remove(abonent);
            SaveToFile();
            Console.WriteLine("Абонент удален.");
        }
        else
        {
            Console.WriteLine("Абонент не найден.");
        }
    }

    public Abonent GetAbonentByPhoneNumber(string phoneNumber)
    {
        return _abonents.FirstOrDefault(a => a.PhoneNumber == phoneNumber);
    }

    public string GetPhoneNumberByName(string name)
    {
        var abonent = _abonents.FirstOrDefault(a => a.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        return abonent?.PhoneNumber;
    }

    private void LoadFromFile()
    {
        if (File.Exists(FilePath))
        {
            var lines = File.ReadAllLines(FilePath);
            foreach (var line in lines)
            {
                var parts = line.Split(',');
                if (parts.Length == 2)
                {
                    var abonent = new Abonent(parts[0].Trim(), parts[1].Trim());
                    _abonents.Add(abonent);
                }
            }
        }
    }

    private void SaveToFile()
    {
        using (var writer = new StreamWriter(FilePath))
        {
            foreach (var abonent in _abonents)
            {
                writer.WriteLine($"{abonent.Name}, {abonent.PhoneNumber}");
            }
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        var phonebook = Phonebook.Instance;

        while (true)
        {
            Console.WriteLine("Выберите действие:");
            Console.WriteLine("1. Добавить абонента");
            Console.WriteLine("2. Удалить абонента");
            Console.WriteLine("3. Получить абонента по номеру телефона");
            Console.WriteLine("4. Получить номер телефона по имени абонента");
            Console.WriteLine("5. Выход");
            Console.Write("Ваш выбор: ");

            var choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Console.Write("Введите имя абонента: ");
                    var name = Console.ReadLine();
                    Console.Write("Введите номер телефона: ");
                    var phoneNumber = Console.ReadLine();
                    phonebook.AddAbonent(name, phoneNumber);
                    break;

                case "2":
                    Console.Write("Введите номер телефона для удаления: ");
                    var numberToRemove = Console.ReadLine();
                    phonebook.RemoveAbonent(numberToRemove);
                    break;

                case "3":
                    Console.Write("Введите номер телефона: ");
                    var numberToFind = Console.ReadLine();
                    var abonent = phonebook.GetAbonentByPhoneNumber(numberToFind);
                    if (abonent != null)
                    {
                        Console.WriteLine($"Абонент: {abonent.Name}");
                    }
                    else
                    {
                        Console.WriteLine("Абонент не найден.");
                    }
                    break;

                case "4":
                    Console.Write("Введите имя абонента: ");
                    var nameToFind = Console.ReadLine();
                    var foundNumber = phonebook.GetPhoneNumberByName(nameToFind);
                    if (foundNumber != null)
                    {
                        Console.WriteLine($"Номер телефона: {foundNumber}");
                    }
                    else
                    {
                        Console.WriteLine("Абонент не найден.");
                    }
                    break;

                case "5":
                    return;

                default:
                    Console.WriteLine("Некорректный выбор. Попробуйте снова.");
                    break;
            }

            Console.WriteLine();
        }
    }
}