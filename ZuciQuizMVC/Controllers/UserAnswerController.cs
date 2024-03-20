using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZuciQuizLibrary.Models;
using ZuciQuizLibrary.Services;

namespace ZuciQuizMVC.Controllers
{
    public class UserAnswerController : Controller
    {
        static HttpClient Svc = new HttpClient { BaseAddress = new Uri("http://localhost:5182/api/UserAnswer/") };
        static HttpClient Svc2 = new HttpClient { BaseAddress = new Uri("http://localhost:5182/api/Answer/") };
        static HttpClient Svc1 = new HttpClient { BaseAddress = new Uri("http://localhost:5182/api/Score/") };
        public async Task<ActionResult> Index()
        {

            List<UserAnswer>userAnswers = await Svc.GetFromJsonAsync<List<UserAnswer>>("");
            return View(userAnswers);
        }

        public async Task<ActionResult> Details(int userAnswerId)
        {
            UserAnswer userAnswer = await Svc.GetFromJsonAsync<UserAnswer>("");
            return View(userAnswer);
        }
        [HttpGet]
        public async Task<ActionResult> GetAllQuestionByTopicId(int topicId)
        {
            try
            {
                string TopicName;
                HttpContext.Session.SetInt32("TopicId", topicId);
                QuestionWithTopic questions = await Svc.GetFromJsonAsync<QuestionWithTopic>($"BytopicId/{topicId}");
                HttpContext.Session.SetInt32("Total",questions.AllQuestionWithTopic.Count);
                return View(questions);
            }
            catch
            {
                throw new Exception("Question Fetching Error");
            }
        }
        [HttpGet]
        public async Task<ActionResult> CheckIsCorrect(int answerId)
        {
            try
            {
                Answer answer = await Svc2.GetFromJsonAsync<Answer>("" + "ByAnswer/" + answerId);
                return View(answer.IsCorrect);
            }
            catch (Exception)
            {
                return View(null);
            }
        }
        [HttpPost]
        public async Task<ActionResult> Store([FromBody] List<UserAnswer> selectedOptions)
        {
            try
            {
                int totalQuestion;
               // HttpContext.Session.SetInt32("Count", selectedOptions.Count);
                int userId = (int)HttpContext.Session.GetInt32("userId");
                int topicId = (int)HttpContext.Session.GetInt32("TopicId");

                int totalScore = 0;

                foreach (var question in selectedOptions)
                {
                    bool isCorrect = await IsAnswerCorrect(question.AnswerId);

                    if (isCorrect)
                    {
                        totalScore++;
                    }

                    UserAnswer userAnswer = new UserAnswer
                    {
                        AnswerId = question.AnswerId,
                        UserId = userId,
                        QuestionId = question.QuestionId
                    };

                    await Create(userAnswer);
                }
                string DateCompleted = HttpContext.Session.GetString("DateCompleted");
                ViewBag.DateCompleted = DateTime.Now;
                ViewBag.TotalScore = totalScore;

                Score score = new Score
                {
                    UserId = userId,
                    Mark = totalScore,
                    TopicId = topicId,
                    DateCompleted = DateTime.Now
                };
                HttpContext.Session.SetInt32("Mark", score.Mark);
                await PostScore(score);

                return RedirectToAction("Details", "Score", new { score });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        private async Task<bool> IsAnswerCorrect(int answerId)
        {
            try
            {
                Answer answer = await Svc2.GetFromJsonAsync<Answer>("ByAnswer/" + answerId);
                return answer.IsCorrect;
            }
            catch (Exception)
            {
                return false;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<ActionResult> PostScore(Score score)
        {
            try
            { 
                await Svc1.PostAsJsonAsync("", score);
                return RedirectToAction(nameof(Index));
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Error posting score: " + ex.Message);
            }
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(UserAnswer userAnswer)
        {
            try
            {
                await Svc.PostAsJsonAsync<UserAnswer>("", userAnswer);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

    }
}
