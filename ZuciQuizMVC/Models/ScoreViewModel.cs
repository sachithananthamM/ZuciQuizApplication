using ZuciQuizLibrary.Models;

namespace ZuciQuizMVC.Models
{
    public class ScoreViewModel : Score
    {
        public string Feedback { get; set; }
        public string UserName {  get; set; }
        public string TopicName {  get; set; }
    }
}
