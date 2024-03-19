using Microsoft.AspNetCore.Mvc.Rendering;
using ZuciQuizLibrary.DataAccessLayer;
using ZuciQuizLibrary.DataAccessLayer.Interfaces;
using ZuciQuizLibrary.Models;
using ZuciQuizLibrary.Services;
using ZuciQuizLibrary.Services.Interfaces;

namespace ZuciQuizMVC.Models
{
    public class ForeignKeyHelper
    {

        public static async Task<List<SelectListItem>> GetTopicNameByTopicId()
        {
            List<SelectListItem> allTopics = new List<SelectListItem>();
            ITopicRepository topicRepository = new TopicRepository();
            List<Topic> topic = await topicRepository.GetAllTopic();
            foreach (Topic topics in topic)
            {
                allTopics.Add(new SelectListItem { Text = topics.TopicName + '-' + topics.Id, Value = topics.Id.ToString() });

            }
            return allTopics;
        }
        public static async Task<List<SelectListItem>> GetQuestionByQuestionId()
        {
            List<SelectListItem> allQuestions = new List<SelectListItem>();
            IQuestionRepository questionRepository = new QuestionRepository();
            List<Question> questions = await questionRepository.GetAllQuestion();
            foreach (Question question in questions)
            {
                allQuestions.Add(new SelectListItem { Text = question.QuestionText , Value = question.Id.ToString() });

            }
            return allQuestions;
        }
    }
}
