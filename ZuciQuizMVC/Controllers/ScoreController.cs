using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZuciQuizLibrary.Models;
using ZuciQuizMVC.Models;

namespace ZuciQuizMVC.Controllers
{
    public class ScoreController : Controller
    {
        static HttpClient Svc = new HttpClient { BaseAddress = new Uri("http://localhost:5182/api/Score/") };
        static HttpClient Svc1 = new HttpClient { BaseAddress = new Uri("http://localhost:5182/api/Topic/") };

        public async Task<ActionResult> Details()
        {
            int userId = (int)HttpContext.Session.GetInt32("userId");
            string UserName = HttpContext.Session.GetString("userName");
            int topicId = (int)HttpContext.Session.GetInt32("TopicId");

            int totalQuestions = (int)HttpContext.Session.GetInt32("Total");
            int correctCount = (int)HttpContext.Session.GetInt32("Mark");
            double score = (double)correctCount / totalQuestions * 100;
            string feedback;

            Topic topicObject = await Svc1.GetFromJsonAsync<Topic>($"ByTopicId/{topicId}"); 
            ScoreViewModel score1 = new ScoreViewModel();
            score1.Mark = (int)score;
            score1.TopicName = topicObject.TopicName;
            score1.UserName = UserName;
            score1.DateCompleted = DateTime.Now;
            if (score >= 90)
            {
                score1.Feedback = "Congratulations on achieving a Highest score of this quiz! Your thorough understanding and impeccable performance are truly commendable.";
            }
            else if (score >= 80)
            {
                score1.Feedback = "Well done! Continue Learning Make You Achive More and Your strong grasp of the material and solid performance are evident.";
            }
            else if (score >= 60)
            {
                score1.Feedback = "Your effort and understanding are notable, and there's potential for further growth.";
            }
            else
            {
                score1.Feedback = "Sorry You Are Fail in this Subject....Try to learn....";
            }
            return View(score1);
        }
        public async Task<ActionResult> GetOneUserScore(int userId)
        {
            List<Score> scores = await Svc.GetFromJsonAsync<List<Score>>($"ByUserId/{userId}");
            return View(scores);
        }
    }
}
