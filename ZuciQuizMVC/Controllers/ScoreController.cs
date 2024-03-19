using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZuciQuizLibrary.Models;

namespace ZuciQuizMVC.Controllers
{
    public class ScoreController : Controller
    {//score base
        static HttpClient Svc = new HttpClient { BaseAddress = new Uri("http://localhost:5182/api/Score/") };
        public ActionResult Details()
        {
            int userId = (int)HttpContext.Session.GetInt32("userId");
            string UserName = HttpContext.Session.GetString("userName");
            int topicId = (int)HttpContext.Session.GetInt32("TopicId");
            int totalQuestions = (int)HttpContext.Session.GetInt32("Count");
            int correctCount = (int)HttpContext.Session.GetInt32("Mark");
            double score = (double)correctCount / totalQuestions * 100;
            string feedback;
            Score score1 = new Score();
            score1.Mark = (int)score;
            score1.UserId = userId;
            score1.TopicId = topicId;
            score1.DateCompleted = DateTime.Now;
           /* if (score >= 90)
            {
                score1.feedback ="Congratulations on achieving a Highest score of this quiz! Your thorough understanding and impeccable performance are truly commendable.";
            }
            else if(score >= 80)
            {
                score1.feedback = "Well done! Continue Learning Make You Achive More and Your strong grasp of the material and solid performance are evident.";
            }
            else if(score >=60)
            {
                score1.feedback = "Your effort and understanding are notable, and there's potential for further growth.";   
            }
            else
            {
                score1.feedback = "Sorry You Are Fail in this Subject....Try to learn....";
            }*/
            return View(score1);
        }
        public async Task<ActionResult> GetOneUserScore(int userId)
        {
            List<Score> scores = await Svc.GetFromJsonAsync<List<Score>>($"ByUserId/{userId}");
            return View(scores);
        }
    }
}
