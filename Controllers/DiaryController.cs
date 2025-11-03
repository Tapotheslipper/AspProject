using Microsoft.AspNetCore.Mvc;
using AspProject.Models;

namespace AspProject.Controllers;

public class DiaryController : Controller
{
    private static readonly Dictionary<int, DiaryEntry> _entries = new();
    private static int _nextId = 0;

    [HttpGet]
    [Route("/diary")]
    public IActionResult Index()
    {
        TempData["default"] = "Дневник пуст.";
        return View(_entries);
    }

    [HttpGet]
    [Route("diary/{id}")]
    public IActionResult OneEntry(int id)
    {
        if (!_entries.ContainsKey(id))
        {
            TempData["default"] = $"Записи с id {id} не существует.";
            return RedirectToAction("Index");
        }
        return View(_entries[id]);
    }

    [HttpGet]
    [Route("diary/new")]
    public IActionResult AddEntry()
    {
        return View();
    }

    [HttpPost]
    [Route("diary/new")]
    [ValidateAntiForgeryToken]
    public IActionResult AddEntry(DiaryEntry model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        model.Id = _nextId++;
        _entries.Add(model.Id, model);
        TempData["result"] = $"Запись {model.Head} успешно добавлена.";
        return RedirectToAction("Index");
    }

    [HttpGet]
    [Route("diary/edit/{id}")]
    public IActionResult EditEntry(int id)
    {
        if (!_entries.ContainsKey(id))
        {
            TempData["default"] = $"Записи с id {id} не существует.";
            return RedirectToAction("Index");
        }
        return View(_entries[id]);
    }

    [HttpPost("diary/edit/{id}")]
    [ValidateAntiForgeryToken]
    public IActionResult EditEntry(int id, DiaryEntry model)
    {
        if (id != model.Id)
        {
            return BadRequest();
        }
        
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        _entries[model.Id] = model;
        TempData["result"] = "Сохранено.";
        return RedirectToAction("OneEntry", new { id = model.Id });
    }
}
