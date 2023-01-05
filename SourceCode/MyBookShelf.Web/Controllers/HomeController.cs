using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBookShelf.Business.Contexts;
using MyBookShelf.Web.Models;

namespace MyBookShelf.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    public DatabaseContext _context;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Login()
    {
        var model = new LoginViewModel();
        return View(model);
    }
    
    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        _context = new DatabaseContext();
        if (ModelState.IsValid)
        {
            var user = from m in _context.Users select m;
            user = user.Where(s => s.UserName.Contains(model.UserName) || s.EMailAddress.Contains(model.UserName));
            if (user.Count() != 0)
            {
                if (user.First().Password == model.Password)
                {
                    return RedirectToAction("Index");
                }
            }
        }
        return RedirectToAction("Error");
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
    }
}