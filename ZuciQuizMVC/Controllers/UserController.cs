using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System.Net.Http.Json;
using ZuciQuizLibrary.Models;
using ZuciQuizLibrary.Services.Interfaces;

namespace ZuciQuizMVC.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        static HttpClient Svc = new HttpClient { BaseAddress = new Uri("http://localhost:5182/api/User/") };
        public ActionResult HomeView()
        {
            return View();
        }
        public async Task<ActionResult> Index()
        {
            List<User> users = await Svc.GetFromJsonAsync<List<User>>("");
            return View(users);
        }

        public async Task<ActionResult> Details(int userId)
        {
            User user = await Svc.GetFromJsonAsync<User>($"ByUserId/{userId}");
            return View(user);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(User user)
        {
            try
            {
                await Svc.PostAsJsonAsync<User>("", user);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [Route("User/Edit/{userId}")]
        public async Task<ActionResult> Edit(int UserId)
        {
            User user = await Svc.GetFromJsonAsync<User>($"ByUserId/{UserId}");
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("User/Edit/{userId}")]
        public async Task<ActionResult> Edit(int userId, User user)
        {
            try
            {
                await Svc.PutAsJsonAsync<User>("" + userId, user);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        [Route("User/Delete/{userId}")]
        public async Task<ActionResult> Delete(int userId)
        {
            User user = await Svc.GetFromJsonAsync<User>($"ByUserId/{userId}");
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("User/Delete/{userId}")]
        public async Task<ActionResult> Delete(int userId, IFormCollection collection)
        {
            try
            {
                await Svc.DeleteAsync($"{userId}");
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public async Task<ActionResult> GetUserByUserId(int userId)
        {
            User user = await Svc.GetFromJsonAsync<User>($"ByUserId/ {userId}");
            return View(user);
        }
    }
}
