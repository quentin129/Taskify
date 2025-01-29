﻿using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using Taskify.Modules.ToDo.Models;

namespace Taskify.Modules.ToDo
{
    public class ToDoDataService
    {
        private static readonly string FolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Taskify");
        private static readonly string FilePath = Path.Combine(FolderPath, "todos.json");

        // Speichert die Notizen als JSON in LocalAppData
        public static void SaveToDos(ObservableCollection<TaskItem> todos)
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