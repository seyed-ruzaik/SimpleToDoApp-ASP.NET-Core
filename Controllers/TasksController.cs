
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using ToDoApp.Models;

namespace ToDoApp.Controllers
{
    public class TasksController : Controller
    {
        // In-memory list to store tasks (this would be a database in a real app)
        private static List<TaskItem> tasks = new List<TaskItem>();

        public IActionResult Index()
        {
            return View(tasks);
        }

        [HttpPost]
        public IActionResult Add(string description)
        {
            if (!string.IsNullOrEmpty(description))
            {
                var newTask = new TaskItem
                {
                    Id = tasks.Count > 0 ? tasks.Max(t => t.Id) + 1 : 1,
                    Description = description,
                    IsCompleted = false
                };
                tasks.Add(newTask);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult MarkComplete(int id)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task != null)
            {
                task.IsCompleted = true;
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task != null)
            {
                tasks.Remove(task);
            }
            return RedirectToAction("Index");
        }
    }
}
