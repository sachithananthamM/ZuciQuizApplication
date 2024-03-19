using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Policy;
using ZuciQuizLibrary.Models;

namespace ZuciQuizMVC.Controllers
{
    public class AccountController : Controller
    {
        
        static HttpClient Svc = new HttpClient { BaseAddress = new Uri("http://localhost:5182/api/User/") };
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            ViewData["ErrorMessage"] = TempData["ErrorMessage"];
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Verify(string userName, string password)
        {
            HttpResponseMessage response = await Svc.GetAsync($"ByuserName/{userName}");
            if (response.IsSuccessStatusCode)
            {
                User user = await response.Content.ReadFromJsonAsync<User>();
                if ((user.UserName != null && user.UserName == userName) && user.Password == password)
                {
                    HttpContext.Session.SetInt32("userId", user.Id);
                    HttpContext.Session.SetString("userName", user.UserName);
                    HttpContext.Session.SetString("Password", user.Password);
                    HttpContext.Session.SetInt32("Role", user.RoleId);

                    if (user.RoleId == 1)
                    {
                        return RedirectToAction("Index", "IndexofAdmin");
                    }
                    else if (user.RoleId == 2)
                    {
                        return RedirectToAction("Index", "Topic");
                    }
                }
                else
                {
                    TempData["ErrorMessage"] = "Enter Correct Password ";
                    return RedirectToAction("Login", "Account");
                }
            }
            else
            {
                TempData["ErrorMessage"] = "Enter Correct UserId  or SignUp new Account";
                return RedirectToAction("Login", "Account");
            }
            return View("Login");
        }


        public ActionResult Register()
        {
            User user = new User();
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(User user)
        {
            try
            {
                HttpResponseMessage res = await Svc.GetAsync($"ByUserName/{user.UserName}");
                if (res.IsSuccessStatusCode)
                {
                    ModelState.AddModelError("userName", "This UserName is  already exists. Please choose a different one.");
                    return View("Register", user);
                }
                else
                {
                    user.RoleId = 2;
                    await Svc.PostAsJsonAsync<User>("", user);
                    return RedirectToAction(nameof(Login));
                }
            }
            catch
            {
                return View();
            }
        }


    }
}
