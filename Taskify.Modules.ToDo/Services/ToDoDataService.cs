using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using Taskify.Modules.ToDo.Models;
using Taskify.Modules.ToDo.ViewModels;

namespace Taskify.Modules.ToDo.Services
{
    public class ToDoDataService
    {
        private static readonly string FolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Taskify");
        private static readonly string FilePath = Path.Combine(FolderPath, "todos.json");

        // Speichert die Notizen als JSON in LocalAppData
        public static void SaveTaskItems(ObservableCollection<TaskItem> todos)
        {
            try
            {
                if (!Directory.Exists(FolderPath))
                {
                    Directory.CreateDirectory(FolderPath);
                }

                string json = JsonSerializer.Serialize(todos, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(FilePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fehler beim Speichern: {ex.Message}");
            }
        }


        public static ObservableCollection<TaskItem> ConvertListOfTaskItemVmsToTaskItems(ObservableCollection<TaskItemViewModel> taskItemVMs)
        {
            ObservableCollection<TaskItem> tasks = new ObservableCollection<TaskItem>();

            foreach (TaskItemViewModel taskVM in taskItemVMs)
            {
                tasks.Add(taskVM.Task);
            }

            return tasks;
        }

        // Lädt die Notizen aus LocalAppData
        public static ObservableCollection<TaskItem> LoadToDos()
        {
            try
            {
                if (File.Exists(FilePath))
                {
                    string json = File.ReadAllText(FilePath);
                    return JsonSerializer.Deserialize<ObservableCollection<TaskItem>>(json) ?? new ObservableCollection<TaskItem>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fehler beim Laden: {ex.Message}");
            }
            return new ObservableCollection<TaskItem>();
        }
    }
}