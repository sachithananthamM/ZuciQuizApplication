using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
using ZuciQuizLibrary.DataAccessLayer.Interfaces;
using ZuciQuizLibrary.Models;

namespace ZuciQuizMVC.Controllers
{
    public class TopicController : Controller
    {
        ITopicRepository _topicRepository;
        static HttpClient Svc = new HttpClient { BaseAddress = new Uri("http://localhost:5182/api/Topic/") };
        static HttpClient Svc1 = new HttpClient { BaseAddress = new Uri("http://localhost:5182/api/User/") };
        public async Task<ActionResult> Index()
        {
            List<Topic> topics = await Svc.GetFromJsonAsync<List<Topic>>("");
            ViewBag.RoleId = HttpContext.Session.GetInt32("Role");
            ViewBag.Id = HttpContext.Session.GetInt32("userId");
            return View(topics);
        }

        public async Task<ActionResult> Details(int topicId)
        {
            Topic topic = await Svc.GetFromJsonAsync<Topic>($"ByTopicId/{topicId}");
            //User user = await Svc1.GetFromJsonAsync<User>($"ByUserId/{topic.CreatedBy}");
            //topic.CreatedBy = user.UserName;
            return View(topic);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Topic topic)
        {
            try
            {
                ViewBag.Id = (int)HttpContext.Session.GetInt32("userId");
                topic.CreatedBy = ViewBag.Id;
                topic.ModifiedBy = ViewBag.Id;
                await Svc.PostAsJsonAsync<Topic>("", topic);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [Route("Topic/Edit/{topicId}")]
        public async Task<ActionResult> Edit(int topicId)
        {
            Topic topic = await Svc.GetFromJsonAsync<Topic>($"ByTopicId/{topicId}");
            return View(topic);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Topic/Edit/{topicId}")]
        public async Task<ActionResult> Edit(int topicId, Topic topic)
        {
            try
            {
                ViewBag.Id = (int)HttpContext.Session.GetInt32("userId");
                topic.ModifiedBy = ViewBag.Id;
                await Svc.PutAsJsonAsync<Topic>($"ByTopicId/{topicId}", topic);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [Route("Topic/Delete/{topicId}")]
        public async Task<ActionResult> Delete(int topicId)
        {
            Topic topic = await Svc.GetFromJsonAsync<Topic>($"ByTopicId/{topicId}");
            return View(topic);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Topic/Delete/{topicId}")]
        public async Task<ActionResult> Delete(int topicId, IFormCollection collection)
        {
            try
            {
                await Svc.DeleteAsync($"ByTopicId/{topicId}");
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
