using System;
using System.Collections.Generic;

namespace Taskify.Modules.ToDo.Models
{
    public class TaskItem
    {
        public Guid Guid { get; } = new Guid();

        public string Title { get; set; }

        public string Description { get; set; }

        public bool IsCompleted { get; set; }

        public DateTime Deadline { get; set; }

        public List<DateTime> Reminders { get; set; } = new List<DateTime>();

        public Priority Priority { get; set; }

        public TaskItem() { }
    }

    public enum Priority
    {
        Low,
        Medium,
        High
    }
}
