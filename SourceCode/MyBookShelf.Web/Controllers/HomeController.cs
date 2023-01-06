using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyBookShelf.Data.Services;
using MyBookShelf.Web.Helpers;
using MyBookShelf.Web.Models;

namespace MyBookShelf.Web.Controllers;

[NoDirectAccess]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        if (HttpContext.Session.Keys.ToList().Any(j => j == "ActiveUserName"))
            ViewBag.Message = HttpContext.Session.GetString("ActiveUserName");
        return View();
    }

    public IActionResult Login()
    {
        #region Session Operations
        if (HttpContext.Session.Keys.ToList().Any(j => j == "ActiveUserId"))
            HttpContext.Session.SetString("ActiveUserId", "");
        else
        {
            HttpContext.Session.Keys.ToList().Add("ActiveUserId");
            HttpContext.Session.SetString("ActiveUserId", ""); 
        }
        #endregion
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
                if (!string.IsNullOrEmpty(result.Id))
                {
                    if (result.IsError)
                    {
                        model = new LoginViewModel();
                        model.OperationMessage = result.Id;
                        ViewBag.Message = result;
                        return View("Login", model);
                    }
                    else
                    {
                        #region Session Operations
                        if (HttpContext.Session.Keys.ToList().Any(j => j == "ActiveUserId"))
                            HttpContext.Session.SetString("ActiveUserId", result.ActiveUser.Id);
                        else
                        {
                            HttpContext.Session.Keys.ToList().Add("ActiveUserId");
                            HttpContext.Session.SetString("ActiveUserId", result.ActiveUser.Id);  
                        }
                        HttpContext.Session.Keys.ToList().Add("ActiveUserIsAdmin");
                        HttpContext.Session.SetString("ActiveUserIsAdmin", result.ActiveUser.IsAdmin ? "1" : "0");
                        HttpContext.Session.Keys.ToList().Add("ActiveUserName");
                        HttpContext.Session.SetString("ActiveUserName", result.ActiveUser.UserName);
                        #endregion
                        return RedirectToAction("Index");
                    }
                }
                return RedirectToAction("Index");
            }
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