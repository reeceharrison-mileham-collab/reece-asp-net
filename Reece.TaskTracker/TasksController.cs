using Microsoft.AspNetCore.Mvc;
using Reece.TaskTracker.Data;

namespace Reece.TaskTracker.Controllers;

public class TasksController : Controller
{
    private readonly TaskRepository _repo;

    public TasksController(TaskRepository repo)
    {
        _repo = repo;
    }

    public IActionResult Index()
    {
        return View(_repo.GetAll());
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(string description, DateTime dueDate)
    {
        _repo.Create(description, dueDate);
        return RedirectToAction(nameof(Index));
    }
}

