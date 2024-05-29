using System;
using System.Collections.Generic;
using System.IO;

public class Task
{
    public int Id { get; set; }
    public string Description { get; set; }
    public bool IsCompleted { get; set; }

    public Task(int id, string description)
    {
        Id = id;
        Description = description;
        IsCompleted = false;
    }

    public override string ToString()
    {
        return $"{Id}. {Description} - {(IsCompleted ? "Tamamlandı" : "Tamamlanmadı")}";
    }
}

class Program
{
    static List<Task> tasks = new List<Task>();
    static int nextId = 1;

    static void Main(string[] args)
    {
        LoadTasksFromFile();

        while (true)
        {
            Console.WriteLine("1. Görev Ekle");
            Console.WriteLine("2. Görevleri Listele");
            Console.WriteLine("3. Görev Sil");
            Console.WriteLine("4. Görev Güncelle");
            Console.WriteLine("5. Görevi Tamamla");
            Console.WriteLine("6. Çıkış");
            Console.Write("Bir seçenek girin: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddTask();
                    break;
                case "2":
                    ListTasks();
                    break;
                case "3":
                    DeleteTask();
                    break;
                case "4":
                    UpdateTask();
                    break;
                case "5":
                    CompleteTask();
                    break;
                case "6":
                    SaveTasksToFile();
                    return;
                default:
                    Console.WriteLine("Geçersiz seçenek, lütfen tekrar deneyin.");
                    break;
            }
            Console.WriteLine("****************");
        }
    }

    static void AddTask()
    {
        Console.Write("Görev açıklamasını girin: ");
        string description = Console.ReadLine();
        tasks.Add(new Task(nextId++, description));
        Console.WriteLine("Görev eklendi.");
    }

    static void ListTasks()
    {
        foreach (var task in tasks)
        {
            Console.WriteLine(task);
        }
    }

    static void DeleteTask()
    {
        Console.Write("Silmek istediğiniz görevin ID'sini girin: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            var task = tasks.Find(t => t.Id == id);
            if (task != null)
            {
                tasks.Remove(task);
                Console.WriteLine("Görev silindi.");
            }
            else
            {
                Console.WriteLine("Görev bulunamadı.");
            }
        }
        else
        {
            Console.WriteLine("Geçersiz ID.");
        }
    }

    static void UpdateTask()
    {
        Console.Write("Güncellemek istediğiniz görevin ID'sini girin: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            var task = tasks.Find(t => t.Id == id);
            if (task != null)
            {
                Console.Write("Yeni açıklamayı girin: ");
                task.Description = Console.ReadLine();
                Console.WriteLine("Görev güncellendi.");
            }
            else
            {
                Console.WriteLine("Görev bulunamadı.");
            }
        }
        else
        {
            Console.WriteLine("Geçersiz ID.");
        }
    }

    static void CompleteTask()
    {
        Console.Write("Tamamlamak istediğiniz görevin ID'sini girin: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            var task = tasks.Find(t => t.Id == id);
            if (task != null)
            {
                task.IsCompleted = true;
                Console.WriteLine("Görev tamamlandı.");
            }
            else
            {
                Console.WriteLine("Görev bulunamadı.");
            }
        }
        else
        {
            Console.WriteLine("Geçersiz ID.");
        }
    }

    static void SaveTasksToFile()
    {
        using (StreamWriter sw = new StreamWriter("tasks.txt"))
        {
            foreach (var task in tasks)
            {
                sw.WriteLine($"{task.Id},{task.Description},{task.IsCompleted}");
            }
        }
        Console.WriteLine("Görevler dosyaya kaydedildi.");
    }

    static void LoadTasksFromFile()
    {
        if (File.Exists("tasks.txt"))
        {
            var lines = File.ReadAllLines("tasks.txt");
            foreach (var line in lines)
            {
                var parts = line.Split(',');
                if (parts.Length == 3 && int.TryParse(parts[0], out int id) && bool.TryParse(parts[2], out bool isCompleted))
                {
                    tasks.Add(new Task(id, parts[1]) { IsCompleted = isCompleted });
                    nextId = Math.Max(nextId, id + 1);
                }
            }
            Console.WriteLine("Görevler dosyadan yüklendi.");
        }
    }
}
