using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using University.Models;
using University.Services;
using University.Data;
using Microsoft.EntityFrameworkCore;

namespace University.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    private readonly IEmailRepository _emailRepository;

    private readonly UniversityContext _context;

    public HomeController(ILogger<HomeController> logger, IEmailRepository emailRepository, UniversityContext context)
    {
        _logger = logger;
        _emailRepository = emailRepository;
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Db()
    {
        var students = _context.Students.ToList();
        return View(students);
    }

    public IActionResult clearStudent()
    {
        _context.Database.ExecuteSqlRaw("SET FOREIGN_KEY_CHECKS = 0; TRUNCATE TABLE Students; SET FOREIGN_KEY_CHECKS = 1;");


        return RedirectToAction("Db");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
