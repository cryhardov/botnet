using System;
using cryhard.TaskPlanner.DataAccess;
class Program
{
    static void Main()
    {
        var repository = new FileWorkItemsRepository();
        var planner = new SimpleTaskPlanner(repository);
        bool running = true;

        while (running)
        {
            Console.WriteLine("Choose an option: [A]dd, [B]uild, [M]ark complete, [R]emove, [Q]uit");
            var choice = Console.ReadLine()?.ToUpper();

            switch (choice)
            {
                case "A":
                    Console.Write("Enter task name: ");
                    var name = Console.ReadLine();
                    var newTask = new WorkItem { Name = name, IsCompleted = false };
                    repository.Add(newTask);
                    repository.SaveChanges();
                    Console.WriteLine("Task added.");
                    break;
                case "M":
                    Console.Write("Enter task ID to mark complete: ");
                    var id = Guid.Parse(Console.ReadLine() ?? string.Empty);
                    var task = repository.Get(id);
                    if (task != null)
                    {
                        task.IsCompleted = true;
                        repository.Update(task);
                        repository.SaveChanges();
                        Console.WriteLine("Task marked complete.");
                    }
                    else
                    {
                        Console.WriteLine("Task not found.");
                    }
                    break;
                case "R":
                    Console.Write("Enter task ID to remove: ");
                    var removeId = Guid.Parse(Console.ReadLine() ?? string.Empty);
                    repository.Remove(removeId);
                    repository.SaveChanges();
                    Console.WriteLine("Task removed.");
                    break;
                case "Q":
                    running = false;
                    Console.WriteLine("Quitting...");
                    break;
                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
        }
    }
}