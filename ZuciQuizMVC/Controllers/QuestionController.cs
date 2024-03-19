using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZuciQuizLibrary.Models;
using ZuciQuizLibrary.Services.Interfaces;

namespace ZuciQuizMVC.Controllers
{

    public class QuestionController : Controller
    {
        readonly IQuestionService _questionService;
        static HttpClient Svc = new HttpClient { BaseAddress = new Uri("http://localhost:5182/api/Question/") };

        public async Task<ActionResult> Index()
        {
            List<Question> questions = await Svc.GetFromJsonAsync<List<Question>>("");
            return View(questions);
        }

        public async Task<ActionResult> Details(int questionId)
        {
            Question question = await Svc.GetFromJsonAsync<Question>($"ByQuestionId/{questionId}");
            return View(question);
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Question question)
        {
            try
            {
                ViewBag.Id = (int)HttpContext.Session.GetInt32("userId");
                question.CreatedBy = ViewBag.Id;
                question.ModifiedBy = ViewBag.Id;
                question.ModifiedOn = DateTime.Now;
                await Svc.PostAsJsonAsync<Question>("", question);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [Route("Question/Edit/{questionId}")]
        public async Task<ActionResult> Edit(int questionId)
        {
            Question question = await Svc.GetFromJsonAsync<Question>("" + "ByQuestionId/" + questionId);
            return View(question);
        }

        [Route("Question/Edit/{questionId}")]
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<ActionResult> Edit(int questionId, Question question)
        {
            try
            {
                ViewBag.Id = (int)HttpContext.Session.GetInt32("userId");

                question.ModifiedBy = ViewBag.Id;
                question.ModifiedOn = DateTime.Now;
                await Svc.PutAsJsonAsync<Question>($"{questionId}", question);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [Route("Question/Delete/{questionId}")]
        public async Task<ActionResult> Delete(int questionId)
        {
            Question question = await Svc.GetFromJsonAsync<Question>($"ByQuestionId/{questionId}");
            return View(question);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Question/Delete/{questionId}")]
        public async Task<ActionResult> Delete(int questionId, Question question)
        {
            try
            {
                await Svc.DeleteAsync($"{questionId}");
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public async Task<ActionResult> GetQuestionByTopicId(int topicId)
        {
            List<Question> questions = await Svc.GetFromJsonAsync<List<Question>>($"BytopicId/{topicId}");
            return View(questions);
        }
        public async Task<ActionResult> GetTopicNameByTopicId(int topicId)
        {
            Question question = await Svc.GetFromJsonAsync<Question>($"ByTopicIdName/" + topicId);
            return View(question);
        }
    }
}
