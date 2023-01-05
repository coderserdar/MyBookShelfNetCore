using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBookShelf.Business.Contexts;
using MyBookShelf.Data.Services;
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
        if (ModelState.IsValid)
        {
            var userService = new UserService();
            var cryptoPassword = string.Empty;
            if (!string.IsNullOrEmpty(model.Password))
                cryptoPassword = Helpers.CryptoOperations.CalculateSHA256((model.Password)).ToLower();
            var userExist = userService.UserExist(model.UserName, cryptoPassword);
            if (userExist)
                return RedirectToAction("Index");
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