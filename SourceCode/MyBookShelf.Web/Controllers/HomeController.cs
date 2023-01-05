using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyBookShelf.Data.Services;
using MyBookShelf.Web.Models;

namespace MyBookShelf.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
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

    [AllowAnonymous]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Login(LoginViewModel model)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var userService = new UserService();
                var cryptoPassword = string.Empty;
                if (!string.IsNullOrEmpty(model.Password))
                    cryptoPassword = Helpers.CryptoOperations.CalculateSHA256((model.Password)).ToLower();
                var result = userService.UserLogin(model.UserName, cryptoPassword);
                // return Json(new SpecialJsonResult() {IsError = false, MessageText = result});
                if (!string.IsNullOrEmpty(result))
                {
                    model = new LoginViewModel();
                    model.OperationMessage = result;
                    ViewBag.Message = result;
                    return View("Login", model);
                }
                else
                    return RedirectToAction("Index");
            }
            else
                return RedirectToAction("Error");
        }
        catch (Exception e)
        {
            return RedirectToAction("Error");
        }
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