using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZuciQuizLibrary.Models;
using ZuciQuizLibrary.Services.Interfaces;

namespace ZuciQuizMVC.Controllers
{
    public class AnswerController : Controller
    {
        private readonly IAnswerService _answerService;
        static HttpClient Clint = new HttpClient { BaseAddress = new Uri("http://localhost:5182/api/Answer/") };

        public async Task<ActionResult> Index()
        {
            List<Answer> answers = await Clint.GetFromJsonAsync<List<Answer>>("");
            return View(answers);
        }

        public async Task<ActionResult> Details(int answerId)
        {
            Answer answer = await Clint.GetFromJsonAsync<Answer>($"ByAnswerId/{answerId}");
            return View(answer);
        }
        public async Task<ActionResult> GetCorrectAnswer(int answerId)
        {
            Answer answer = await Clint.GetFromJsonAsync<Answer>($"ByAnswer/{answerId}");
            return View(answer);
        }

        public async Task<ActionResult> GetQuestionWithOptions(int questionId)
        {
            try
            {
                QuestionWithOptions answer = await Clint.GetFromJsonAsync<QuestionWithOptions>($"QuestionWithOptions/{questionId}");
                return View(answer);
            }
            catch
            {
                throw new Exception("Error Accour in Options");
            }
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Answer answer)
        {
            try
            {
                ViewBag.Id = (int)HttpContext.Session.GetInt32("userId");
                answer.CreatedBy = ViewBag.Id;
                answer.CreatedOn=DateTime.Now;
                answer.ModifiedBy = ViewBag.Id;
                answer.ModifiedOn = DateTime.Now;
                await Clint.PostAsJsonAsync("", answer);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [Route("Answer/Edit/{answerId}")]
        public async Task<ActionResult> Edit(int answerId)
        {
            Answer answer = await Clint.GetFromJsonAsync<Answer>("" + "ByAnswerId/" + answerId);
            return View(answer);
        }

        [Route("Answer/Edit/{answerId}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int answerId, Answer answer)
        {
            try
            {
                ViewBag.Id = (int)HttpContext.Session.GetInt32("userId");
                answer.ModifiedBy = ViewBag.Id;
                answer.ModifiedOn = DateTime.Now;
                await Clint.PutAsJsonAsync($"{answerId}", answer);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [Route("Answer/Delete/{answerId}")]
        public async Task<ActionResult> Delete(int answerId)
        {
            Answer answer = await Clint.GetFromJsonAsync<Answer>($"ByAnswerId/{answerId}");
            return View(answer);
        }

        [Route("Answer/Delete/{answerId}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int answerId, Answer answer)
        {
            try
            {
                await Clint.DeleteAsync($"{answerId}");
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public async Task<ActionResult> GetAllOptionsByQuestionId(int questionId)
        {
            List<Answer> answers = await Clint.GetFromJsonAsync<List<Answer>>("" + "ByQuestionId/" + questionId);
            return View(answers);
        }


    }
}
